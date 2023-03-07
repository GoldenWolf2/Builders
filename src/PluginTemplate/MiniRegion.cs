using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using TShockAPI;

namespace MiniGamesAPI.Core
{
	public class MiniRegion
	{
		public string Name { get; set; }
		public int ID { get; set; }
		public List<string> AllowGroups { get; set; }
		public List<string> AllowUsers { get; set; }
		public List<string> Owners { get; set; }
		public Point TopLeft
		{
			get
			{
				return new Point(this.Area.X, this.Area.Y);
			}
		}
		public Point TopRight
		{
			get
			{
				return new Point(this.Area.X + this.Area.Width-1 , this.Area.Y);
			}
		}
		public Point BottomLeft
		{
			get
			{
				return new Point(this.Area.X, this.Area.Y + this.Area.Height-1);
			}
		}
		public Point BottomRight
		{
			get
			{
				return new Point(this.Area.X + this.Area.Width-1, this.Area.Y + this.Area.Height-1);
			}
		}
		public Point Center
		{
			get
			{
				return new Point((this.Area.Width / 2 + this.Area.X) , (this.Area.Y + this.Area.Height/ 2) );
			}
		}
		public Rectangle Area { get; set; }
		public bool IsLocked { get; set; }
		public MiniRegion(string name, int id, int topLeft_x,int topLeft_y,int bottomRight_x,int bottomRight_y)
		{
			this.ID = id;
			this.Name = name;
			this.IsLocked = false;
			this.AllowGroups = new List<string>();
			this.AllowUsers = new List<string>();
			this.Owners = new List<string>();
			int x, y, width, height;
			x = Math.Min(topLeft_x,bottomRight_x);
			y = Math.Min(topLeft_y,bottomRight_y);
			width = Math.Abs(topLeft_x - bottomRight_x) + 1;
			height = Math.Abs(topLeft_y - bottomRight_y) + 1;
			var area = new Rectangle(x, y, width, height);
			this.Area = area;
		}
		public MiniRegion(string name, int id, Rectangle area)
		{
			this.ID = id;
			this.Name = name;
			this.IsLocked = false;
			this.AllowGroups = new List<string>();
			this.AllowUsers = new List<string>();
			this.Owners = new List<string>();
			this.Area = area;
		}
		public void ShowFramework()
		{
			Vector2 width = new Vector2((TopRight.X+1  - TopLeft.X )* 16f, 0f);
			Vector2 height = new Vector2(0f,(BottomLeft.Y+1  - TopLeft.Y)*16f);
			Vector2 center = new Vector2(Area.Center.X * 16f, Area.Center.Y * 16f);
			int proj_ = Projectile.NewProjectile(new EntitySource_DebugCommand(), new Vector2(TopLeft.X * 16f, TopLeft.Y * 16f), width * 0.01f, 116, 0, 0f, 255, 0f, 0f);
			int proj_2 = Projectile.NewProjectile(new EntitySource_DebugCommand(), new Vector2(TopLeft.X * 16f, TopLeft.Y * 16f), height * 0.01f, 116, 0, 0f, 255, 0f, 0f);
			int proj_3 = Projectile.NewProjectile(new EntitySource_DebugCommand(), new Vector2((BottomLeft.X) * 16f, (BottomLeft.Y+1) * 16f), width * 0.01f, 116, 0, 0f, 255, 0f, 0f);
			int proj_4 = Projectile.NewProjectile(new EntitySource_DebugCommand(), new Vector2((TopRight.X+1) * 16f, TopRight.Y * 16f), height * 0.01f, 116, 0, 0f, 255, 0f, 0f);
			int proj_5 = Projectile.NewProjectile(new EntitySource_DebugCommand(), center, Vector2.Zero, 467, 0, 0f, 255, 0f, 0f);
			TSPlayer.All.SendData(PacketTypes.ProjectileNew, "", proj_, 0f, 0f, 0f, 0);
			TSPlayer.All.SendData(PacketTypes.ProjectileNew, "", proj_2, 0f, 0f, 0f, 0);
			TSPlayer.All.SendData(PacketTypes.ProjectileNew, "", proj_3, 0f, 0f, 0f, 0);
			TSPlayer.All.SendData(PacketTypes.ProjectileNew, "", proj_4, 0f, 0f, 0f, 0);
			TSPlayer.All.SendData(PacketTypes.ProjectileNew, "", proj_5, 0f, 0f, 0f, 0);
		}
		public string ShowCoordination() 
		{
			return $"左上角({TopLeft.X},{TopLeft.Y})\n  左下角({BottomLeft.X},{BottomLeft.Y})\n  右上角({TopRight.X},{TopRight.Y})\n  右下角({BottomRight.X},{BottomRight.Y})\n";
		}
		public bool Contain(Point point)
		{
			return Area.Contains(point);
		}
		public bool Contain(int x, int y)
		{
			return Area.Contains(x, y);
		}
		public void BuildFramework(int tileID,bool send=false) 
		{
            for (int i = TopLeft.X; i <= TopRight.X; i++)
            {
				for (int j = TopLeft.Y; j <= BottomLeft.Y; j++)
				{
					if (i > TopLeft.X && i < TopRight.X && j > TopLeft.Y && j < BottomLeft.Y) continue;
					WorldGen.PlaceTile(i,j,tileID);
				}
			}
            if (send) TSPlayer.All.SendTileRect((short)TopLeft.X, (short)TopLeft.Y, (byte)(Area.Width + 3), (byte)(Area.Height + 3));

		}
		public List<MiniRegion> Divide(int width,int height,int amount,int gap) 
		{
			int x = TopLeft.X;
			int y = TopLeft.Y;
			if (gap*amount+amount*(width+2)>Area.Width)return null;
			var regions = new List<MiniRegion>();
            for (int i = 0; i < amount; i++)
            {
				Rectangle area = new Rectangle(x,y,width+2,height+2);
				regions.Add(new MiniRegion(Name+$"_{i}",ID+i+1,area));
				x += gap + width+2 ;
            }
			return regions;
		}
	}
}
