using System.Collections.Immutable;

var lines = await File.ReadAllLinesAsync("input.txt");

var seeds = ParseNumbers(lines[0][7..]);

var mappings = ParseMappings(lines[2..]);

var locations = seeds.Select(s => MapSeed(mappings, s));

var puzzle1 = locations.Min();

Console.WriteLine($"Day 5 - Puzzle 1: {puzzle1}");

var seedRanges = seeds.Chunk(2).Select(c => new Range(c[0], c[1])).ToImmutableArray();

var locationRanges = seedRanges.SelectMany(r => MapSeedRange(mappings, r));

var puzzle2 = locationRanges.Min(r => r.Start);

Console.WriteLine($"Day 5 - Puzzle 2: {puzzle2}");

static ImmutableArray<long> ParseNumbers(
    string value) =>
    value
        .Split(' ')
        .Select(long.Parse)
        .ToImmutableArray();

static Dictionary<string, Mapping> ParseMappings(
    string[] lines)
{
    var mappings = new Dictionary<string, Mapping>();

    Mapping? mapping = null;

    foreach (var line in lines)
    {
        if (mapping is null)
        {
            var parts = line[..^5].Split('-');

            mapping = new(parts[0], parts[2]);

            mappings[mapping.From] = mapping;

            continue;
        }

        if (line.Length == 0)
        {
            mapping = null;

            continue;
        }

        var range = ParseNumbers(line);

        mapping.Ranges.Add(new(range[0], range[1], range[2]));
    }

    return mappings;
}

static long MapSeed(
    Dictionary<string, Mapping> mappings,
    long value)
{
    var mapping = mappings["seed"];

    do
    {
        value = mapping.Map(value);
    }
    while (mappings.TryGetValue(mapping.To, out mapping));

    return value;
}

static IEnumerable<Range> MapSeedRange(
    Dictionary<string, Mapping> mappings,
    Range range)
{
    var ranges = ImmutableArray.Create(range);

    var mapping = mappings["seed"];

    do
    {
        ranges = ranges.SelectMany(r => mapping.MapRange(r)).ToImmutableArray();
    }
    while (mappings.TryGetValue(mapping.To, out mapping));

    return ranges;
}
