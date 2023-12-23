namespace AdventOfCode2023.Day06;

internal static class GameUtil
{
    internal static long WinOptions(
        long time,
        long distance)
    {
        var d = Math.Sqrt(time * time - 4 * distance);

        var min = (long)Math.Ceiling((time - d) / 2d);

        if (min * time - min * min <= distance)
        {
            min++;
        }

        var max = (long)Math.Floor((time + d) / 2d);

        if (max * time - max * max <= distance)
        {
            max--;
        }

        return max - min + 1;
    }
}
