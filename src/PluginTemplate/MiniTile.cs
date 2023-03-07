using OTAPI.Tile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using TShockAPI;

namespace MiniGamesAPI.Core
{
	public class MiniTile
	{
		public int X { get; set; }
		public int Y { get; set; }
		public bool Active { get { return Tile.active(); } }
		public int Type { get { return Tile.type; } }
		public ITile Tile { get; set; }
		public MiniTile(int x, int y, ITile tile)
		{
			X = x;
			Y = y;
			Tile = new Tile(tile);

		}
		public void Place()
		{
			/*if (Active)
			{
				WorldGen.PlaceTile(X, Y, this.Type, false, false, -1, Tile.blockType());
				WorldGen.PlaceWall(X,Y,this.Tile.wall);
			}*/
			Terraria.Main.tile[X, Y] = new Tile(Tile);
		}
		public void Kill(bool fail=false,bool effectOnly = false,bool noitem=false) {
			WorldGen.KillTile(X, Y,fail,effectOnly,noitem);
		}
		public void Update() 
		{
			TSPlayer.All.SendTileRect((short)X,(short)Y,1,1);
		}
	}
}
