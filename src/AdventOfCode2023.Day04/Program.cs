using System.Collections.Immutable;
using AdventOfCode2023.Day04;

var lines = await File.ReadAllLinesAsync("input.txt");

var cards = lines.Select(Card.Parse).ToImmutableArray();

var puzzle1 = cards.Sum(Points);

Console.WriteLine($"Day 4 - Puzzle 1: {puzzle1}");

static int Points(
    Card card)
{
    var haveWinningCount = card.Have.Intersect(card.Winning).Count();

    return haveWinningCount == 0 ?
        0 :
        1 << (haveWinningCount - 1);
}
