namespace Bede.Spin.Core.Domain;

public class Player(decimal balance = 0)
{
    public decimal Balance { get; private set; } = balance;

    public void Deposit(decimal amount)
    {
        Guard.Against.NegativeOrZero(amount, nameof(amount));
        Balance += amount;
    }
    public void Withdraw(decimal amount)
    {
        Guard.Against.NegativeOrZero(amount, nameof(amount));
        Balance -= amount;
    }
}