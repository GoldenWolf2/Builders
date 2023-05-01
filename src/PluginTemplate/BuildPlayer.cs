using System;
using Microsoft.Xna.Framework;
using MiniGamesAPI.Core;
using Terraria;
using Terraria.GameContent.NetModules;
using Terraria.Net;
using TShockAPI;

namespace BuildMaster
{
    public class BuildPlayer : MiniPlayer, IComparable<BuildPlayer>
    {
        public int AquiredMarks { get; set; } // Number of marks acquired
        public MiniRegion CurrentRegion { get; set; } // Current region the player is in
        public int GiveMarks { get; set; } // Number of marks given by the player
        public string SelectedTopic { get; set; } // Topic selected by the player
        public bool Locked { get; set; } // Whether the player is locked
        public bool Marked { get; set; } // Whether the player is marked

        public BuildPlayer(int id, string name, TSPlayer player)
        {
            base.ID = id; // Player ID
            base.Name = name; // Player name
            base.Player = player; // TSPlayer object
            base.CurrentRoomID = 0; // ID of the current room the player is in
            this.CurrentRegion = null; // Current region the player is in
            this.Locked = false; // Whether the player is locked
            this.Marked = false; // Whether the player is marked
            this.AquiredMarks = 0; // Number of marks acquired
        }

        public void Join(IRoom room)
        {
            if (room.GetPlayerCount() >= room.MaxPlayer)
            {
                this.SendInfoMessage("This room is full.");
                return;
            }
            if (room.Status != null)
            {
                this.SendInfoMessage("Cannot join game in this room.");
                return;
            }
            if (ConfigUtils.GetRoomByID(base.CurrentRoomID) != null)
            {
                this.Leave();
            }
            base.Join(room.ID);
            room.Players.Add(this);
            room.Broadcast("Player " + base.Name + " joined the room.", Color.MediumAquamarine);
            this.Teleport(room.WaitingPoint);
            if (!room.Loaded)
            {
                room.LoadRegion();
            }
        }

        public void Leave()
        {
            BuildRoom roomByID = ConfigUtils.GetRoomByID(base.CurrentRoomID);
            if (roomByID == null)
            {
                this.SendInfoMessage("Room does not exist.");
                return;
            }
            if (roomByID.Status != null)
            {
                this.SendInfoMessage("Cannot leave game in this room.");
                return;
            }
            roomByID.Players.Remove(this);
            roomByID.Broadcast("Player " + base.Name + " left the room.", Color.Crimson);
            this.Teleport(Main.spawnTileX, Main.spawnTileY);
            base.Leave();
        }

        public int CompareTo(BuildPlayer other)
        {
            return other.AquiredMarks.CompareTo(this.AquiredMarks);
        }

        public void Creative()
        {
            if (ConfigUtils.config.UnlockAll)
            {
                for (int i = 1; i <= 5042; i++)
                {
                    NetPacket netPacket = NetCreativeUnlocksModule.SerializeItemSacrifice(i, 999);
                    NetManager.Instance.SendToClient(netPacket, base.Player.Index);
                }
                return;
            }
            foreach (int num in ConfigUtils.config.Range.Keys)
            {
                for (int j = num; j < ConfigUtils.config.Range[num]; j++)
                {
                    NetPacket netPacket2 = NetCreativeUnlocksModule.SerializeItemSacrifice(j, 999);
                    NetManager.Instance.SendToClient(netPacket2, base.Player.Index);
                }
            }
        }

        public void UnCreative()
        {
            for (int i = 1; i <= 5042; i++)
            {
                NetPacket netPacket = NetCreativeUnlocksModule.SerializeItemSacrifice(i, 0);
                NetManager.Instance.SendToClient(netPacket, base.Player.Index);
            }
        }
    }
}
