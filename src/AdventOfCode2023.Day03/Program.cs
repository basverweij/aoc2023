using AdventOfCode2023.Day03;

var lines = await File.ReadAllLinesAsync("input.txt");

var partNumbers = new List<int>();

for (var i = 0; i < lines.Length; i++)
{
    partNumbers.AddRange(PartsUtil.GetPartNumbers(i, lines));
}

var puzzle1 = partNumbers.Sum();

Console.WriteLine($"Day 3 - Puzzle 1: {puzzle1}");
