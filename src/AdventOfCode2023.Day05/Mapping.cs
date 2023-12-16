using System.Diagnostics.CodeAnalysis;

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
}

internal sealed record MappingRange(
    long DestinationRangeStart,
    long SourceRangeStart,
    long RangeLength
) :
    IComparable<MappingRange>
{
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
