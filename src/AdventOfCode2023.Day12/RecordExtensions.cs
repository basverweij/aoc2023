using System.Text.RegularExpressions;

namespace AdventOfCode2023.Day12;

internal static partial class RecordExtensions
{
    internal static int GetArrangementCount(
        this Record @this)
    {
        var springs = @this.Springs.Trim('.');

        var sections = Splitter().Split(springs);

        Console.WriteLine($"{string.Join(" ", @this.GroupLengths)} -> {string.Join(" ", sections)}");

        var splitSections = SplitSections(@this.GroupLengths, sections).DistinctBy(s => string.Join(" ", s));

        Console.WriteLine($"Split Sections: {string.Join(", ", splitSections.Select(s => string.Join(" ", s)))}");

        return splitSections.Sum(s => GetArrangementCount(@this.GroupLengths, s));
    }

    #region Internal

    private static IEnumerable<string[]> SplitSections(
        int[] groupLengths,
        string[] sections)
    {
        if (groupLengths.Length == sections.Length)
        {
            if (IsValidSections(groupLengths, sections))
            {
                yield return sections;
            }

            yield break;
        }

        if (groupLengths.Sum() > sections.Sum(s => s.Length) - (groupLengths.Length - sections.Length))
        {
            // cannot split without making at least one section too small

            yield break;
        }

        for (var i = 0; i < sections.Length; i++)
        {
            for (var j = sections[i].IndexOf('?'); j != -1; j = sections[i].IndexOf('?', j + 1))
            {
                if (j == 0)
                {
                    var splitSections = new string[sections.Length];

                    Array.Copy(sections, splitSections, sections.Length);

                    splitSections[i] = splitSections[i][1..];

                    foreach (var s in SplitSections(groupLengths, splitSections))
                    {
                        yield return s;
                    }
                }

                if (j == sections[i].Length - 1)
                {
                    var splitSections = new string[sections.Length];

                    Array.Copy(sections, splitSections, sections.Length);

                    splitSections[i] = splitSections[i][..^1];

                    foreach (var s in SplitSections(groupLengths, splitSections))
                    {
                        yield return s;
                    }

                    yield break;
                }

                if (j >= groupLengths[i])
                {
                    var splitSections = new string[sections.Length + 1];

                    if (i > 0)
                    {
                        Array.Copy(sections, splitSections, i);
                    }

                    splitSections[i] = sections[i][..j];

                    splitSections[i + 1] = sections[i][(j + 1)..];

                    if (i < sections.Length - 1)
                    {
                        Array.Copy(sections, i + 1, splitSections, i + 2, sections.Length - i - 1);
                    }

                    foreach (var s in SplitSections(groupLengths, splitSections))
                    {
                        yield return s;
                    }
                }
            }
        }
    }

    private static bool IsValidSections(
        int[] groupLengths,
        string[] sections)
    {
        for (var i = 0; i < groupLengths.Length; i++)
        {
            if (groupLengths[i] > sections[i].Length)
            {
                return false;
            }
        }

        return true;
    }

    private static int GetArrangementCount(
        int[] groupLengths,
        string[] sections) =>
        groupLengths
        .Zip(sections)
        .Select(zip => GetArrangementCount(zip.First, zip.Second))
        .Multiply();

    private static int GetArrangementCount(
        int groupLength,
        string section)
    {
        if (groupLength > section.Length)
        {
            throw new ArgumentException($"invalid section: must be at least {section.Length} long", nameof(section));
        }

        var first = section.IndexOf('#');

        if (first == -1)
        {
            // all positions are unknown

            return section.Length - groupLength + 1;
        }

        var last = section.LastIndexOf('#');

        var unknownLength = groupLength - (last - first + 1);

        if (unknownLength == 0)
        {
            // all positions are known

            return 1;
        }

        return Math.Min(first, unknownLength) + Math.Min(section.Length - last - 1, unknownLength);
    }

    #endregion

    #region Helpers

    [GeneratedRegex("[.]+")]
    private static partial Regex Splitter();

    #endregion
}
