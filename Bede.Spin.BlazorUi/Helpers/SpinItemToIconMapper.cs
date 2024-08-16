using Bede.Spin.Core.Domain;
using Bede.Spin.Core.Models;

namespace Bede.Spin.BlazorUi;

public static class SpinItemToIconMapper
{
    public static string[] MapSpinItemsToIcons(this List<SpinItem> spinItems)
    {
        var icons = new List<string>();
        //var items = spinItems.Skip(startIdx).Take(take).ToList();
        foreach (var itm in spinItems)
        {
            var fruitIcon = itm.Fruit switch
            {
                FruitItem.Apple => @"images\apple.png",
                FruitItem.Banana => @"images\banana.png",
                FruitItem.Pineapple => @"images\pineapple.png",
                FruitItem.Wildcard => @"images\wildcard.png",
                _ => throw new ArgumentOutOfRangeException($"Not supported fruit? => {itm.Fruit}")
            };
            icons.Add(fruitIcon);
        }

        return icons.ToArray();
    }
}