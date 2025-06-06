namespace LinearCongruentGenerator;

/// <summary>
/// Simple Linear Congruential Generator implementation.
/// </summary>
public class LCGRandomizer
{
    private readonly long _multiplier;
    private readonly long _addition;
    private readonly long _modulus;
    private long _seed;

    public LCGRandomizer(long multiplier, long addition, long modulus, long seed = 1)
    {
        _multiplier = multiplier;
        _addition = addition;
        _modulus = modulus;
        _seed = Mod(seed, _modulus);
    }

    /// <summary>
    /// Returns the next random number in the sequence.
    /// </summary>
    public long Next()
    {
        _seed = Mod(_multiplier * _seed + _addition, _modulus);
        return _seed;
    }

    /// <summary>
    /// Sets the current seed value.
    /// </summary>
    public void SetSeed(long seed)
    {
        _seed = Mod(seed, _modulus);
    }

    /// <summary>
    /// Jumps ahead by the specified number of iterations using a fast method.
    /// </summary>
    public void Jump(long jumps)
    {
        if (jumps <= 0)
            return;

        long a = ModPow(_multiplier, jumps, _modulus);

        long b;
        if (_multiplier == 1)
        {
            b = Mod(jumps * _addition, _modulus);
        }
        else
        {
            long inv = ModInverse(_multiplier - 1, _modulus);
            b = Mod((a - 1) * inv, _modulus);
            b = Mod(b * _addition, _modulus);
        }

        _seed = Mod(a * _seed + b, _modulus);
    }

    private static long Mod(long value, long modulus)
    {
        long result = value % modulus;
        return result < 0 ? result + modulus : result;
    }

    private static long ModPow(long value, long exponent, long modulus)
    {
        long result = 1;
        long baseVal = Mod(value, modulus);
        long exp = exponent;

        while (exp > 0)
        {
            if ((exp & 1) == 1)
            {
                result = Mod(result * baseVal, modulus);
            }
            baseVal = Mod(baseVal * baseVal, modulus);
            exp >>= 1;
        }

        return result;
    }

    private static long ModInverse(long value, long modulus)
    {
        long t = 0, newT = 1;
        long r = modulus, newR = Mod(value, modulus);

        while (newR != 0)
        {
            long quotient = r / newR;
            (t, newT) = (newT, t - quotient * newT);
            (r, newR) = (newR, r - quotient * newR);
        }

        if (r > 1)
            throw new InvalidOperationException("Value is not invertible");

        if (t < 0)
            t += modulus;

        return t;
    }
}
