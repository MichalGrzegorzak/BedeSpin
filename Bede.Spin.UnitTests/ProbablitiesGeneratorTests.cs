using Bede.Spin.Core.Models;
using Bede.Spin.Core.Services;
using FluentAssertions;
using Xunit.Abstractions;

namespace Bede.Spin.UnitTests;

public class ProbablitiesGeneratorTests
{
    private readonly ITestOutputHelper _console;

    public ProbablitiesGeneratorTests(ITestOutputHelper console)
    {
        _console = console;
    }

    [Theory]
    [InlineData(FruitItem.Apple, 45)]
    [InlineData(FruitItem.Banana, 35)]
    [InlineData(FruitItem.Pineapple, 15)]
    [InlineData(FruitItem.Wildcard, 5)]
    public void Verify_Expected_Probabilties(FruitItem fruit, int expected)
    {
        const int tolerance = 1;
        var spinItems = new ProbabilitiesGenerator().GenerateArray(1_000_000).MapToSpinItems();
        var countFruit = spinItems.Count(x => x.Fruit == fruit) /10000;
        
        _console.WriteLine($"{fruit}: {countFruit}");
        countFruit.Should().BeInRange(expected - tolerance, expected + tolerance);
    }
}