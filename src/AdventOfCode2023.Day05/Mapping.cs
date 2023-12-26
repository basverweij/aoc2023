internal sealed record Mapping(
    string From,
    string To)
{
    internal SortedSet<MappingRange> Ranges { get; } = [];

    internal long Map(
        long sourceValue)
    {
        foreach (var range in Ranges)
        {
            if (range.TryMap(sourceValue, out var destinationValue))
            {
                return destinationValue;
            }
        }

        return sourceValue;
    }

    internal IEnumerable<Range> MapRange(
        Range sourceRange)
    {
        foreach (var range in Ranges)
        {
            if (sourceRange.Start >= range.SourceRangeEndExclusive)
            {
                continue;
            }

            if (sourceRange.EndExclusive <= range.SourceRangeStart)
            {
                // source range is completely unmapped

                yield return sourceRange;

                yield break;
            }

            if (sourceRange.Start < range.SourceRangeStart)
            {
                // source range is partially unmapped

                var unmappedLength = range.SourceRangeStart - sourceRange.Start;

                yield return new(sourceRange.Start, unmappedLength);

                sourceRange = new(range.SourceRangeStart, sourceRange.Length - unmappedLength);
            }

            if (sourceRange.EndExclusive <= range.SourceRangeEndExclusive)
            {
                // source range is completely mapped

                yield return new(sourceRange.Start - range.SourceRangeStart + range.DestinationRangeStart, sourceRange.Length);

                yield break;
            }

            // source range is partially mapped

            var mappedLength = range.SourceRangeEndExclusive - sourceRange.Start;

            yield return new(sourceRange.Start - range.SourceRangeStart + range.DestinationRangeStart, mappedLength);

            sourceRange = new(sourceRange.Start + mappedLength, sourceRange.Length - mappedLength);
        }

        yield return sourceRange;
    }
}

internal sealed record MappingRange(
    long DestinationRangeStart,
    long SourceRangeStart,
    long RangeLength
) :
    IComparable<MappingRange>
{
    internal long SourceRangeEndExclusive => SourceRangeStart + RangeLength;

    /// <summary>
    /// Sort by SourceRangeStart.
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public int CompareTo(
        MappingRange? other)
    {
        if (other is null ||
            SourceRangeStart == other.SourceRangeStart)
        {
            return 0;
        }

        return SourceRangeStart < other.SourceRangeStart ? -1 : 1;
    }

    internal bool TryMap(
        long sourceValue,
        out long destinationValue)
    {
        if (sourceValue < SourceRangeStart ||
            sourceValue >= SourceRangeStart + RangeLength)
        {
            destinationValue = 0;

            return false;
        }

        destinationValue = sourceValue - SourceRangeStart + DestinationRangeStart;

        return true;
    }
};

internal sealed record Range(
    long Start,
    long Length)
{
    internal long EndExclusive => Start + Length;
}
