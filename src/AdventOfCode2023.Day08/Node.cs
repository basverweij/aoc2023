namespace AdventOfCode2023.Day08;

internal sealed record Node(
    string Name,
    string Left,
    string Right)
{
    internal static Node Parse(
        string line) =>
        new(line[0..3], line[7..10], line[12..15]);
}
