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

			// Get data for each location.
			//string caveTownData = asset.AsDictionary<string, string>()[5];
		}
	}
}
