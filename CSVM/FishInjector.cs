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
		public bool CanEdit<T>(IAssetInfo asset)
		{
			return asset.AssetNameEquals(@"Data\Fish");
		}

		/// <summary>Load a matched asset.</summary>
		/// <param name="asset">Basic metadata about the asset being loaded.</param>
		public void Edit<T>(IAssetData asset)
		{
			asset.AsDictionary<int, string>().Set(798, "Gear/5/floater/-1/-1/600 2600/spring summer fall winter/both/-1/1/.7/.1/0");
			asset.AsDictionary<int, string>().Set(799, "Rod/5/floater/-1/-1/600 2600/spring summer fall winter/both/-1/1/.7/.1/0");
		}
	}
}
