namespace AdventOfCode2023.Day07;

internal sealed record Hand(
    char[] Cards,
    int Bid
) : IComparable<Hand>
{
    internal static Hand Parse(
        string line)
    {
        var span = line.AsSpan();

        var bid = int.Parse(span[6..]);

        return new(span[0..5].ToArray(), bid);
    }

    public HandType Type { get; } = BuildType(Cards);

    public int CompareTo(
        Hand? other)
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
        ['2'] = 2,
        ['3'] = 3,
        ['4'] = 4,
        ['5'] = 5,
        ['6'] = 6,
        ['7'] = 7,
        ['8'] = 8,
        ['9'] = 9,
        ['T'] = 10,
        ['J'] = 11,
        ['Q'] = 12,
        ['K'] = 13,
        ['A'] = 14,
    };

    private static HandType BuildType(
        char[] cards)
    {
        var counts = cards
            .GroupBy(c => c)
            .Select(g => g.Count())
            .OrderDescending()
            .Take(2)
            .ToArray();

        return counts[0] switch
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

internal enum HandType
{
    HighCard,
    OnePair,
    TwoPair,
    ThreeOfAKind,
    FullHouse,
    FourOfAKind,
    FiveOfAKind,
}
