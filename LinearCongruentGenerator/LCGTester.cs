namespace LinearCongruentGenerator;

/// <summary>
/// Helper methods for manual testing of <see cref="LCGRandomizer"/> instances.
/// </summary>
public static class LCGTester
{
    /// <summary>
    /// Returns the next <paramref name="count"/> values from the given generator.
    /// The generator state is restored afterwards.
    /// </summary>
    public static List<long> SampleSequence(LCGRandomizer rng, int count)
    {
        long seed = rng.Seed;
        var values = new List<long>();
        for (int i = 0; i < count; i++)
            values.Add(rng.Next());
        rng.SetSeed(seed);
        return values;
    }

    /// <summary>
    /// Compares the next <paramref name="count"/> numbers produced by two generators.
    /// The generators are reset to their original seeds after comparison.
    /// </summary>
    public static bool CompareSequences(LCGRandomizer first, LCGRandomizer second, int count)
    {
        long seed1 = first.Seed;
        long seed2 = second.Seed;
        bool equal = true;
        for (int i = 0; i < count; i++)
        {
            if (first.Next() != second.Next())
            {
                equal = false;
                break;
            }
        }
        first.SetSeed(seed1);
        second.SetSeed(seed2);
        return equal;
    }

    /// <summary>
    /// Verifies that using <see cref="LCGRandomizer.Jump"/> yields the same state
    /// as calling <see cref="LCGRandomizer.Next"/> repeatedly.
    /// A second generator with the same parameters must be provided.
    /// </summary>
    public static bool ValidateJump(LCGRandomizer rng, LCGRandomizer reference, long steps)
    {
        long seed = rng.Seed;
        reference.SetSeed(seed);

        if (steps >= 0)
        {
            for (long i = 0; i < steps; i++)
                reference.Next();
        }
        else
        {
            for (long i = 0; i < -steps; i++)
                reference.Jump(-1);
        }

        long expected = reference.Seed;
        rng.Jump(steps);
        bool ok = rng.Seed == expected;

        rng.SetSeed(seed);
        reference.SetSeed(seed);
        return ok;
    }
}
