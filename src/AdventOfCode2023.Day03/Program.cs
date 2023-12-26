using AdventOfCode2023.Day03;

using System.Collections.Immutable;

var lines = await File.ReadAllLinesAsync("input.txt");

var partNumbers = new List<(int partNumber, Symbol symbol)>();

for (var i = 0; i < lines.Length; i++)
{
    partNumbers.AddRange(PartsUtil.GetPartNumbers(i, lines));
}

var puzzle1 = partNumbers.Sum(pn => pn.partNumber);

Console.WriteLine($"Day 3 - Puzzle 1: {puzzle1}");

var symbols = partNumbers
    .Where(pn => pn.symbol.Value == '*')
    .GroupBy(pn => pn.symbol, pn => pn.partNumber)
    .ToDictionary(g => g.Key, g => g.ToImmutableArray())
    .Where(s => s.Value.Length == 2);

var puzzle2 = symbols.Sum(s => s.Value[0] * s.Value[1]);

Console.WriteLine($"Day 3 - Puzzle 2: {puzzle2}");
