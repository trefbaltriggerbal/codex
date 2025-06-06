using LinearCongruentGenerator;
using Xunit;

namespace LinearCongruentGenerator.Tests;

public class CRandComparisonTests
{
    private static long CRand(ref long seed)
    {
        seed = unchecked(1103515245 * seed + 12345) & 0x7fffffff;
        return seed;
    }

    [Fact]
    public void MatchesCRandSequence()
    {
        long seed = 1;
        var rng = new LCGRandomizer(1103515245, 12345, 1L << 31, seed);
        for (int i = 0; i < 10; i++)
        {
            long expected = CRand(ref seed);
            Assert.Equal(expected, rng.Next());
        }
    }
}
