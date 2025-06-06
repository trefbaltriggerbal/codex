using LinearCongruentGenerator;
using Xunit;

namespace LinearCongruentGenerator.Tests;

public class LCGValidatorTests
{
    [Theory]
    [InlineData(0, 1, 10)]
    [InlineData(10, 1, 5)]
    public void InvalidMultiplier_Throws(long m, long a, long c)
    {
        Assert.Throws<InvalidMultiplierException>(() => LCGValidator.Validate(m, a, c));
    }

    [Theory]
    [InlineData(1, -1, 10)]
    [InlineData(1, 10, 10)]
    public void InvalidAddition_Throws(long m, long a, long c)
    {
        Assert.Throws<InvalidAdditionException>(() => LCGValidator.Validate(m, a, c));
    }

    [Theory]
    [InlineData(1, 0, 0)]
    [InlineData(1, 0, -1)]
    public void InvalidModulus_Throws(long m, long a, long c)
    {
        Assert.Throws<InvalidModulusException>(() => LCGValidator.Validate(m, a, c));
    }

    [Fact]
    public void AnsiParametersWithSmallModulus_Throws()
    {
        long multiplier = 1103515245;
        long addition = 12345;
        Assert.Throws<InvalidMultiplierException>(() => LCGValidator.Validate(multiplier, addition, 5));
    }
}
