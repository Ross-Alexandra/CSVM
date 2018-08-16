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
					fields[4] = fields[4] != "-1" ? fields[4] + $"{this.itemPairs["gear"]} -1 {this.itemPairs["rod"]} -1" : $"{this.itemPairs["gear"]} -1 {this.itemPairs["rod"]} -1";
					fields[5] = fields[5] != "-1" ? fields[4] + $"{this.itemPairs["gear"]} -1 {this.itemPairs["rod"]} -1" : $"{this.itemPairs["gear"]} -1 {this.itemPairs["rod"]} -1";
					fields[6] = fields[6] != "-1" ? fields[4] + $"{this.itemPairs["gear"]} -1 {this.itemPairs["rod"]} -1" : $"{this.itemPairs["gear"]} -1 {this.itemPairs["rod"]} -1";
					fields[7] = fields[7] != "-1" ? fields[4] + $"{this.itemPairs["gear"]} -1 {this.itemPairs["rod"]} -1" : $"{this.itemPairs["gear"]} -1 {this.itemPairs["rod"]} -1";

					return string.Join("/", fields);
				});

			// Get data for each location. - TODO
			//string caveTownData = asset.AsDictionary<string, string>()[5];
		}
	}
}
