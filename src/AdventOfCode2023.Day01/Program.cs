using AdventOfCode2023.Day01;

var lines = await File.ReadAllLinesAsync("input.txt");

var puzzle1 = lines.Sum(DigitsUtil.ParseLineForDigits);

Console.WriteLine($"Day 1 - Puzzle 1: {puzzle1}");

var puzzle2 = lines.Sum(DigitsUtil.ParseLineForDigitsAndNumbers);

Console.WriteLine($"Day 1 - Puzzle 2: {puzzle2}");
