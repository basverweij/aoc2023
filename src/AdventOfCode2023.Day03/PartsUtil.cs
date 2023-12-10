using System.Text.RegularExpressions;

namespace AdventOfCode2023.Day03;

internal static partial class PartsUtil
{
    internal static int[] GetPartNumbers(
        int row,
        string[] lines)
    {
        var partNumbers = new List<int>();

        var matches = PartNumbers().Matches(lines[row]).OfType<Match>();

        foreach (var match in matches)
        {
            if (!lines.IsAdjacentToSymbol(row, match.Index, match.Index + match.Length - 1))
            {
                continue;
            }

            partNumbers.Add(int.Parse(match.ValueSpan));
        }

        return [.. partNumbers];
    }

    private static bool IsAdjacentToSymbol(
        this string[] @this,
        int row,
        int columnFromInclusive,
        int columnToInclusive)
    {
        int fromRowInclusive = row == 0 ? row : row - 1;

        int toRowInclusive = row == @this.Length - 1 ? row : row + 1;

        if (columnFromInclusive > 0 && @this.HasSymbolInColumn(columnFromInclusive - 1, fromRowInclusive, toRowInclusive))
        {
            return true;
        }

        if (columnToInclusive < @this[row].Length - 1 && @this.HasSymbolInColumn(columnToInclusive + 1, fromRowInclusive, toRowInclusive))
        {
            return true;
        }

        if (row > 0 && @this.HasSymbolInRow(row - 1, columnFromInclusive, columnToInclusive))
        {
            return true;
        }

        if (row < @this.Length - 1 && @this.HasSymbolInRow(row + 1, columnFromInclusive, columnToInclusive))
        {
            return true;
        }

        return false;
    }

    private static bool HasSymbolInColumn(
        this string[] @this,
        int column,
        int fromRowInclusive,
        int toRowInclusive)
    {
        for (var row = fromRowInclusive; row <= toRowInclusive; row++)
        {
            var c = @this[row][column];

            if ((c < '0' || c > '9') && c != '.')
            {
                return true;
            }
        }

        return false;
    }

    private static bool HasSymbolInRow(
        this string[] @this,
        int row,
        int fromColumnInclusive,
        int toColumnInclusive)
    {
        for (var column = fromColumnInclusive; column <= toColumnInclusive; column++)
        {
            var c = @this[row][column];

            if ((c < '0' || c > '9') && c != '.')
            {
                return true;
            }
        }

        return false;
    }

    [GeneratedRegex("""\d+""")]
    private static partial Regex PartNumbers();
}
