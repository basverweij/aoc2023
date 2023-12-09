using System.Text.RegularExpressions;

namespace AdventOfCode2023.Day02;

public sealed partial record Game(
    int Id,
    int MaxRed,
    int MaxGreen,
    int MaxBlue)
{
    internal static Game ParseLine(
        string line)
    {
        var match = Line().Match(line);

        if (!match.Success)
        {
            throw new ArgumentException("invalid line", nameof(line));
        }

        var id = int.Parse(match.Groups["id"].Value);

        var (maxRed, maxGreen, maxBlue) = (0, 0, 0);

        var counts = match.Groups["sets"].Value.Split(_separators, StringSplitOptions.None);

        foreach (var count in counts)
        {
            var space = count.IndexOf(' ');

            var n = int.Parse(count[0..space]);

            switch (count[(space + 1)..])
            {
                case "red": if (maxRed < n) { maxRed = n; }; break;
                case "green": if (maxGreen < n) { maxGreen = n; }; break;
                case "blue": if (maxBlue < n) { maxBlue = n; }; break;
            }
        }

        return new(id, maxRed, maxGreen, maxBlue);
    }

    [GeneratedRegex("""^Game (?<id>\d+): (?<sets>.+)$""")]
    private static partial Regex Line();

    private static readonly string[] _separators = ["; ", ", "];
}
