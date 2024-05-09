using System;
using System.Security.AccessControl;
using Microsoft.Xna.Framework;
using sdv_chest_values;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;
namespace sdv_chest_values
{
    internal class ChestMethods
    {
        public static long GetTotalValue(StardewValley.Objects.Chest chest)
        {
            long total = 0;
            foreach(Item i in chest.Items)
            {
                if(i.sellToStorePrice() != 0)
                    for(int j = 0; j < i.Stack; j++)
                    {
                        total += i.sellToStorePrice();
                    }
            }
            return total;
        }
    }
}
