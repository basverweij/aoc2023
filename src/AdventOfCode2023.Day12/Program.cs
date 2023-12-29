using AdventOfCode2023.Day12;

var lines = await File.ReadAllLinesAsync("input.txt");

lines = [
    "???.### 1,1,3",
    ".??..??...?##. 1,1,3",
    "?#?#?#?#?#?#?#? 1,3,1,6",
    "????.#...#... 4,1,1",
    "????.######..#####. 1,6,5",
    "?###???????? 3,2,1",
];

var records = lines.Select(Record.Parse).ToArray();

foreach (var record in records)
{
    Console.WriteLine($"> {record.GetArrangementCount()}\n");
}
