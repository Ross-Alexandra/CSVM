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
	public class LootTableEditor : IAssetEditor
	{
		//attributes
		IMonitor monitor;
		private readonly Dictionary<string, int> itemPairs;

		public LootTableEditor(IMonitor monitor, Dictionary<string, int> itemPairs)
		{
			this.monitor = monitor;
			this.itemPairs = itemPairs;
		}

		public bool CanEdit<T>(IAssetInfo asset)
		{
			return asset.AssetNameEquals(@"Data\Monsters");
		}

		public void Edit<T>(IAssetData asset)
		{

			//Inject items into the loot table of specific monsters.
			injectLoot(asset, "Metal Head", itemPairs["rod"], .1); //Inject Metal Head's loot table with a rod which drops .1% of the time.
		}

		private void injectLoot(IAssetData asset, string monster, int item, double percentage)
		{
			string curMonsterString;
			if (!asset.AsDictionary<string, string>().Data.TryGetValue(monster, out curMonsterString))
			{
				string alert = $"Unable to add {item} to {monster} as \"{monster}\" does not exist in the assets. This may cause unforseen issues.";
				this.monitor.Log(alert, LogLevel.Error);
				return;
			}

			string[] monsterParams = curMonsterString.Split('/');
			monsterParams[6] += $" {item} {percentage / 100}";

			asset.AsDictionary<string, string>().Set(monster, String.Join("/", monsterParams));
		}
	}
}
