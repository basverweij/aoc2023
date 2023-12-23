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

var (leftNodes, rightNodes, endNodes) = (new ushort[17_576], new ushort[17_576], new bool[17_576]);

foreach (var n in nodes.Values)
{
    leftNodes[n.Index] = nodes[n.Left].Index;

    rightNodes[n.Index] = nodes[n.Right].Index;

    endNodes[n.Index] = n.Name.EndsWith('Z');
}

(instructionPointer, steps, var indexes) = (0, 1, nodes.Values.Where(n => n.Name.EndsWith('A')).Select(n => n.Index).ToArray());

for (; ; steps++)
{
    if (steps % 100_000_000 == 0) { Console.WriteLine(steps); }

    var goLeft = instructions[instructionPointer] == 'L';

    var allEndNodes = true;

    for (var i = 0; i < indexes.Length; i++)
    {
        indexes[i] = goLeft ? leftNodes[indexes[i]] : rightNodes[indexes[i]];

        if (!endNodes[indexes[i]])
        {
            allEndNodes = false;
        }
    }

    if (allEndNodes)
    {
        break;
    }

    instructionPointer = (instructionPointer + 1) % instructions.Length;
}

Console.WriteLine($"Day 8 - Puzzle 2: {steps}");
