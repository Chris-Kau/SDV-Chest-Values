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
        const int textXOffset = -25;
        const int textYOffset = 40;
        public override void Entry(IModHelper helper)
        {
            helper.Events.Display.MenuChanged += this.CheckChest;
            helper.Events.Display.RenderedActiveMenu += this.DisplayText;
        }

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
                //Checks to see if the mouse cursor is on a chest
                if(loc.objects.ContainsKey(Game1.currentCursorTile ) && (loc.Objects[Game1.currentCursorTile] is StardewValley.Objects.Chest chest))
                {
                    MouseText.DrawText(e, mousePos, textXOffset, textYOffset, chest);
                }
                //Checks to see if the mouse cursor is one tile above the chest
                if (loc.objects.ContainsKey(Game1.currentCursorTile + new Vector2(0,1)) && (loc.Objects[Game1.currentCursorTile + new Vector2(0, 1)] is StardewValley.Objects.Chest chest2))
                {
                    MouseText.DrawText(e, mousePos, textXOffset, textYOffset, chest2);
                }
            }
        }
    }
}