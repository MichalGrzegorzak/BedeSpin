using Bede.Spin.Core.Domain;
using Bede.Spin.Core.Models;
using FluentAssertions;

namespace Bede.Spin.UnitTests;

public class SpinItemTests
{
    [Fact]
    public void IsEqual_Should_Fail()
    {
        var apple1 = new SpinItem("A", 1, FruitItem.Apple);
        var apple2 = new SpinItem("AA", 1, FruitItem.Apple);
        var apple3 = new SpinItem(" A", 1, FruitItem.Apple);
        
        (apple1 == apple2).Should().BeFalse();
        (apple1 == apple3).Should().BeFalse();
    }
    
    [Fact]
    public void Equals_Should_Fail()
    {
        var banana1 = new SpinItem("B", 2, FruitItem.Banana);
        var banana2 = new SpinItem("B2", 2, FruitItem.Banana);
        
        banana1.Equals(banana2).Should().BeFalse();
    }
    
    [Fact]
    public void IsEqual_Should_Pass()
    {
        var apple1 = new SpinItem("A", 1, FruitItem.Apple);
        var apple2 = new SpinItem("A", 1, FruitItem.Apple);
        var apple3 = new SpinItem("A", 2, FruitItem.Apple);
        
        (apple1 == apple2).Should().BeTrue();
        (apple1 == apple3).Should().BeTrue();
    }
    
    [Fact]
    public void Equals_Should_Pass()
    {
        var banana1 = new SpinItem("B", 2, FruitItem.Banana);
        var banana2 = new SpinItem("B", 2, FruitItem.Banana);
        
        banana1.Equals(banana2).Should().BeTrue();
    }
    
    [Fact]
    public void Wildcard_IsEqual_Should_Pass()
    {
        var pine1 = new SpinItem("A", 1, FruitItem.Pineapple);
        var pine2 = new SpinItem("A", 2, FruitItem.Pineapple);
        var wildcard = new SpinItem("*", 0, FruitItem.Wildcard, IsWildcard: true);
        
        (wildcard == pine1).Should().BeTrue();
        (pine2 == wildcard).Should().BeTrue();
    }
}