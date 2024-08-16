using Bede.Spin.Core.Domain;
using Bede.Spin.Core.Models;
using FluentAssertions;

namespace Bede.Spin.UnitTests;

public class PayoutCalculatorTests
{
    [Fact]
    public void Verify_Payout_Calculations_Simple()
    {
        var rowsAndCols = new List<List<SpinItem>>()
        {
            new() { SpinItem.Create(FruitItem.Apple), SpinItem.Create(FruitItem.Apple), SpinItem.Create(FruitItem.Apple) },
            new() { SpinItem.Create(FruitItem.Banana), SpinItem.Create(FruitItem.Banana), SpinItem.Create(FruitItem.Banana) },
            new() { SpinItem.Create(FruitItem.Pineapple), SpinItem.Create(FruitItem.Pineapple), SpinItem.Create(FruitItem.Pineapple) },
            new() { SpinItem.Create(FruitItem.Wildcard), SpinItem.Create(FruitItem.Wildcard), SpinItem.Create(FruitItem.Wildcard) },
        };
        
        var payout = new PayoutCalculator().Calculate(100, rowsAndCols);
        
        payout.Should().Be(540);
    }
    
    [Fact]
    public void Verify_Payout_Calculations_Simple_zero()
    {
        var rowsAndCols = new List<List<SpinItem>>()
        {
            new() { SpinItem.Create(FruitItem.Apple), SpinItem.Create(FruitItem.Apple), SpinItem.Create(FruitItem.Banana) },
            new() { SpinItem.Create(FruitItem.Banana), SpinItem.Create(FruitItem.Banana), SpinItem.Create(FruitItem.Apple) },
            new() { SpinItem.Create(FruitItem.Wildcard), SpinItem.Create(FruitItem.Pineapple), SpinItem.Create(FruitItem.Apple) },
            new() { SpinItem.Create(FruitItem.Pineapple), SpinItem.Create(FruitItem.Wildcard), SpinItem.Create(FruitItem.Apple) },
            new() { SpinItem.Create(FruitItem.Wildcard), SpinItem.Create(FruitItem.Wildcard), SpinItem.Create(FruitItem.Wildcard) },
        };
        
        var payout = new PayoutCalculator().Calculate(100, rowsAndCols);
        
        payout.Should().Be(0);
    }
    
    [Fact]
    public void Verify_Payout_Calculations_Single_Wildcards()
    {
        var rowsAndCols = new List<List<SpinItem>>()
        {
            new() { SpinItem.Create(FruitItem.Wildcard), SpinItem.Create(FruitItem.Apple), SpinItem.Create(FruitItem.Apple) },
            new() { SpinItem.Create(FruitItem.Wildcard), SpinItem.Create(FruitItem.Banana), SpinItem.Create(FruitItem.Banana) },
            new() { SpinItem.Create(FruitItem.Wildcard), SpinItem.Create(FruitItem.Pineapple), SpinItem.Create(FruitItem.Pineapple) },
            new() { SpinItem.Create(FruitItem.Wildcard), SpinItem.Create(FruitItem.Wildcard), SpinItem.Create(FruitItem.Wildcard) },
        };
        
        var payout = new PayoutCalculator().Calculate(10, rowsAndCols);
        
        payout.Should().Be(36);
    }
    
    [Fact]
    public void Verify_Payout_Calculations_Double_Wildcards()
    {
        var rowsAndCols = new List<List<SpinItem>>()
        {
            new() { SpinItem.Create(FruitItem.Wildcard), SpinItem.Create(FruitItem.Wildcard), SpinItem.Create(FruitItem.Apple) },
            new() { SpinItem.Create(FruitItem.Wildcard), SpinItem.Create(FruitItem.Wildcard), SpinItem.Create(FruitItem.Banana) },
            new() { SpinItem.Create(FruitItem.Wildcard), SpinItem.Create(FruitItem.Wildcard), SpinItem.Create(FruitItem.Pineapple) },
            new() { SpinItem.Create(FruitItem.Wildcard), SpinItem.Create(FruitItem.Wildcard), SpinItem.Create(FruitItem.Wildcard) },
        };
        
        var payout = new PayoutCalculator().Calculate(10, rowsAndCols);
        
        payout.Should().Be(18);
    }
    
    [Fact]
    public void Verify_Payout_Calculations_Only_Wildcards()
    {
        var rowsAndCols = new List<List<SpinItem>>()
        {
            new() { SpinItem.Create(FruitItem.Wildcard), SpinItem.Create(FruitItem.Wildcard), SpinItem.Create(FruitItem.Wildcard) },
            new() { SpinItem.Create(FruitItem.Wildcard), SpinItem.Create(FruitItem.Wildcard), SpinItem.Create(FruitItem.Wildcard) },
            new() { SpinItem.Create(FruitItem.Wildcard), SpinItem.Create(FruitItem.Wildcard), SpinItem.Create(FruitItem.Wildcard) },
            new() { SpinItem.Create(FruitItem.Wildcard), SpinItem.Create(FruitItem.Wildcard), SpinItem.Create(FruitItem.Wildcard) },
        };
        
        var payout = new PayoutCalculator().Calculate(1000, rowsAndCols);
        
        payout.Should().Be(0);
    }
    
    [Fact]
    public void Verify_Payout_Calculations_Simple_2x2()
    {
        var rowsAndCols = new List<List<SpinItem>>()
        {
            new() { SpinItem.Create(FruitItem.Apple), SpinItem.Create(FruitItem.Apple) },
            new() { SpinItem.Create(FruitItem.Banana), SpinItem.Create(FruitItem.Banana) },
        };
        
        var payout = new PayoutCalculator().Calculate(10, rowsAndCols);
        
        payout.Should().Be(20);
    }
    
    [Fact]
    public void Verify_Payout_Calculations_Simple_2x1()
    {
        var rowsAndCols = new List<List<SpinItem>>()
        {
            new() { SpinItem.Create(FruitItem.Apple), SpinItem.Create(FruitItem.Wildcard) },
        };
        
        var payout = new PayoutCalculator().Calculate(10, rowsAndCols);
        
        payout.Should().Be(4);
    }
    
    [Fact]
    public void Verify_Payout_Calculations_Simple_1x4()
    {
        var rowsAndCols = new List<List<SpinItem>>()
        {
            new() { SpinItem.Create(FruitItem.Apple), SpinItem.Create(FruitItem.Wildcard), SpinItem.Create(FruitItem.Wildcard), SpinItem.Create(FruitItem.Wildcard) },
        };
        
        var payout = new PayoutCalculator().Calculate(10, rowsAndCols);
        
        payout.Should().Be(4);
    }
    
    [Fact]
    public void Verify_Payout_Calculations_Simple_1x4_zero()
    {
        var rowsAndCols = new List<List<SpinItem>>()
        {
            new() { SpinItem.Create(FruitItem.Apple), SpinItem.Create(FruitItem.Wildcard), SpinItem.Create(FruitItem.Banana), SpinItem.Create(FruitItem.Wildcard) },
        };
        
        var payout = new PayoutCalculator().Calculate(10, rowsAndCols);
        
        payout.Should().Be(0);
    }
}