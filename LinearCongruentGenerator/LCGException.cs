namespace LinearCongruentGenerator;

/// <summary>
/// Base class for LCG validation exceptions.
/// </summary>
public abstract class LCGException : Exception
{
    protected LCGException(string message) : base(message)
    {
    }
}

/// <summary>
/// Thrown when the multiplier value is invalid.
/// </summary>
public class InvalidMultiplierException : LCGException
{
    public InvalidMultiplierException(string? message = null)
        : base(message ?? "Invalid multiplier value.")
    {
    }
}

/// <summary>
/// Thrown when the addition value is invalid.
/// </summary>
public class InvalidAdditionException : LCGException
{
    public InvalidAdditionException(string? message = null)
        : base(message ?? "Invalid addition value.")
    {
    }
}

/// <summary>
/// Thrown when the modulus value is invalid.
/// </summary>
public class InvalidModulusException : LCGException
{
    public InvalidModulusException(string? message = null)
        : base(message ?? "Invalid modulus value.")
    {
    }
}
