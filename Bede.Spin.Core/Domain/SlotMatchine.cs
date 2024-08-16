using Bede.Spin.Core.Extensions;
using Bede.Spin.Core.Services;

namespace Bede.Spin.Core.Domain;

public class SlotMatchine(IProbabilitiesGenerator generator, IPayoutCalculator payoutCalculator, int noOfRows = 4, int noOfColumns = 3)
{
    public List<List<SpinItem>> SpinResultAsRowsAndColumns { get; private set; }

    public decimal Spin(Player player, decimal stake)
    {
        Guard.Against.NegativeOrZero(stake, nameof(stake));
        
        player.Withdraw(stake);
        
        //generate random results
        var probabilitiesArray = generator.GenerateArray(noOfColumns * noOfRows);
        var spinResults = probabilitiesArray.MapToSpinItems();
        
        //transform results as rows & cols grid
        SpinResultAsRowsAndColumns = spinResults.ChopListIntoRowsAndColumns(noOfRows, noOfColumns);
        
        //calculate payout
        var payout = payoutCalculator.Calculate(stake, SpinResultAsRowsAndColumns);
        
        if(payout > 0)
            player.Deposit(payout);
        
        return payout;
    }

    
}