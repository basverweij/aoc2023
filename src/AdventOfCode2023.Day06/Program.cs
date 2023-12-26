using AdventOfCode2023.Day06;

using System.Collections.Immutable;

var lines = await File.ReadAllLinesAsync("input.txt");

var times = ParseValues(lines[0]);

var distances = ParseValues(lines[1]);

var winOptions = times.Zip(distances).Select(z => GameUtil.WinOptions(z.First, z.Second)).ToImmutableArray();

var puzzle1 = winOptions.Aggregate(1L, (a, s) => a * s);

Console.WriteLine($"Day 6 - Puzzle 1: {puzzle1}");

var time = ParseValue(lines[0]);

var distance = ParseValue(lines[1]);

var puzzle2 = GameUtil.WinOptions(time, distance);

Console.WriteLine($"Day 6 - Puzzle 2: {puzzle2}");

static ImmutableArray<int> ParseValues(
    string line) =>
    line
        .Split(' ', StringSplitOptions.RemoveEmptyEntries)
        .Skip(1)
        .Select(int.Parse)
        .ToImmutableArray();

static long ParseValue(
    string line) =>
    long.Parse(line
        .Replace(" ", "")
        .Split(':')[1]);
