using System.Buffers;
using System.Text.RegularExpressions;

namespace AdventOfCode2023.Day01;

internal static partial class DigitsUtil
{
    internal static int ParseLineForDigits(
        string line)
    {
        var span = line.AsSpan();

        var firstDigit = span.IndexOfAny(_digits);

        var lastDigit = span.LastIndexOfAny(_digits);

        return (line[firstDigit] - '0') * 10 + (line[lastDigit] - '0');
    }

    internal static int ParseLineForDigitsAndNumbers(
        string line)
    {
        var matches = DigitsAndNumbers().Matches(line);

        var firstDigit = ParseMatch(matches[0]);

        var lastDigit = ParseMatch(matches[^1]);

        return (firstDigit * 10) + lastDigit;
    }

    #region Helpers

    private static readonly SearchValues<char> _digits = SearchValues.Create("0123456789");

    [GeneratedRegex(@"([0-9])|(?=(one))|(?=(two))|(?=(three))|(?=(four))|(?=(five))|(?=(six))|(?=(seven))|(?=(eight))|(?=(nine))")]
    private static partial Regex DigitsAndNumbers();

    private static int ParseMatch(
        Match match)
    {
        if (match.Groups[1].Success)
        {
            return match.Groups[1].ValueSpan[0] - '0';
        }

        for (var i = 2; i < match.Groups.Count; i++)
        {
            if (match.Groups[i].Success)
            {
                return i - 1;
            }
        }

        throw new InvalidOperationException("should never happen");
    }

    #endregion
}
