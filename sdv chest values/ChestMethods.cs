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
using StardewValley.Objects;
using StardewValley.Buildings;
namespace sdv_chest_values
{
    internal class ChestMethods
    {
        public static Dictionary<Vector2, long> ChestValues = new();

        private static long GetTotalValue(StardewValley.Objects.Chest chest)
        {
            long total = 0;
            if (chest.Name == "Junimo Chest")
            {
                foreach (Item i in Game1.player.team.GetOrCreateGlobalInventory("JunimoChests"))
                {
                    if (i.sellToStorePrice() != 0)
                        total += i.sellToStorePrice() * i.Stack;
                }
            }
            else
            {
                foreach (Item i in chest.Items)
                {
                    if (i.sellToStorePrice() != 0)
                        total += i.sellToStorePrice() * i.Stack;
                }

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
                    long tv = GetTotalValue(chest);
                    ChestValues[pair.Key] = tv;
                }
            }
            foreach(var junimoHut in Game1.getFarm().buildings.Where(x=>x is JunimoHut).Select(x=>(JunimoHut)x))
            {
                long tv = GetTotalValue(junimoHut.GetOutputChest());
                ChestValues[new Vector2(junimoHut.tileX.Value + 1, junimoHut.tileY.Value + 1)] = tv;
            }
        }

        public static void UpdateChestValue(StardewValley.Objects.Chest chest)
        {
            long tv = GetTotalValue(chest);
            ChestValues[chest.TileLocation] = tv;
        }

        public static void RemoveChestValue(StardewValley.Objects.Chest chest)
        {
            ChestValues.Remove(chest.TileLocation);
        }

        public static void AddChestValue(StardewValley.Objects.Chest chest)
        {
            long tv = GetTotalValue(chest);
            ChestValues[chest.TileLocation] = tv;
        }
    }
}
