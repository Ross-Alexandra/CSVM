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
		IModHelper helper; //Helper for the mod
		private Dictionary<string, int> itemPairs; //item_name: item_index

		// A list of the names of all the textures being injected in order of
		// appearance in the spritesheet (left to right, then top to bottom).
		private string[] textures = 
		{
			"gear",
			"rod"
		};


		public TextureInjector(IModHelper helper, ref Dictionary<string, int> itemPairs)
		{
			this.helper = helper;
			this.itemPairs = itemPairs;
		}

		public bool CanEdit<T>(IAssetInfo asset)
		{
			return asset.AssetNameEquals(@"Maps\springobjects");
		}

		public void Edit<T>(IAssetData asset)
		{
			// Create a texture of the original spritesheet, and collect its data.
			Texture2D spriteSheet = asset.AsImage().Data;
			int sheetHeight = spriteSheet.Height;
			int sheetWidth = spriteSheet.Width;
			Color[] originalData = new Color[sheetHeight * sheetWidth];
			spriteSheet.GetData(originalData);

			// Create a texture of the csvm spritesheet, and collect its data.
			Texture2D csvmSpriteSheet = this.helper.Content.Load<Texture2D>("Res/csvmTileSheet.png", ContentSource.ModFolder);
			int csvmSheetHeight = csvmSpriteSheet.Height;
			int csvmSheetWidth = csvmSpriteSheet.Width;
			Color[] csvmData = new Color[csvmSheetHeight * csvmSheetWidth];
			csvmSpriteSheet.GetData(csvmData);

			// Combine the original and the csvm spritesheets 
			Texture2D combinedSpriteSheet = new Texture2D(Game1.game1.GraphicsDevice, sheetWidth, sheetHeight + csvmSheetHeight, false, SurfaceFormat.Color);
			Color[] combinedData = new Color[originalData.Length + csvmData.Length];
			originalData.CopyTo(combinedData, 0);
			csvmData.CopyTo(combinedData, originalData.Length);
			combinedSpriteSheet.SetData(combinedData);

			// Replace the game's spritesheet with the newly created one.
			asset.ReplaceWith(combinedSpriteSheet);

			// Assign an index to each new texture added in the itemPairs dictionary.
			// The key is set to the name from textures, and the index is its item
			// index that will be recognized in game.
			for (int i = 0; i < (textures.Length / 16) + 1; i++)
			{
				int remainingTextures = i == (textures.Length / 16) ? textures.Length % 16 : 16;
				for (int j = 0; j < remainingTextures; j++)
				{
					// Current texture is the number texture we're on. 16 * the row + the column.
					int currentTexture = (16 * i) + j;

					// Create the key value pair of texture_name: texture's index.
					// Note, currentTexture is not incremented by one as this is an index (Started from 0) and
					// not an absolute position (started from 1.)
					this.itemPairs[textures[currentTexture]] = ((sheetHeight / 16) * (sheetWidth / 16)) + (currentTexture);
				}
			}
		}
	}
}
