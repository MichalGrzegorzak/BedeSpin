using Bede.Spin.Core.Domain;
using Bede.Spin.Core.Models;

namespace Bede.Spin.Core.Services;

public static class ProbabilityMapper
{
    public static List<SpinItem> MapToSpinItems(this Probability[] probabilitiesArray)
    {
        var fruits = new List<SpinItem>();
        foreach (Probability prob in probabilitiesArray)
        {
            var fruit = prob.Value switch
            {
                >= 1 and <= 45 => SpinItem.Create(FruitItem.Apple),
                > 45 and <= 80 => SpinItem.Create(FruitItem.Banana),
                > 80 and <= 95 => SpinItem.Create(FruitItem.Pineapple),
                > 95 and <= 100 => SpinItem.Create(FruitItem.Wildcard),
                _ => throw new ArgumentOutOfRangeException()
            };
            fruits.Add(fruit);
        }

        return fruits;
    }
}