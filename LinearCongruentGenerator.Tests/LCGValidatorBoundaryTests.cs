using LinearCongruentGenerator;
using Xunit;

namespace LinearCongruentGenerator.Tests;

public class LCGValidatorBoundaryTests
{
    [Theory]
    [InlineData(5, 1, 5)]
    [InlineData(6, 1, 5)]
    public void MultiplierAtLeastModulus_Throws(long m, long a, long c)
    {
        Assert.Throws<InvalidMultiplierException>(() => LCGValidator.Validate(m, a, c));
    }

    [Theory]
    [InlineData(1, 5, 5)]
    [InlineData(1, 6, 5)]
    public void AdditionAtLeastModulus_Throws(long m, long a, long c)
    {
        Assert.Throws<InvalidAdditionException>(() => LCGValidator.Validate(m, a, c));
    }

    [Theory]
    [InlineData(1, 0, 0)]
    [InlineData(1, 0, -5)]
    public void ModulusNonPositive_Throws(long m, long a, long c)
    {
        Assert.Throws<InvalidModulusException>(() => LCGValidator.Validate(m, a, c));
    }
}
