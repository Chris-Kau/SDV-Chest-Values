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
        public static void DrawText(RenderedActiveMenuEventArgs e, Vector2 mousePos, int textpos, StardewValley.Objects.Chest chest, Vector2 chestTilePos, float TextBgTransparency)
        {
            string total_value = "";
            if (ChestMethods.ChestValues.Count <= 0)
            {
                total_value = "Chest data has not loaded";
            }
            else
            {
                if (ChestMethods.ChestValues.TryGetValue(chestTilePos, out long value))
                    total_value = value.ToString();
            }

            string valueText = $"{total_value}";
            Vector2 textSize = Game1.smallFont.MeasureString(valueText);

            //only check to see if the player wants the text on the left of the cursor so we can take into account how many digits are in the total_value so the text doesnt overlap the cursor.
            if (textpos == 3)
            {
                Vector2 textPosition = mousePos + new Vector2(tpos[textpos][0], tpos[textpos][1]) - new Vector2(textSize.X, 0);

                Rectangle backgroundRect = new Rectangle(
                    (int)textPosition.X - 30,
                    (int)textPosition.Y - 4,
                    (int)textSize.X + 34,
                    (int)textSize.Y + 4
                );
                e.SpriteBatch.Draw(Game1.fadeToBlackRect, backgroundRect, Color.Black * TextBgTransparency);
                e.SpriteBatch.DrawString(Game1.smallFont, valueText, textPosition, Color.White);

                Vector2 iconPosition = textPosition - new Vector2(20, -3); 
                ClickableTextureComponent coinsIcon = new ClickableTextureComponent(
                    new Rectangle((int)iconPosition.X - 5, (int)iconPosition.Y, 15, 14),
                    Game1.mouseCursors,
                    new Rectangle(280, 412, 15, 14),
                    1.5f
                );
                coinsIcon.draw(Game1.spriteBatch);
            }
            else
            {
                Vector2 textPosition = mousePos + new Vector2(tpos[textpos][0] + 25, tpos[textpos][1]);

                // 2. Draw semi-transparent black rectangle behind text
                Rectangle backgroundRect = new Rectangle(
                    (int)textPosition.X - 30,
                    (int)textPosition.Y - 4,
                    (int)textSize.X + 34,
                    (int)textSize.Y + 4
                );
                e.SpriteBatch.Draw(Game1.fadeToBlackRect, backgroundRect, Color.Black * TextBgTransparency);
                //draws the price as a string near the cursor
                e.SpriteBatch.DrawString(Game1.smallFont, valueText, mousePos + new Vector2(tpos[textpos][0] + 25, tpos[textpos][1]), Color.White);
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
