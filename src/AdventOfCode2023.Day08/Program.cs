using System.Collections.Frozen;
using AdventOfCode2023.Day08;

var lines = await File.ReadAllLinesAsync("input.txt");

var instructions = lines[0].ToCharArray();

var nodes = lines[2..].Select(Node.Parse).ToFrozenDictionary(n => n.Name);

var (instructionPointer, steps, node) = (0, 0, nodes["AAA"]);

for (; node.Name != "ZZZ"; steps++)
{
    node = instructions[instructionPointer] == 'L' ? nodes[node.Left] : nodes[node.Right];

    instructionPointer = (instructionPointer + 1) % instructions.Length;
}

Console.WriteLine($"Day 8 - Puzzle 1: {steps}");
