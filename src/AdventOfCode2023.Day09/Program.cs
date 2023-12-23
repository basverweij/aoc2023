using AdventOfCode2023.Day09;

using System.Collections.Immutable;

var lines = await File.ReadAllLinesAsync("input.txt");

var reports = lines.Select(ParseReport).ToImmutableArray();

var puzzle1 = reports.Select(ReportUtil.Extrapolate).Sum();

Console.WriteLine($"Day 9 - Puzzle 1: {puzzle1}");

var puzzle2 = reports.Select(ReportUtil.ExtrapolateBackwards).Sum();

Console.WriteLine($"Day 9 - Puzzle 2: {puzzle2}");

static long[] ParseReport(string line) => line.Split(' ').Select(long.Parse).ToArray();
