using Bede.Spin.Core.Domain;
using Bede.Spin.Core.Models;
using Bede.Spin.Core.Services;
using FluentAssertions;
using NSubstitute;
using Xunit.Abstractions;

namespace Bede.Spin.UnitTests;

public class SlotMachineTests
{
    private readonly ITestOutputHelper _console;

    public SlotMachineTests(ITestOutputHelper console)
    {
        _console = console;
    }

    private readonly Probability[] _allApples = new Probability[] { new(1), new(1), new(1), new(1), new(1), new(1), new(1), new(1), new(1), new(1), new(1), new(1) };
    private readonly Probability[] _noWinners = new Probability[] { new(1), new(50), new(99), new(1), new(50), new(99), new(1), new(50), new(99), new(1), new(50), new(99) };

    [Fact]
    public void Should_WithDraw_Stake_Money_From_User()
    {
        var player = new Player(10);
        
        var generator = Substitute.For<IProbabilitiesGenerator>();
        generator.GenerateArray(12).Returns(_noWinners);

        var slot = new SlotMatchine(generator, new PayoutCalculator());
        var won = slot.Spin(player, 10);
        
        won.Should().Be(0);
        player.Balance.Should().Be(0);
    }
    
    [Fact]
    public void Should_Deposit_Won_Money_To_User()
    {
        var player = new Player(20);
        
        var generator = Substitute.For<IProbabilitiesGenerator>();
        generator.GenerateArray(12).Returns(_allApples);

        var slot = new SlotMatchine(generator, new PayoutCalculator());
        var won = slot.Spin(player, 10);
        
        won.Should().Be(48);
        player.Balance.Should().Be(58);
    }
}