using System.Buffers;

namespace AdventOfCode2023.Day01;

internal static class DigitsUtil
{
    private static readonly SearchValues<char> _digits = SearchValues.Create("0123456789");

    internal static int ParseLine(
        string line)
    {
        var span = line.AsSpan();

        var firstDigit = span.IndexOfAny(_digits);

        var lastDigit = span.LastIndexOfAny(_digits);

        return (line[firstDigit] - '0') * 10 + (line[lastDigit] - '0');
    }
}
