using System.Security.Cryptography;

namespace Bede.Spin.Core.Domain;

public struct Probability
{
    public Probability(int value)
    {
        Guard.Against.OutOfRange(value, nameof(value), 1, 100);
        Value = value;
    }

    public int Value { get; }
    
    public static Probability Random()
    {
        return new Probability(RandomNumberGenerator.GetInt32(1, 101));
    }
}