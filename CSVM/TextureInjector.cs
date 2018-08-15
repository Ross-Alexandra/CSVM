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
	public class TextureInjector : IAssetEditor
	{
		//attributes
		IModHelper helper;

		public TextureInjector(IModHelper helper)
		{
			this.helper = helper;
		}

		public bool CanEdit<T>(IAssetInfo asset)
		{
			return asset.AssetNameEquals(@"Maps\springobjects");
		}

		public void Edit<T>(IAssetData asset)
		{
			//Create and load textures.
			Texture2D gear = this.helper.Content.Load<Texture2D>("Res/Gear.png", ContentSource.ModFolder);
			Texture2D rod = this.helper.Content.Load<Texture2D>("Res/Rod.png", ContentSource.ModFolder);

			//Add textures to source file.
			asset.AsImage().PatchImage(gear, targetArea: new Rectangle(96, 528, 16, 16));
			asset.AsImage().PatchImage(rod, targetArea: new Rectangle(112, 528, 16, 16));
		}
	}
}
