using LinearCongruentGenerator;
using System.Collections.Generic;
using Xunit;

namespace LinearCongruentGenerator.Tests;

public class LCGRandomizerTests
{
    [Fact]
    public void JumpForwardAndBackward()
    {
        long m = 1103515245;
        long a = 12345;
        long c = 1L << 31;
        var rng1 = new LCGRandomizer(m, a, c, 1);
        var values = new List<long>();
        for (int i = 0; i < 5; i++)
            values.Add(rng1.Next());

        var rng2 = new LCGRandomizer(m, a, c, 1);
        rng2.Jump(5);
        Assert.Equal(rng1.Seed, rng2.Seed);

        rng2.Jump(-1);
        Assert.Equal(values[3], rng2.Seed);
    }

    [Fact]
    public void JumpNegativeValues_MoveBackwards()
    {
        long m = 1103515245;
        long a = 12345;
        long c = 1L << 31;

        var rng1 = new LCGRandomizer(m, a, c, 1);
        var values = new List<long>();
        for (int i = 0; i < 7; i++)
            values.Add(rng1.Next());

        var rng2 = new LCGRandomizer(m, a, c, 1);
        rng2.Jump(7);
        rng2.Jump(-3);

        Assert.Equal(values[3], rng2.Seed);
    }

    [Fact]
    public void SetSeed_ResetsSequence()
    {
        long m = 1103515245;
        long a = 12345;
        long c = 1L << 31;

        var rng1 = new LCGRandomizer(m, a, c, 1);
        rng1.SetSeed(10);

        var rng2 = new LCGRandomizer(m, a, c, 10);

        for (int i = 0; i < 5; i++)
            Assert.Equal(rng2.Next(), rng1.Next());
    }
}
