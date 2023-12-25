namespace AdventOfCode2023.Day10;

[Flags]
internal enum Connections
{
    None = 0,

    North = 1,

    East = 2,

    South = 4,

    West = 8,

    All = North | East | South | West,
}

internal static class ConnectionsExtensions
{
    internal static Connections GetConnections(
        this char @this) =>
        _connections.TryGetValue(@this, out var connections) ?
            connections :
            throw new ArgumentException($"invalid grid character: '{@this}'");

    private static readonly Dictionary<char, Connections> _connections = new()
    {
        ['|'] = Connections.North | Connections.South,
        ['-'] = Connections.East | Connections.West,
        ['L'] = Connections.North | Connections.East,
        ['J'] = Connections.North | Connections.West,
        ['7'] = Connections.South | Connections.West,
        ['F'] = Connections.South | Connections.East,
        ['S'] = Connections.All,
        ['.'] = Connections.None,
    };
}
