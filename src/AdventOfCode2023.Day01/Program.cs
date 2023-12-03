using System.Buffers;
using AdventOfCode2023.Day01;

var lines = await File.ReadAllLinesAsync("input.txt");

var puzzle1 = lines.Sum(DigitsUtil.ParseLine);

Console.WriteLine($"Day 1 - Puzzle 1: {puzzle1}");
