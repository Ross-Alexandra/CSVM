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
	class csvmInputEvents
	{
		Mod coreMod;

		public csvmInputEvents(Mod coreMod)
		{
			this.coreMod = coreMod;
		}

		public void ButtonPressed(object sender, EventArgsInput e)
		{
			if (Context.IsWorldReady) // save is loaded
			{
				this.coreMod.Monitor.Log($"{Game1.player.Name} pressed {e.Button}.");
			}
		}

	}
}
