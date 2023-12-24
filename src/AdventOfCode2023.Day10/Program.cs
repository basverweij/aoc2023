using AdventOfCode2023.Day10;

using Point = (int x, int y);

var lines = await File.ReadAllLinesAsync("input.txt");

var grid = lines.Select(line => line.ToCharArray()).ToArray();

var startingPoint = FindStartingPoint(grid);

var candidate = GetNeighbours(grid, startingPoint).First();

var (previous, current) = (startingPoint, candidate);

var length = 0;

for (; current != startingPoint; length++)
{
    (previous, current) = (current, GetNeighbours(grid, current).Single(n => n != previous));
}

var puzzle1 = length / 2 + (length % 2 == 0 ? 0 : 1);

Console.WriteLine($"Day 10 - Puzzle 1: {puzzle1}");

static Point FindStartingPoint(
    char[][] grid)
{
    for (var row = 0; row < grid.Length; row++)
    {
        var column = Array.FindIndex(grid[row], c => c == 'S');

        if (column < 0)
        {
            continue;
        }

        return (column, row);
    }

    throw new ArgumentException("grid does not contain a starting point");
}

static IEnumerable<Point> GetNeighbours(
    char[][] grid,
    Point point)
{
    var connections = grid[point.y][point.x].GetConnections();

    if (connections.HasFlag(Connections.North) &&
        point.y > 0 &&
        grid[point.y - 1][point.x].GetConnections().HasFlag(Connections.South))
    {
        yield return (point.x, point.y - 1);
    }

    if (connections.HasFlag(Connections.East) &&
        point.x < grid[point.y].Length - 1 &&
        grid[point.y][point.x + 1].GetConnections().HasFlag(Connections.West))
    {
        yield return (point.x + 1, point.y);
    }

    if (connections.HasFlag(Connections.South) &&
        point.y < grid.Length - 1 &&
        grid[point.y + 1][point.x].GetConnections().HasFlag(Connections.North))
    {
        yield return (point.x, point.y + 1);
    }

    if (connections.HasFlag(Connections.West) &&
        point.x > 0 &&
        grid[point.y][point.x - 1].GetConnections().HasFlag(Connections.East))
    {
        yield return (point.x - 1, point.y);
    }
}
