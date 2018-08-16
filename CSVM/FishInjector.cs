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
	public class FishInjector : IAssetEditor
	{
		// A dictionary created in InjectTextures which holds the index for each item being injected.
		private readonly Dictionary<string, int> itemPairs;

		// Initialized the dictionary with the actual dictionary used.
		public FishInjector(Dictionary<string, int> itemPairs)
		{
			this.itemPairs = itemPairs;
		}


		public bool CanEdit<T>(IAssetInfo asset)
		{
			return asset.AssetNameEquals(@"Data\Fish");
		}

		//Injects the fish into the game's data files.
		public void Edit<T>(IAssetData asset)
		{
			asset.AsDictionary<int, string>().Set(this.itemPairs["gear"], "Mysterious Gear/5/floater/-1/-1/600 2600/spring summer fall winter/both/-1/0/.7/.1/0");
			asset.AsDictionary<int, string>().Set(this.itemPairs["rod"], "Mysterious Rod/5/floater/-1/-1/600 2600/spring summer fall winter/both/-1/0/.7/.1/0");
		}
	}
}
