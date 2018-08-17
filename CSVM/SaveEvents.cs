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
	public class csvmSaveEvents
	{
		ModEntry coreMod;

		public csvmSaveEvents(ModEntry coreMod)
		{
			this.coreMod = coreMod;
		}

		public void LocationInjector(object sender, EventArgs e)
		{

			//Load location assets.
			Map testRoomMap = this.coreMod.Helper.Content.Load<Map>("Res/RossRoom.tbin", ContentSource.ModFolder);
			Map cityMap = this.coreMod.Helper.Content.Load<Map>("Res/Town.tbin", ContentSource.ModFolder);
			Map cityCaveMap = this.coreMod.Helper.Content.Load<Map>("Res/townCave.tbin", ContentSource.ModFolder);

			//Get asset keys for created areas.
			string testRoomAssetKey = this.coreMod.Helper.Content.GetActualAssetKey("Res/RossRoom.tbin", ContentSource.ModFolder);
			string cityAssetKey = this.coreMod.Helper.Content.GetActualAssetKey("Res/Town.tbin", ContentSource.ModFolder);
			string cityCaveAssetKey = this.coreMod.Helper.Content.GetActualAssetKey("Res/townCave.tbin", ContentSource.ModFolder);

			//Create locations based off Maps declared.
			GameLocation TestArea = new GameLocation(testRoomAssetKey, "TestRoom") { IsOutdoors = false, IsFarm = false };
			GameLocation cityCave = new GameLocation(cityCaveAssetKey, "TownCave") { IsOutdoors = false, IsFarm = false };

			//Load locations into game.
			Game1.locations.Add(TestArea);
			Game1.locations.Add(cityCave);

			//Edit current locations in the game.
			Game1.getLocationFromName("Town").map = cityMap;
			Game1.getLocationFromName("Town").updateMap();

			//Add Wap points to game
			Game1.getLocationFromName("Town").warps.Add(new Warp(27, 46, "Town Cave", 4, 9, false));
			Game1.getLocationFromName("Town").warps.Add(new Warp(28, 46, "Town Cave", 4, 9, false));
			cityCave.warps.Add(new Warp(4, 10, "Town", 27, 47, false));
		}
	}
}
