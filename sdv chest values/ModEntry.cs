using System;
using System.Security.AccessControl;
using GenericModConfigMenu;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sdv_chest_values;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;
using StardewValley.Menus;

namespace sdv_chest_values
{
    /// <summary>The mod entry point.</summary>
    internal sealed class ModEntry : Mod
    {

        private ModConfig Config { get; set; } = new ModConfig();
        public override void Entry(IModHelper helper)
        {
            helper.Events.GameLoop.GameLaunched += this.OnGameLaunched;
            helper.Events.Display.RenderedActiveMenu += this.DisplayText;
        }

        private void OnGameLaunched(object? sender, GameLaunchedEventArgs e)
        {
            var configMenu = this.Helper.ModRegistry.GetApi<IGenericModConfigMenuApi>("spacechase0.GenericModConfigMenu");
            if (configMenu is null)
                return;

            // register mod
            configMenu.Register(
                mod: this.ModManifest,
                reset: () => this.Config = new ModConfig(),
                save: () => this.Helper.WriteConfig(this.Config)
            );

            configMenu.AddNumberOption(
                this.ModManifest,
                () => Config.textPosition,
                value => Config.textPosition = value,
                () => "Text Position Relative to Mouse:\n1: Below    2: Above    3: Left    4: Right",
                min:1,
                max:4,
                interval: 1
             );

            configMenu.AddBoolOption(
                this.ModManifest,
                () => Config.toggleHover,
                value => Config.toggleHover = value,
                () => "Toggle Chest Hover Effect"
            );


        }
        private void DisplayText(object? sender, RenderedActiveMenuEventArgs e)
        {
            if(Context.IsWorldReady && Game1.activeClickableMenu is null && Config.toggleHover)
            {
                GameLocation loc = Game1.player.currentLocation;
                Vector2 mousePos = new Vector2(Game1.getMousePosition().X, Game1.getMousePosition().Y);

                //Checks to see if the mouse cursor is on a chest
                if(loc.objects.ContainsKey(Game1.currentCursorTile ) && (loc.Objects[Game1.currentCursorTile] is StardewValley.Objects.Chest chest))
                {
                    MouseText.DrawText(e, mousePos, Config.textPosition, chest);
                }
                //Checks to see if the mouse cursor is one tile above the chest
                if (loc.objects.ContainsKey(Game1.currentCursorTile + new Vector2(0,1)) && (loc.Objects[Game1.currentCursorTile + new Vector2(0, 1)] is StardewValley.Objects.Chest chest2))
                {
                    MouseText.DrawText(e, mousePos, Config.textPosition, chest2);
                }
            }
        }
    }
}
//tpos[Config.textPosition][0]