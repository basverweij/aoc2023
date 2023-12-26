using AdventOfCode2023.Day07;

using System.Collections.Immutable;

var lines = await File.ReadAllLinesAsync("input.txt");

var hands = lines.Select(Hand.Parse).Order().ToImmutableArray();

var puzzle1 = hands.Select((hand, rank) => hand.Bid * (rank + 1)).Sum();

Console.WriteLine($"Day 7 - Puzzle 1: {puzzle1}");

var handsWithJokers = lines.Select(HandWithJokers.Parse).Order().ToImmutableArray();

var puzzle2 = handsWithJokers.Select((hand, rank) => hand.Bid * (rank + 1)).Sum();

Console.WriteLine($"Day 7 - Puzzle 2: {puzzle2}");
