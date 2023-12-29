using System.Numerics;

namespace AdventOfCode2023.Day12;

internal static class MathExtensions
{
    internal static T Multiply<T>(
        this IEnumerable<T> @this)
        where T : IMultiplicativeIdentity<T, T>, IMultiplyOperators<T, T, T> =>
        @this.Aggregate(
            T.MultiplicativeIdentity,
            (a, v) => a * v);
}
