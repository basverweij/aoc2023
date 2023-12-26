namespace AdventOfCode2023.Day03;

internal sealed record Symbol(
    char Value,
    int Row,
    int Column)
{
    internal static Symbol None = new('\0', -1, -1);
}
