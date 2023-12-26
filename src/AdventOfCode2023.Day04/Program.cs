using AdventOfCode2023.Day04;

using System.Collections.Immutable;

var lines = await File.ReadAllLinesAsync("input.txt");

var cards = lines.Select(Card.Parse).ToImmutableArray();

var puzzle1 = cards.Sum(Points);

Console.WriteLine($"Day 4 - Puzzle 1: {puzzle1}");

var counts = Enumerable.Repeat(1, cards.Length).ToArray();

foreach (var card in cards)
{
    var haveWinningCount = card.HaveWinningCount;

    if (haveWinningCount == 0)
    {
        continue;
    }

    var count = counts[card.Index];

    for (var i = 1; i <= haveWinningCount; i++)
    {
        counts[card.Index + i] += count;
    }
}

var puzzle2 = counts.Sum();

Console.WriteLine($"Day 4 - Puzzle 2: {puzzle2}");

static int Points(
    Card card)
{
    var haveWinningCount = card.HaveWinningCount;

    return haveWinningCount == 0 ?
        0 :
        1 << (haveWinningCount - 1);
}
