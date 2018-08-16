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

	public class ItemInjector : IAssetEditor
	{
		// A dictionary created in InjectTextures which holds the index for each item being injected.
		private readonly Dictionary<string, int> itemPairs;

		// Initialized the dictionary with the actual dictionary used.
		public ItemInjector(Dictionary<string, int> itemPairs)
		{
			this.itemPairs = itemPairs;
		}

		public bool CanEdit<T>(IAssetInfo asset)
		{
			return asset.AssetNameEquals(@"Data\ObjectInformation");
		}

		/// <summary>Load a matched asset.</summary>
		/// <param name="asset">Basic metadata about the asset being loaded.</param>
		public void Edit<T>(IAssetData asset)
		{
			asset.AsDictionary<int, string>().Set(this.itemPairs["gear"], "Mysterious Gear/200/-300/Fish/Mysterious Gear/A cog from a machine... I wonder what it comes from...");
			asset.AsDictionary<int, string>().Set(this.itemPairs["rod"], "Mysterious Rod/200/-300/Fish/Mysterious Rod/A metalic threaded rod... I wonder who made this...");
		}

	}
}
