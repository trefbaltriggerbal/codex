namespace LinearCongruentGenerator;

/// <summary>
/// Provides validation helpers for LCG parameters.
/// </summary>
public static class LCGValidator
{
    /// <summary>
    /// Validates multiplier, addition and modulus values.
    /// </summary>
    public static void Validate(long multiplier, long addition, long modulus)
    {
        if (modulus <= 0)
            throw new InvalidModulusException("Modulus must be greater than 0.");
        if (multiplier <= 0 || multiplier >= modulus)
            throw new InvalidMultiplierException("Multiplier must be > 0 and < modulus.");
        if (addition < 0 || addition >= modulus)
            throw new InvalidAdditionException("Addition must be >= 0 and < modulus.");
    }
}
