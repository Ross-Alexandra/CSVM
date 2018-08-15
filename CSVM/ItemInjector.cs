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
		public bool CanEdit<T>(IAssetInfo asset)
		{
			return asset.AssetNameEquals(@"Data\ObjectInformation");
		}

		/// <summary>Load a matched asset.</summary>
		/// <param name="asset">Basic metadata about the asset being loaded.</param>
		public void Edit<T>(IAssetData asset)
		{
			asset.AsDictionary<int, string>().Set(798, "Mysterious Gear/200/-300/Fish/Gear/A cog from a machine... I wonder what it comes from...");
			asset.AsDictionary<int, string>().Set(799, "Mysterious Rod/200/-300/Fish/Rod/A metalic threaded rod... I wonder who made this...");
		}

	}
}
