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
            helper.Events.Player.Warped += this.LoadChestValuesOnWarped;
            helper.Events.Display.MenuChanged += this.UpdateChestValueOnMenuChanged;
            helper.Events.World.ObjectListChanged += this.UpdateChestValueOnChestPlaced;
            helper.Events.GameLoop.DayStarted += this.UpdateChestValueOnDayStarted;
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

        private async void LoadChestValuesOnWarped(object? sender, WarpedEventArgs e)
        {
            GameLocation newLocation = e.NewLocation;
            await Task.Run(() =>
            {
                ChestMethods.UpdateAllChestValues(newLocation);
            });
        }

        private async void UpdateChestValueOnMenuChanged(object? sender, MenuChangedEventArgs e)
        {
            if(e.OldMenu != null && e.OldMenu is StardewValley.Menus.ItemGrabMenu menu)
            {
                if(menu.context is StardewValley.Objects.Chest chest)
                {
                    await Task.Run(() =>
                    {
                        ChestMethods.UpdateChestValue(chest);
                    });
                }
            }
        }

        private async void UpdateChestValueOnChestPlaced(object? sender, ObjectListChangedEventArgs e)
        {
            await Task.Run(() =>
            {
                if (e.IsCurrentLocation)
                {
                    Console.WriteLine("Removed:");
                    foreach (var ob in e.Removed.Where(x => x.Value is StardewValley.Objects.Chest))
                    {
                        var chest = (StardewValley.Objects.Chest)ob.Value;
                        ChestMethods.RemoveChestValue(chest);
                    }
                    Console.WriteLine("Added:");
                    foreach (var ob in e.Added.Where(x => x.Value is StardewValley.Objects.Chest))
                    {
                        var chest = (StardewValley.Objects.Chest)ob.Value;
                        ChestMethods.AddChestValue(chest);
                    }
                }

            });

        }

        private async void UpdateChestValueOnDayStarted(object? sender, DayStartedEventArgs e)
        {
            GameLocation loc = Game1.player.currentLocation;
            await Task.Run(() =>
            {
                ChestMethods.UpdateAllChestValues(loc);
            });
        }
        private void DisplayText(object? sender, RenderedActiveMenuEventArgs e)
        {
            if(Context.IsWorldReady && Game1.activeClickableMenu is null && Config.toggleHover)
            {
                GameLocation loc = Game1.player.currentLocation;
                Vector2 mousePos = new Vector2(Game1.getMousePosition().X, Game1.getMousePosition().Y);
                Vector2 mouseTilePos = Game1.currentCursorTile;
                //Checks to see if the mouse cursor is on a chest
                if (loc.objects.ContainsKey(Game1.currentCursorTile) && (loc.Objects[Game1.currentCursorTile] is StardewValley.Objects.Chest chest))
                {
                    MouseText.DrawText(e, mousePos, Config.textPosition, chest, mouseTilePos);
                }
                //Checks to see if the mouse cursor is one tile above the chest because chest hitbox yeah...
                //if (loc.objects.ContainsKey(Game1.currentCursorTile + new Vector2(0, 1)) && (loc.Objects[Game1.currentCursorTile + new Vector2(0, 1)] is StardewValley.Objects.Chest chest2))
                //{
                //    MouseText.DrawText(e, mousePos, Config.textPosition, chest2, mouseTilePos + new Vector2(0, 1));
                //}
            }
        }
    }
}
