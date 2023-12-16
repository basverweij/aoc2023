using System.Collections.Immutable;

var lines = await File.ReadAllLinesAsync("input.txt");

var seeds = ParseNumbers(lines[0][7..]);

var mappings = ParseMappings(lines[2..]);

var locations = seeds.Select(s => MapSeed(mappings, s));

var puzzle1 = locations.Min();

Console.WriteLine($"Day 5 - Puzzle 1: {puzzle1}");

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
    } while (mappings.TryGetValue(mapping.To, out mapping));

    return value;
}
