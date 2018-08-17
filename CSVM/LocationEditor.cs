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
	public class LocationEditor : IAssetEditor
	{
		//attributes
		IMonitor monitor;
		private readonly Dictionary<string, int> itemPairs;

		public LocationEditor(IMonitor monitor, Dictionary<string, int> itemPairs)
		{
			this.monitor = monitor;
			this.itemPairs = itemPairs;
		}

		public bool CanEdit<T>(IAssetInfo asset)
		{
			return asset.AssetNameEquals(@"Data\Locations");
		}

		public void Edit<T>(IAssetData asset)
		{

			//Add data for new locations.
			string[] baseLocationParams = { "-1" , "-1" , "-1", "-1", "-1", "-1", "-1", "-1", "-1"};

			// Add the fish to the town cave.
			string[] townCaveParams = (string[]) baseLocationParams.Clone();
			townCaveParams[4] = $"{this.itemPairs["gear"]} -1";
			townCaveParams[5] = $"{this.itemPairs["gear"]} -1";
			townCaveParams[6] = $"{this.itemPairs["gear"]} -1";
			townCaveParams[7] = $"{this.itemPairs["gear"]} -1";

			// Reset the assets.
			asset.AsDictionary<string, string>().Set("TownCave", String.Join("/", townCaveParams));
		}
	}
}
