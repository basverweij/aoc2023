namespace AdventOfCode2023.Day08;

internal sealed record Node(
    string Name,
    string Left,
    string Right)
{
    internal static Node Parse(
        string line) =>
        new(line[0..3], line[7..10], line[12..15]);

    internal ushort Index { get; } = (ushort)((Name[0] - 'A') * 26 * 26 + (Name[1] - 'A') * 26 + (Name[2] - 'A'));
}
