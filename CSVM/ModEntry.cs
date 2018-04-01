using System;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using xTile;

namespace YourProjectName
{
	public class FishInjector: IAssetEditor
	{
		public bool CanEdit<T>(IAssetInfo asset)
		{
			return asset.AssetNameEquals(@"Data\Fish");
		}

		/// <summary>Load a matched asset.</summary>
		/// <param name="asset">Basic metadata about the asset being loaded.</param>
		public void Edit<T>(IAssetData asset)
		{
			asset.AsDictionary<int, string>().Set(797, "Gear/5/floater/-1/-1/600 2600/spring summer fall winter/both/-1/4/.1/.1/0");
			asset.AsDictionary<int, string>().Set(798, "Rod/5/floater/-1/-1/600 2600/spring summer fall winter/both/-1/4/.7/.1/0");
		}
	}

	public class ItemInjector : IAssetEditor
	{
		public bool CanEdit<T>(IAssetInfo asset)
		{
			return asset.AssetNameEquals(@"Data\ObjectInformation");
		}

		/// <summary>Load a matched asset.</summary>
		/// <param name="asset">Basic metadata about the asset being loaded.</param>
		public void Edit<T>(IAssetData asset)
		{
			asset.AsDictionary<int, string>().Set(797, "Gear/200/-300/Fish/Gear/A cog from a machine... I wonder what it comes from...");
			asset.AsDictionary<int, string>().Set(798, "Rod/200/-300/Fish/Rod/A metalic threaded rod... I wonder who made this...");
		}

	}

	public class TextureInjector: IAssetEditor
	{
		//attributes
		IModHelper helper;

		public TextureInjector(IModHelper helper)
		{
			this.helper = helper;
		}

		public bool CanEdit<T>(IAssetInfo asset)
		{
			return asset.AssetNameEquals(@"Maps\springobjects");
		}

		public void Edit<T>(IAssetData asset)
		{
			//Create and load textures.
			Texture2D gear = this.helper.Content.Load<Texture2D>("Res/Item797.png", ContentSource.ModFolder);
			Texture2D rod = this.helper.Content.Load<Texture2D>("Res/Item798.png", ContentSource.ModFolder);


			//Add textures to source file.
			asset.AsImage().PatchImage(gear, targetArea: new Rectangle(80, 528, 16,16));
			asset.AsImage().PatchImage(rod, targetArea: new Rectangle(96, 528, 16, 16));
		}
	}

	public class LocationEditor: IAssetEditor
	{
		//attributes
		IMonitor monitor;

		public LocationEditor(IMonitor monitor)
		{
			this.monitor = monitor;
		}
		/// <summary>Get whether this instance can edit the given asset.</summary>
		/// <param name="asset">Basic metadata about the asset being loaded.</param>
		public bool CanEdit<T>(IAssetInfo asset)
		{
			return asset.AssetNameEquals(@"Data\Locations");
		}

		/// <summary>Edit a matched asset.</summary>
		/// <param name="asset">A helper which encapsulates metadata about an asset and enables changes to it.</param>
		public void Edit<T>(IAssetData asset)
		{
			asset
				.AsDictionary<string, string>()
				.Set((id, data) =>
				{
					string[] fields = data.Split('/');
					fields[4] = fields[4] != "-1" ? fields[4] + " 797 -1 798 -1" : "797 -1 798 -1";
					fields[5] = fields[5] != "-1" ? fields[5] + " 797 -1 798 -1" : "797 -1 798 -1";
					fields[6] = fields[6] != "-1" ? fields[6] + " 797 -1 798 -1" : "797 -1 798 -1";
					fields[7] = fields[7] != "-1" ? fields[7] + " 797 -1 798 -1" : "797 -1 798 -1";

					return string.Join("/", fields);
				});
		}
	}

	/// <summary>The mod entry point.</summary>
	public class ModEntry : Mod
    {
		IModHelper helper;
		/*********
        ** Public methods
        *********/
		/// <summary>The mod entry point, called after the mod is first loaded.</summary>
		/// <param name="helper">Provides simplified APIs for writing mods.</param>
		public override void Entry(IModHelper helper)
		{
			this.helper = helper;
			InputEvents.ButtonPressed += this.InputEvents_ButtonPressed;

			//Load in new assest.
			helper.Content.AssetEditors.Add(new FishInjector());
			helper.Content.AssetEditors.Add(new ItemInjector());
			helper.Content.AssetEditors.Add(new LocationEditor(this.Monitor));
			helper.Content.AssetEditors.Add(new TextureInjector(helper));

			//Add locations into game.
			SaveEvents.AfterLoad += this.InjectNewLocations;
		}

        /*********
        ** Private methods
        *********/
        /// <summary>The method invoked when the player presses a controller, keyboard, or mouse button.</summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event data.</param>
        private void InputEvents_ButtonPressed(object sender, EventArgsInput e)
        {
			if (Context.IsWorldReady) // save is loaded
			{
				this.Monitor.Log($"{Game1.player.name} pressed {e.Button}.");
			}
		}

		private void InjectNewLocations(object sender, EventArgs e)
		{
			//Load location assets.
			Map TestRoom = this.helper.Content.Load<Map>("Res/RossRoom.tbin", ContentSource.ModFolder);

			//Create locations based off Maps declared.
			GameLocation TestArea = new GameLocation(TestRoom, "TestRoom") { IsOutdoors = false, IsFarm = false };

			//Load locations into game.
			Game1.locations.Add(TestArea);

			//Add Wap points to game
			Game1.getLocationFromName("Town").warps.Add(new Warp(18, 41, "TestRoom", 8, 11, false));
		}
    }
}