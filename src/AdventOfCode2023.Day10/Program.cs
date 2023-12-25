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

var (up, down, left, right) = (Up(), Down(grid.Length), Left(), Right(grid[0].Length));

var enclosed = new HashSet<Point>();

var loopArray = loop.ToArray();

for (var i = 1; i < loopArray.Length; i++)
{
    var (deltaX, deltaY) = (loopArray[i].x - loopArray[i - 1].x, loopArray[i].y - loopArray[i - 1].y);

    var (iterator, condition) = (deltaX, deltaY) switch
    {
        (1, 0) => down,
        (-1, 0) => up,
        (0, -1) => right,
        (0, 1) => left,
        _ => throw new InvalidOperationException($"invalid loop point delta: ({deltaX}, {deltaY})"),
    };

    for (var p = iterator(loopArray[i]); condition(p); p = iterator(p))
    {
        if (loop.Contains(p))
        {
            break;
        }

        enclosed.Add(p);
    }
}

var puzzle2 = enclosed.Count;

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

static (Func<Point, Point> iterator, Func<Point, bool> condition) Up() =>
    (p => (p.x, p.y - 1), p => p.y >= 0);

static (Func<Point, Point> iterator, Func<Point, bool> condition) Down(
    int maxY) =>
    (p => (p.x, p.y + 1), p => p.y < maxY);

static (Func<Point, Point> iterator, Func<Point, bool> condition) Left() =>
    (p => (p.x - 1, p.y), p => p.x >= 0);

static (Func<Point, Point> iterator, Func<Point, bool> condition) Right(
    int maxX) =>
    (p => (p.x + 1, p.y), p => p.x < maxX);
