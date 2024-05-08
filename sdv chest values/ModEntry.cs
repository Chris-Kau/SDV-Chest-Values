using System;
using System.Security.AccessControl;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sdv_chest_values;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;
using StardewValley.Menus;

namespace YourProjectName
{
    /// <summary>The mod entry point.</summary>
    internal sealed class ModEntry : Mod
    {
        /*********
        ** Public methods
        *********/
        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
            helper.Events.Display.MenuChanged += this.CheckChest;
            helper.Events.Display.RenderedActiveMenu += this.DisplayText;
        }


        /*********
        ** Private methods
        *********/
        /// <summary>Raised after the player presses a button on the keyboard, controller, or mouse.</summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event data.</param>
        private void CheckChest(object? sender, MenuChangedEventArgs e)
        {
            if(e.NewMenu is StardewValley.Menus.ItemGrabMenu menu)
            {
                if(menu.context is StardewValley.Objects.Chest chest)
                {
                    long total = ChestMethods.GetTotalValue(chest);
                    Console.WriteLine($"{total}");
                }
            }
        }

        private void DisplayText(object? sender, RenderedActiveMenuEventArgs e)
        {
            if(Context.IsWorldReady && Game1.activeClickableMenu is null)
            {
                GameLocation loc = Game1.player.currentLocation;
                Vector2 mousePos = new Vector2(Game1.getMousePosition().X, Game1.getMousePosition().Y);
                List<Vector2> chestLocs = ChestMethods.GetChestLocations(loc);
                foreach (Vector2 cl in chestLocs)
                {
                    if ((Game1.currentCursorTile == cl || Game1.currentCursorTile == cl + new Vector2(0,-1)) && loc.Objects[cl] is StardewValley.Objects.Chest chest)
                    {
                        //draws the price as a string underneath the cursor
                        e.SpriteBatch.DrawString(Game1.smallFont,$"{ChestMethods.GetTotalValue(chest)}", mousePos + new Vector2(0,40), Color.White);
                        //draws the coin icon to the left of the price string
                        ClickableTextureComponent coinsIcon = new ClickableTextureComponent(
                            new Rectangle((int)mousePos.X - 25, (int)mousePos.Y + 43, 15,14),
                            Game1.mouseCursors,
                            new Rectangle(280,412,15,14),
                            1.5f
                            );
                        coinsIcon.draw(Game1.spriteBatch);
                    }
                }
            }
        }
    }
}