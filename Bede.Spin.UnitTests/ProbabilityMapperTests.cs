using Bede.Spin.Core.Domain;
using Bede.Spin.Core.Models;
using Bede.Spin.Core.Services;
using FluentAssertions;

namespace Bede.Spin.UnitTests;

public class ProbabilityMapperTests
{
    [Theory]
    [InlineData(0)]
    [InlineData(101)]
    public void Create_Probability_Should_Fail_OutOfRange(int probability)
    {
        Action act = () => new Probability(probability);

        act.Should().Throw<ArgumentOutOfRangeException>()
            .WithMessage("Input value was out of range (Parameter 'value')");
    }
    
    [Theory]
    [InlineData(1)]
    [InlineData(100)]
    [InlineData(55)]
    public void Create_Probability_Should_Pass(int probability)
    {
        Action act = () => new Probability(probability);

        act.Should().NotThrow();
    }
    
    [Theory]
    [InlineData(1, FruitItem.Apple)]
    [InlineData(22, FruitItem.Apple)]
    [InlineData(45, FruitItem.Apple)]
    [InlineData(46, FruitItem.Banana)]
    [InlineData(66, FruitItem.Banana)]
    [InlineData(80, FruitItem.Banana)]
    [InlineData(81, FruitItem.Pineapple)]
    [InlineData(88, FruitItem.Pineapple)]
    [InlineData(95, FruitItem.Pineapple)]
    [InlineData(96, FruitItem.Wildcard)]
    [InlineData(98, FruitItem.Wildcard)]
    [InlineData(100, FruitItem.Wildcard)]
    public void Probability_To_FruitMapping_Should_Pass(int probability, FruitItem expected)
    {
        var prob = new Probability(probability);
        var spinItems = (new[] { prob }).MapToSpinItems();
        
        spinItems.First().Fruit.Should().Be(expected);
    }
}