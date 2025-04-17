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
        public static Dictionary<Vector2, long> ChestValues = new();

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
        public static void UpdateAllChestValues(GameLocation location)
        {
            ChestValues.Clear();
            foreach (var pair in location.Objects.Pairs)
            {
                if(pair.Value is StardewValley.Objects.Chest chest)
                {
                    var tv = GetTotalValue(chest.Items);
                    ChestValues[pair.Key] = tv;
                }
            }
        }

        public static void UpdateChestValue(StardewValley.Objects.Chest chest)
        {
            var tv = GetTotalValue(chest.Items);
            ChestValues[chest.TileLocation] = tv;
        }

        public static void RemoveChestValue(StardewValley.Objects.Chest chest)
        {
            ChestValues.Remove(chest.TileLocation);
        }

        public static void AddChestValue(StardewValley.Objects.Chest chest)
        {
            ChestValues[chest.TileLocation] = GetTotalValue(chest.Items);
        }
    }
}
