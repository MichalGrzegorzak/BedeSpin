namespace Bede.Spin.Core.Domain;

public interface IPayoutCalculator
{
    decimal Calculate(decimal stake, List<List<SpinItem>> rowsAndCols);
}

public class PayoutCalculator : IPayoutCalculator
{
    public decimal Calculate(decimal stake, List<List<SpinItem>> rowsAndCols)
    {
        decimal allLinesCoefficiency = 0;

        foreach (var rowLine in rowsAndCols)
        {
            var notWild = rowLine.FirstOrDefault(l => !l.IsWildcard);
            if (notWild == null)
                continue;
                
            bool hasWon = rowLine.All(c => c == notWild);
            allLinesCoefficiency += hasWon ? rowLine.Sum(c => c.Coefficiency) : 0;
        }

        decimal payout = allLinesCoefficiency * stake;
        return payout;
    }
}