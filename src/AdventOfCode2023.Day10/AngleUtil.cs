namespace AdventOfCode2023.Day10;
internal static class AngleUtil
{
    internal static bool IsClockwise(
        IEnumerable<Point> loop)
    {
        var angles = 0;

        var previous = loop.First();

        var previousDelta = (0, 0);

        foreach (var current in loop.Skip(1))
        {
            var delta = (current.x - previous.x, current.y - previous.y);

            if (previousDelta == (0, 0))
            {
                previousDelta = delta;
            }
            else if (previousDelta != delta)
            {
                angles += Angle(previousDelta, delta);

                previousDelta = delta;
            }

            previous = current;
        }

        return angles > 0;
    }

    #region Helpers

    private static int Angle(
        Point previousDelta,
        Point delta) =>
        _angles.TryGetValue((previousDelta, delta), out var angle) ?
            angle :
            0;

    private static readonly Dictionary<(Point, Point), int> _angles = new()
    {
        [((1, 0), (0, 1))] = 1,
        [((1, 0), (0, -1))] = -1,
        [((-1, 0), (0, 1))] = -1,
        [((-1, 0), (0, -1))] = 1,
        [((0, 1), (1, 0))] = -1,
        [((0, 1), (-1, 0))] = 1,
        [((0, -1), (1, 0))] = 1,
        [((0, -1), (-1, 0))] = -1,
    };

    #endregion
}
