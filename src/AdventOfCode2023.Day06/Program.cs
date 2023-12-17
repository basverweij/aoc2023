using System.Collections.Immutable;
using AdventOfCode2023.Day06;

var lines = await File.ReadAllLinesAsync("input.txt");

var times = ParseValues(lines[0]);

var distances = ParseValues(lines[1]);

var winOptions = times.Zip(distances).Select(z => GameUtil.WinOptions(z.First, z.Second)).ToImmutableArray();

var puzzle1 = winOptions.Aggregate(1, (a, s) => a * s);

Console.WriteLine($"Day 6 - Puzzle 1: {puzzle1}");

static ImmutableArray<int> ParseValues(
    string line) => line
    .Split(' ', StringSplitOptions.RemoveEmptyEntries)
    .Skip(1)
    .Select(int.Parse)
    .ToImmutableArray();
