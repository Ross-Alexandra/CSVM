using System;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using xTile;

namespace CSVM
{

	public class ModEntry : Mod
    {

		public Dictionary<string, int> itemIndices;

		public override void Entry(IModHelper helper)
		{

			//Setup input events object.
			csvmInputEvents InputEventsObject = new csvmInputEvents(this);

			//Add input events from the object.
			InputEvents.ButtonPressed += InputEventsObject.ButtonPressed;

			//Setup save events object.
			csvmSaveEvents SaveEventsObject = new csvmSaveEvents(this);

			//Add save events from the object.
			StardewModdingAPI.Events.SaveEvents.AfterLoad += SaveEventsObject.LocationInjector;

			//Inject new assest into game.
			itemIndices = new Dictionary<string, int>();

			helper.Content.AssetEditors.Add(new TextureInjector(helper, ref itemIndices));
			helper.Content.AssetEditors.Add(new FishInjector(itemIndices));
			helper.Content.AssetEditors.Add(new LocationEditor(this.Monitor, itemIndices));
			helper.Content.AssetEditors.Add(new ItemInjector(itemIndices));
			helper.Content.AssetEditors.Add(new LootTableEditor(this.Monitor, itemIndices));
		}
	}
}