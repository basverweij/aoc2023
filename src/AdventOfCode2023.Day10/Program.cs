using AdventOfCode2023.Day10;

using Point = (int x, int y);

var lines = await File.ReadAllLinesAsync("input.txt");

var grid = lines.Select(line => line.ToCharArray()).ToArray();

var startingPoint = FindStartingPoint(grid);

var (previous, current) = (startingPoint, GetNeighbours(grid, startingPoint).First());

var loop = new HashSet<Point>()
{
    startingPoint,
    current,
};

for (; current != startingPoint; loop.Add(current))
{
    (previous, current) = (current, GetNeighbours(grid, current).Single(n => n != previous));
}

var puzzle1 = loop.Count / 2;

Console.WriteLine($"Day 10 - Puzzle 1: {puzzle1}");

var (minY, maxY) = (loop.MinBy(p => p.y).y, loop.MaxBy(p => p.y).y);

var (minX, maxX) = (loop.MinBy(p => p.x).x, loop.MaxBy(p => p.x).x);

var puzzle2 = 0;

for (var y = minY; y <= maxY; y++)
{
    for (var x = minX; x <= maxX; x++)
    {
        if (loop.Contains((x, y)))
        {
            continue;
        }

        if (IsEnclosed(grid, loop, x, y))
        {
            Console.WriteLine((x, y));

            puzzle2++;
        }
    }
}

Console.WriteLine($"Day 10 - Puzzle 2: {puzzle2}");

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

static bool IsEnclosed(
    char[][] grid,
    IReadOnlySet<Point> loop,
    int x,
    int y)
{
    var (crossings, onCrossing) = (0, false);

    for (var i = x - 1; i >= 0; i--)
    {
        var onLoop = loop.Contains((i, y));

        var connected = grid[y][i].GetConnections().HasFlag(Connections.East);

        switch (onCrossing, onLoop, connected)
        {
            case (false, true, _): onCrossing = true; break;
            case (true, true, false): crossings++; break;
            case (true, false, _): onCrossing = false; crossings++; break;
        }
    }

    if (onCrossing)
    {
        crossings++;
    }

    return crossings % 2 == 1;
}
