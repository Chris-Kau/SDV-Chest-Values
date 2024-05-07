using StardewValley.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StardewValley;
using StardewModdingAPI;
namespace sdv_chest_values
{
    internal class ChestMethods
    {
        public static long GetTotalValue(StardewValley.Objects.Chest chest)
        {
            long total = 0;
            foreach(Item i in chest.Items)
            {
                for(int j = 0; j < i.Stack; j++)
                {
                    total += i.sellToStorePrice();
                }
            }
            return total;
        }
    }
}
