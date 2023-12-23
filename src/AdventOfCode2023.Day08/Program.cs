using AdventOfCode2023.Day08;

using System.Collections.Frozen;
using System.Collections.Immutable;

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

var startNodes = nodes.Values.Where(n => n.Name.EndsWith('A')).ToImmutableArray();

var nodeSteps = new int[startNodes.Length];

for (var i = 0; i < startNodes.Length; i++)
{
    (instructionPointer, steps) = (0, 0);

    node = startNodes[i];

    for (; !node.Name.EndsWith('Z'); steps++)
    {
        node = instructions[instructionPointer] == 'L' ? nodes[node.Left] : nodes[node.Right];

        instructionPointer = (instructionPointer + 1) % instructions.Length;
    }

    nodeSteps[i] = steps;
}

var puzzle2 = Lcm(nodeSteps);

Console.WriteLine($"Day 8 - Puzzle 2: {puzzle2}");

static long Lcm(
    int[] values)
{
    long lcm = values[0];

    for (var i = 1; i < values.Length; i++)
    {
        lcm *= values[i] / Gcd(lcm, values[i]);
    }

    return lcm;
}

static long Gcd(
    long a,
    long b) =>
    b == 0 ? a : Gcd(b, a % b);
