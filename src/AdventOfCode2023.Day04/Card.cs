using System.Collections.Immutable;
using System.Text.RegularExpressions;

namespace AdventOfCode2023.Day04;

internal sealed partial record Card(
    int Index,
    IReadOnlySet<int> Winning,
    IReadOnlySet<int> Have)
{
    internal static Card Parse(
        string line)
    {
        var match = ParseCard().Match(line);

        if (!match.Success)
        {
            throw new ArgumentException("invalid line", nameof(line));
        }

        var index = int.Parse(match.Groups[nameof(Card.Index)].ValueSpan);

        var winning = ParseNumbers(match.Groups[nameof(Card.Winning)]);

        var have = ParseNumbers(match.Groups[nameof(Card.Have)]);

        return new(index, winning, have);
    }

    private static IReadOnlySet<int> ParseNumbers(
        Group group) =>
        group
            .Captures
            .Select(c => int.Parse(c.ValueSpan))
            .ToImmutableHashSet();

    [GeneratedRegex("""^Card\s+(?<Index>\d+):(\s+(?<Winning>\d+))+\s+\|(\s+(?<Have>\d+))+$""")]
    private static partial Regex ParseCard();
}
