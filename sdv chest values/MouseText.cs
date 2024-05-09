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

namespace sdv_chest_values
{
    internal class MouseText
    {
        public static void DrawText(RenderedActiveMenuEventArgs e, Vector2 mousePos, int xOffset, int yOffset, StardewValley.Objects.Chest chest)
        {
            //draws the price as a string underneath the cursor
            e.SpriteBatch.DrawString(Game1.smallFont, $"{ChestMethods.GetTotalValue(chest)}", mousePos + new Vector2(0, yOffset), Color.White);
            //draws the coin icon to the left of the price string
            ClickableTextureComponent coinsIcon = new ClickableTextureComponent(
                new Rectangle((int)mousePos.X + xOffset, (int)mousePos.Y + yOffset + 3, 15, 14),
                Game1.mouseCursors,
                new Rectangle(280, 412, 15, 14),
                1.5f
                );
            coinsIcon.draw(Game1.spriteBatch);
        }
    }
}
