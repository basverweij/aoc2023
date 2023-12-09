using AdventOfCode2023.Day02;

var lines = await File.ReadAllLinesAsync("input.txt");

var games = lines.Select(Game.ParseLine);

var puzzle1 = games.Where(IsPossible).Sum(g => g.Id);

Console.WriteLine($"Day 2 - Puzzle 1: {puzzle1}");

var puzzle2 = games.Select(Power).Sum();

Console.WriteLine($"Day 2 - Puzzle 2: {puzzle2}");

static bool IsPossible(
    Game game)
{
    const int maxRed = 12;

    const int maxGreen = 13;

    const int maxBlue = 14;

    return game is { MaxRed: <= maxRed, MaxGreen: <= maxGreen, MaxBlue: <= maxBlue };
}

static int Power(
    Game game)
{
    return game.MaxRed * game.MaxGreen * game.MaxBlue;
}
