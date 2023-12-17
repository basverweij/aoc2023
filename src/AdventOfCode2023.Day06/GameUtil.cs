namespace AdventOfCode2023.Day06;

internal static class GameUtil
{
    internal static int WinOptions(
        int time,
        int distance)
    {
        var d = Math.Sqrt(time * time - 4 * distance);

        var min = (int)Math.Ceiling((time - d) / 2d);

        if (min * time - min * min <= distance)
        {
            min++;
        }

        var max = (int)Math.Floor((time + d) / 2d);

        if (max * time - max * max <= distance)
        {
            max--;
        }

        return max - min + 1;
    }
}
