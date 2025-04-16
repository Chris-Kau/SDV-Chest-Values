using System;
using System.Security.AccessControl;
using Microsoft.Xna.Framework;
using sdv_chest_values;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;
using System.Linq;
using System.Threading.Tasks;
namespace sdv_chest_values
{
    internal class ChestMethods
    {
        public static Dictionary<Vector2, long>? ChestValues;

        private static long GetTotalValue(IEnumerable<Item> Items)
        {
            long total = 0;
            foreach (Item i in Items)
            {
                if (i.sellToStorePrice() != 0)
                    total += i.sellToStorePrice() * i.Stack;
            }
            return total;
        }

        /// <summary>
        /// Populates the Chests Dictionary with all Chests and their values on the given location
        /// </summary>
        /// <param name="location"></param>
        public static Dictionary<Vector2, long> UpdateAllChestValues(GameLocation location)
        {
            Dictionary<Vector2, long> ChestValues = new();
            foreach (var pair in location.Objects.Pairs)
            {
                if(pair.Value is StardewValley.Objects.Chest chest)
                {
                    var tv = GetTotalValue(chest.Items);
                    ChestValues[pair.Key] = tv;
                    //Because the chest hitbox for mouse hovering is technically 2 tiles tall, we have to take that into account
                    ChestValues[pair.Key + new Vector2(0, 1)] = tv;
                }
            }
            return ChestValues;
        }
    }
}
