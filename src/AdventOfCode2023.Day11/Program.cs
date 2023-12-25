var lines = await File.ReadAllLinesAsync("input.txt");

var space = lines.Select(line => line.ToCharArray()).ToArray();

var galaxies = FindGalaxies(space).ToArray();

var expandedGalaxies = ExpandSpace(galaxies).ToArray();

var pairs = BuildPairs(expandedGalaxies).ToArray();

var puzzle1 = pairs.Select(p => Distance(p.Item1, p.Item2)).Sum();

Console.WriteLine($"Day 11 - Puzzle 1: {puzzle1}");

expandedGalaxies = ExpandSpace(galaxies, 1_000_000).ToArray();

pairs = BuildPairs(expandedGalaxies).ToArray();

var puzzle2 = pairs.Select(p => Distance(p.Item1, p.Item2)).Sum();

Console.WriteLine($"Day 11 - Puzzle 2: {puzzle2}");

static IEnumerable<Point> FindGalaxies(
    char[][] space)
{
    for (var y = 0; y < space.Length; y++)
    {
        for (var x = 0; x < space[y].Length; x++)
        {
            if (space[y][x] == '#')
            {
                yield return (x, y);
            }
        }
    }
}

static IEnumerable<Point> ExpandSpace(
    IEnumerable<Point> galaxies,
    int rate = 2)
{
    var xs = galaxies.Select(g => g.x).ToHashSet();

    var ys = galaxies.Select(g => g.y).ToHashSet();

    foreach (var galaxy in galaxies)
    {
        var expandX = galaxy.x - xs.Count(x => x < galaxy.x);

        var expandY = galaxy.y - ys.Count(y => y < galaxy.y);

        yield return (galaxy.x + expandX * (rate - 1), galaxy.y + expandY * (rate - 1));
    }
}

static IEnumerable<(Point, Point)> BuildPairs(
    IReadOnlyList<Point> points)
{
    for (var i = 0; i < points.Count; i++)
    {
        for (var j = i + 1; j < points.Count; j++)
        {
            yield return (points[i], points[j]);
        }
    }
}

static long Distance(
    Point a,
    Point b) =>
    Math.Abs(a.x - b.x) + Math.Abs(a.y - b.y);
