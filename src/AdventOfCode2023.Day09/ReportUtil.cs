namespace AdventOfCode2023.Day09;

internal static class ReportUtil
{
    internal static long Extrapolate(
        long[] report)
    {
        var deltas = GetDeltas(report);

        return deltas.Sum(d => d[^1]);
    }

    internal static long ExtrapolateBackwards(
        long[] report)
    {
        var deltas = GetDeltas(report);

        deltas.Reverse();

        return deltas.Aggregate(0L, (a, delta) => delta[0] - a);
    }

    private static List<long[]> GetDeltas(
        long[] report)
    {
        var deltas = new List<long[]>()
        {
            report,
        };

        var previous = report;

        while (true)
        {
            var delta = new long[previous.Length - 1];

            var done = true;

            for (var i = 0; i < delta.Length; i++)
            {
                delta[i] = previous[i + 1] - previous[i];

                if (delta[i] != 0)
                {
                    done = false;
                }
            }

            if (done)
            {
                break;
            }

            deltas.Add(delta);

            previous = delta;
        }

        return deltas;
    }
}
