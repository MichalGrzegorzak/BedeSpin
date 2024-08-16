using Bede.Spin.Core.Models;

namespace Bede.Spin.Core.Domain;

public record SpinItem(string Symbol, decimal Coefficiency, FruitItem Fruit, bool IsWildcard = false)
{
    public virtual bool Equals(SpinItem? other)
    {
        if (this is null || other is null)
            return false;
        if (IsWildcard || other.IsWildcard)
            return true;

        return Symbol == other.Symbol;
    }
    
    public static SpinItem Create(FruitItem fruit) => fruit switch
    {
        FruitItem.Apple => new SpinItem("A", 0.4m, FruitItem.Apple),
        FruitItem.Banana => new SpinItem("B", 0.6m, FruitItem.Banana),
        FruitItem.Pineapple => new SpinItem("P", 0.8m, FruitItem.Pineapple),
        FruitItem.Wildcard => new SpinItem("*", 0m, FruitItem.Wildcard, IsWildcard: true)
    };
    
}