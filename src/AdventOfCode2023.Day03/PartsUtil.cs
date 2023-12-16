using System.Text.RegularExpressions;

namespace AdventOfCode2023.Day03;

internal static partial class PartsUtil
{
    internal static (int partNumber, Symbol symbol)[] GetPartNumbers(
        int row,
        string[] lines)
    {
        var partNumbers = new List<(int, Symbol)>();

        var matches = PartNumbers().Matches(lines[row]).OfType<Match>();

        foreach (var match in matches)
        {
            if (!lines.IsAdjacentToSymbol(row, match.Index, match.Index + match.Length - 1, out var symbol))
            {
                continue;
            }

            partNumbers.Add((int.Parse(match.ValueSpan), symbol));
        }

        return [.. partNumbers];
    }

    private static bool IsAdjacentToSymbol(
        this string[] @this,
        int row,
        int columnFromInclusive,
        int columnToInclusive,
        out Symbol symbol)
    {
        int fromRowInclusive = row == 0 ? row : row - 1;

        int toRowInclusive = row == @this.Length - 1 ? row : row + 1;

        if (columnFromInclusive > 0 && @this.HasSymbolInColumn(columnFromInclusive - 1, fromRowInclusive, toRowInclusive, out symbol))
        {
            return true;
        }

        if (columnToInclusive < @this[row].Length - 1 && @this.HasSymbolInColumn(columnToInclusive + 1, fromRowInclusive, toRowInclusive, out symbol))
        {
            return true;
        }

        if (row > 0 && @this.HasSymbolInRow(row - 1, columnFromInclusive, columnToInclusive, out symbol))
        {
            return true;
        }

        if (row < @this.Length - 1 && @this.HasSymbolInRow(row + 1, columnFromInclusive, columnToInclusive, out symbol))
        {
            return true;
        }

        symbol = Symbol.None;

        return false;
    }

    private static bool HasSymbolInColumn(
        this string[] @this,
        int column,
        int fromRowInclusive,
        int toRowInclusive,
        out Symbol symbol)
    {
        for (var row = fromRowInclusive; row <= toRowInclusive; row++)
        {
            var c = @this[row][column];

            if ((c < '0' || c > '9') && c != '.')
            {
                symbol = new(c, row, column);

                return true;
            }
        }

        symbol = Symbol.None;

        return false;
    }

    private static bool HasSymbolInRow(
        this string[] @this,
        int row,
        int fromColumnInclusive,
        int toColumnInclusive,
        out Symbol symbol)
    {
        for (var column = fromColumnInclusive; column <= toColumnInclusive; column++)
        {
            var c = @this[row][column];

            if ((c < '0' || c > '9') && c != '.')
            {
                symbol = new(c, row, column);

                return true;
            }
        }

        symbol = Symbol.None;

        return false;
    }

    [GeneratedRegex("""\d+""")]
    private static partial Regex PartNumbers();
}
