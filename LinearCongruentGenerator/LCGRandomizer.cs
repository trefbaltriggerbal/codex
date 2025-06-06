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

        long accMul = 1;
        long accAdd = 0;
        long curMul = _multiplier;
        long curAdd = _addition;
        long step = jumps;

        while (step > 0)
        {
            if ((step & 1) == 1)
            {
                accMul = Mod(accMul * curMul, _modulus);
                accAdd = Mod(accAdd * curMul + curAdd, _modulus);
            }

            curAdd = Mod(curAdd * (curMul + 1), _modulus);
            curMul = Mod(curMul * curMul, _modulus);
            step >>= 1;
        }

        _seed = Mod(accMul * _seed + accAdd, _modulus);
    }

    private static long Mod(long value, long modulus)
    {
        long result = value % modulus;
        return result < 0 ? result + modulus : result;
    }

}
