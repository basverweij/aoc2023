namespace AdventOfCode2023.Day12;
internal sealed record Record(
    string Springs,
    int[] GroupLengths)
{
    internal static Record Parse(
        string line)
    {
        var space = line.IndexOf(' ');

        var springs = line[..space];

        var groupLengths = line[(space + 1)..].Split(',').Select(int.Parse).ToArray();

        return new(springs, groupLengths);
    }
}
