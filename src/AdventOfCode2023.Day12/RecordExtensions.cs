namespace AdventOfCode2023.Day12;

internal static class RecordExtensions
{
    internal static int GetArrangementCount(
        this Record @this)
    {
        var springs = @this.Springs.TrimEnd('.');

        var sections = new Range[@this.GroupLengths.Length];

        var sectionCount = springs.AsSpan().Split(sections, '.', StringSplitOptions.RemoveEmptyEntries);

        Console.WriteLine($"{@this.Springs}: {string.Join(" ", @this.GroupLengths)} -> {string.Join(" ", sections)}");

        var splitSections = @this
            .SplitSections(sections[..sectionCount])
            .DistinctBy(s => string.Join(" ", s))
            .ToArray();

        Console.WriteLine($"Split Sections: {string.Join(", ", splitSections.Select(s => string.Join(" ", s)))}");

        return splitSections.Sum(s => @this.GetArrangementCount(s));
    }

    #region Internal

    private static IEnumerable<Range[]> SplitSections(
        this Record @this,
        Range[] sections)
    {
        for (var i = 0; i < sections.Length; i++)
        {
            if (@this.GroupLengths[i] > sections[i].GetOffsetAndLength(@this.Springs.Length).Length)
            {
                yield break;
            }
        }

        if (@this.GroupLengths.Length == sections.Length)
        {
            yield return sections;

            yield break;
        }

        Console.WriteLine($"Splitting Sections: {@this.Springs}: {string.Join(" ", @this.GroupLengths)} -> {string.Join(", ", sections.Select(s => string.Join(" ", s)))}");

        for (var i = 0; i < sections.Length; i++)
        {
            for (var j = @this.Springs[sections[i]].IndexOf('?', 1); j != -1; j = @this.Springs[sections[i]].IndexOf('?', j + 1))
            {
                if (j == sections[i].GetOffsetAndLength(@this.Springs.Length).Length - 1 ||
                    j < @this.GroupLengths[i])
                {
                    continue;
                }

                var splitSections = new Range[sections.Length + 1];

                if (i > 0)
                {
                    Array.Copy(sections, splitSections, i);
                }

                splitSections[i] = new Range(sections[i].Start, sections[i].Start.Value + j);

                splitSections[i + 1] = new Range(sections[i].Start.Value + j + 1, sections[i].End);

                if (i < sections.Length - 1)
                {
                    Array.Copy(sections, i + 1, splitSections, i + 2, sections.Length - i - 1);
                }

                foreach (var s in @this.SplitSections(splitSections))
                {
                    yield return s;
                }
            }
        }
    }

    private static int GetArrangementCount(
        this Record @this,
        Range[] sections) =>
        sections
            .Select((s, g) => @this.GetArrangementCount(g, s))
            .Multiply();

    private static int GetArrangementCount(
        this Record @this,
        int group,
        Range section)
    {
        var (_, sectionLength) = section.GetOffsetAndLength(@this.Springs.Length);

        if (@this.GroupLengths[group] > sectionLength)
        {
            throw new ArgumentException($"invalid section: must be at least {@this.GroupLengths[group]} long, but was {sectionLength} long", nameof(section));
        }

        var first = @this.Springs[section].IndexOf('#');

        if (first == -1)
        {
            // all positions are unknown

            return sectionLength - @this.GroupLengths[group] + 1;
        }

        var last = @this.Springs[section].LastIndexOf('#');

        var unknownLength = @this.GroupLengths[group] - (last - first + 1);

        if (unknownLength == 0)
        {
            // all positions are known

            return 1;
        }

        return Math.Min(first, unknownLength) + Math.Min(sectionLength - last - 1, unknownLength);
    }

    #endregion
}
