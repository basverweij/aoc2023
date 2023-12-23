namespace AdventOfCode2023.Day07;

internal sealed record HandWithJokers(
    char[] Cards,
    int Bid
) : IComparable<HandWithJokers>
{
    internal static HandWithJokers Parse(
        string line)
    {
        var span = line.AsSpan();

        var bid = int.Parse(span[6..]);

        return new(span[0..5].ToArray(), bid);
    }

    public HandType Type { get; } = BuildType(Cards);

    public int CompareTo(
        HandWithJokers? other)
    {
        ArgumentNullException.ThrowIfNull(other);

        if (Type != other.Type)
        {

            return Type - other.Type;
        }

        for (var i = 0; i < Cards.Length; i++)
        {
            if (Cards[i] != other.Cards[i])
            {

                return _cardValues[Cards[i]] - _cardValues[other.Cards[i]];
            }
        }

        return 0;
    }

    #region Helpers

    private static readonly Dictionary<char, int> _cardValues = new()
    {
        ['J'] = 1,
        ['2'] = 2,
        ['3'] = 3,
        ['4'] = 4,
        ['5'] = 5,
        ['6'] = 6,
        ['7'] = 7,
        ['8'] = 8,
        ['9'] = 9,
        ['T'] = 10,
        ['Q'] = 11,
        ['K'] = 12,
        ['A'] = 13,
    };

    private static HandType BuildType(
        char[] cards)
    {
        var jokerCount = cards.Where(c => c == 'J').Count();

        if (jokerCount == 5)
        {
            return HandType.FiveOfAKind;
        }

        var counts = cards
            .Where(c => c != 'J')
            .GroupBy(c => c)
            .Select(g => g.Count())
            .OrderDescending()
            .Take(2)
            .ToArray();

        return (counts[0] + jokerCount) switch
        {
            5 => HandType.FiveOfAKind,
            4 => HandType.FourOfAKind,
            3 when counts[1] == 2 => HandType.FullHouse,
            3 => HandType.ThreeOfAKind,
            2 when counts[1] == 2 => HandType.TwoPair,
            2 => HandType.OnePair,
            _ => HandType.HighCard,
        };
    }

    #endregion
}
