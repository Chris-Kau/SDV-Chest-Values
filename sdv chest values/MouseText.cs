using StardewValley.Menus;
using StardewValley.Objects;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StardewModdingAPI.Events;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using xTile.Tiles;

namespace sdv_chest_values
{
    internal class MouseText
    {
        private static Dictionary<int, List<int>> tpos = new()
                    {
                        { 1, new List<int> {-25, 40}},
                        { 2, new List<int> {-25, -30}},
                        { 3, new List<int> {0, 2}},
                        { 4, new List<int> {40, 2}}
                    };
        public static void DrawText(RenderedActiveMenuEventArgs e, Vector2 mousePos, int textpos, StardewValley.Objects.Chest chest, Vector2 chestTilePos)
        {
            string total_value = "";
            if (ChestMethods.ChestValues == null)
            {
                total_value = "Chest data has not loaded";
            }
            else
            {
                if (ChestMethods.ChestValues.TryGetValue(chestTilePos, out long value))
                    total_value = value.ToString();
            }



            //only check to see if the player wants the text on the left of the cursor so we can take into account how many digits are in the total_value so the text doesnt overlap the cursor.
            if (textpos == 3)
            {
                //draws the price as a string near the cursor
                e.SpriteBatch.DrawString(Game1.smallFont, $"{total_value}", mousePos + new Vector2((total_value.Length + 2) * -10 + tpos[textpos][0], tpos[textpos][1]), Color.White);
                //draws the coin icon to the left of the price string
                ClickableTextureComponent coinsIcon = new ClickableTextureComponent(
                    new Rectangle((int)mousePos.X + ((total_value.Length + 4) * -10) + tpos[textpos][0] - 5, (int)mousePos.Y + tpos[textpos][1] + 3, 15, 14),
                    Game1.mouseCursors,
                    new Rectangle(280, 412, 15, 14),
                    1.5f
                    );
                coinsIcon.draw(Game1.spriteBatch);
            }
            else
            {
                //draws the price as a string near the cursor
                e.SpriteBatch.DrawString(Game1.smallFont, $"{total_value}", mousePos + new Vector2(tpos[textpos][0] + 25, tpos[textpos][1]), Color.White);
                //draws the coin icon to the left of the price string
                ClickableTextureComponent coinsIcon = new ClickableTextureComponent(
                    new Rectangle((int)mousePos.X + tpos[textpos][0], (int)mousePos.Y + tpos[textpos][1] + 3, 15, 14),
                    Game1.mouseCursors,
                    new Rectangle(280, 412, 15, 14),
                    1.5f
                    );
                coinsIcon.draw(Game1.spriteBatch);
            }

        }
    }
}
