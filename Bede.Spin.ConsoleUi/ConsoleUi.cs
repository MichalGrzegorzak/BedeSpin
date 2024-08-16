using Bede.Spin.Core.Domain;
using Bede.Spin.Core.Extensions;
using Bede.Spin.Core.Services;

namespace Bede.Spin.ConsoleUi;

public class ConsoleUi
{
    private readonly IConsoleWrapper _console;

    public ConsoleUi(IConsoleWrapper console)
    {
        _console = console;
    }

    public void Run()
    {
        var player = new Player();
        _console.WriteLine("Please deposit money you would like to play with: ");
        var initialBalance = _console.ReadLine().ParseSafe<decimal>(def: 100);
        player.Deposit(initialBalance);
        
        var slotMatchine = new SlotMatchine(new ProbabilitiesGenerator(), new PayoutCalculator(),4, 3);

        while (player.Balance > 0)
        {
            _console.Write("Enter stake amount: ");
            var stake = _console.ReadLine().ParseSafe<decimal>(def: 10);
            if (stake <= 0)
            {
                _console.WriteLine("Sorry, stake must be > 0");
                continue;
            }

            _console.WriteLine("");
            if (stake > player.Balance)
            {
                _console.WriteLine("Sorry, not enough money to place such bet.");
                continue;
            }
    
            var payout = slotMatchine.Spin(player, stake);
            
            // PRINT SYMBOL LINES
            var gridResults = slotMatchine.SpinResultAsRowsAndColumns;
            foreach (var rowKey in gridResults)
            {
                var symbols = rowKey.Select(l => l.Symbol);
                Console.WriteLine(string.Join("", symbols));
            }
    
            if (payout > 0)
                _console.WriteLine($"You have won: {payout:F1}");
            
            _console.WriteLine($"Current balance is: {player.Balance:F1}");
        }
        _console.WriteLine("Good bye!");
    }
}