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
                for(int j = 0; j < i.Stack; j++)
                {
                    total += i.sellToStorePrice();
                }
            }
            return total;
        }

        public static List<Vector2> GetChestLocations(GameLocation location)
        {
            List<Vector2> chestLocs = new List<Vector2>();
            //Loops through each tile at the given location to check if there's a chest
            for (int x = 0; x < location.map.Layers[0].LayerWidth; x++)
            {
                for (int y = 0; y < location.map.Layers[0].LayerHeight; y++)
                {
                    Vector2 temp = new Vector2(x, y);
                    //checks to see if that tile is a valid tile, then check if the object on that tile is a Chest
                    if (location.objects.ContainsKey(temp) && location.Objects[temp] is StardewValley.Objects.Chest)
                        chestLocs.Add(new Vector2(x, y));
                }
            }
            return chestLocs;
        }
    }
}
