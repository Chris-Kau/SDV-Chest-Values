using System;
using Microsoft.Xna.Framework;
using sdv_chest_values;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;

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

        }
    }
}