using Microsoft.Xna.Framework;
using MiniGamesAPI.Hooks;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using TShockAPI;
using static MiniGamesAPI.Enum;

namespace MiniGamesAPI.Core
{
    public class MiniPlayer
    {
        [JsonIgnore]
        public TSPlayer Player { get; set; }
        [JsonIgnore]
        public MiniCircle Circle {get;set;}
        public int Kills { get; set; }
        public int Deaths { get; set; }
        public int Assistances { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }
        public bool IsReady { get; set; }
        public bool Visiable { get; set; }
        public int CurrentRoomID { get; set; }
        public int SelectPackID { get; set; }
        [JsonIgnore]
        public MiniPack BackUp { get; set; }
        public PlayerStatus Status { get; set; }
        [JsonIgnore]
        public Vector2 Position { get { return Player.TPlayer.position; } }
        public void Ready()
        {
            IsReady = !IsReady;
        }
        public MiniPlayer(int id, TSPlayer player)
        {
            ID = id;
            Name = player.Name;
            Player = player;
            BackUp = null;
            IsReady = false;
            Status = PlayerStatus.Waiting;
            Kills = 0;
            Deaths = 0;
            Assistances = 0;
            CurrentRoomID = 0;
            SelectPackID = 0;
            
        }
        public MiniPlayer()
        {
            Kills = 0;
            Deaths = 0;
            Assistances = 0;
            CurrentRoomID = 0;
            SelectPackID = 0;
            Status = PlayerStatus.Waiting;
            IsReady = false;
            Hooks.HookManager.GameSecond += OnGameSecond;
        }

        private void OnGameSecond(GameSecondArgs args)
        {
            
        }

        public override string ToString()
        {
            StringBuilder playerInfo = new StringBuilder();
            playerInfo.AppendLine($"玩家名：{Name}");
            playerInfo.AppendLine($"击杀数：{Kills}");
            playerInfo.AppendLine($"死亡数：{Deaths}");
            playerInfo.AppendLine($"助攻数：{Assistances}");
            playerInfo.AppendLine($"准备状态：{IsReady}");
            playerInfo.AppendLine($"房间号：{CurrentRoomID}");
            playerInfo.AppendLine($"当前状态：{Status}");
            return playerInfo.ToString();
        }
        public virtual void Teleport(Point point)
        {
            var x = Math.Max(0, point.X);
            var y = Math.Max(0, point.Y);
            x = Math.Min(x, Terraria.Main.maxTilesX - 1);
            y = Math.Min(y, Terraria.Main.maxTilesY - 1);
            Player.Teleport(x * 16, y * 16 - 48);
        }
        public virtual void Teleport(int x, int y)
        {
            x = Math.Min(x, Terraria.Main.maxTilesX - 1);
            y = Math.Min(y, Terraria.Main.maxTilesY - 1);
            Player.Teleport(x * 16, y * 16 - 48);
        }
        public virtual void SendInfoMessage(string msg)
        {
            Player.SendMessage(msg, Color.DarkTurquoise);

        }
        public virtual void SendSuccessMessage(string msg)
        {
            Player.SendMessage(msg, Color.MediumAquamarine);
        }
        public virtual void SendErrorMessage(string msg)
        {
            Player.SendMessage(msg, Color.Crimson);
        }
        public virtual void SendMessage(string msg, Color color)
        {
            Player.SendMessage(msg, color);
        }
        public virtual void SetBuff(int type, int time = 3600, bool bypass = false)
        {
            Player.SetBuff(type, time, bypass);
        }
        public virtual void SetPVP(bool value)
        {
            Player.TPlayer.hostile = value;
            Player.SendData(PacketTypes.TogglePvp, "", Player.Index);
            TSPlayer.All.SendData(PacketTypes.TogglePvp, "", Player.Index);
        }
        public virtual void SetTeam(int id)
        {
            if (Player == null) return;
            Player.SetTeam(id);
        }
        public virtual void RestorePlayerInv(MiniPack pack) {
            pack.RestoreCharacter(Player);
        }
        public virtual void DropItem(int itemID)
        {
            int slot = Player.TPlayer.FindItem(itemID);
            var item = Player.TPlayer.inventory[slot];
            //下面是实现物品掉落
            var dropItem = Item.NewItem(new EntitySource_DebugCommand(), Player.TPlayer.position, Player.TPlayer.width, Player.TPlayer.height, item.type, item.stack, true, item.prefix, false);
            TSPlayer.All.SendData(PacketTypes.ItemDrop, "", dropItem);

            //实现物品没收

            item.netID = 0;
            TSPlayer.All.SendData(PacketTypes.PlayerSlot, item.Name, Player.Index, slot, item.prefix);
        }
        public virtual bool CheckContainItem(int netid)
        {
            return Player.TPlayer.HasItem(netid);
        }
        public float KDA() {
            float kills = (float)Kills;
            float deaths = (float)Deaths;
            float assists = (float)Assistances;
            if (kills == 0) kills = 1;
            if (deaths == 0) deaths = 1;
            if (assists == 0) assists = 1;
            float kda = (kills + assists) / deaths;
            return kda;
        }
        public Vector2 ToAnotherPlayer(MiniPlayer player) {
            return player.Position - this.Position;
        }
        public bool ClearRecord() {
            Kills = 0;
            Deaths = 0;
            Assistances = 0;
            return true;
        }
        public void SendBoardMsg(string info) {
            Player.SendData(PacketTypes.Status, info);
        }
        public void Godmode(bool state)
        {
            Player.GodMode = state;
            var power = CreativePowerManager.Instance.GetPower<CreativePowers.GodmodePower>();
            power.SetEnabledState(Player.Index, Player.GodMode);
        }
        public int FindItem(int netid)
        {
            return Player.TPlayer.FindItem(netid);
        }
        public void RemoveItem(int netid) 
        {
            var slot = FindItem(netid);
            Player.TPlayer.inventory[slot].netDefaults(0);
            Player.SendData(PacketTypes.PlayerSlot,"",Player.Index,slot, Player.TPlayer.inventory[slot].prefix);
        }
        public void Firework(int num) 
        {
            int type=0;
            switch (num)
            {
                case 1:
                    type = Terraria.ID.ProjectileID.RocketFireworkRed;
                    break;
                case 2:
                    type = Terraria.ID.ProjectileID.RocketFireworkBlue;
                    break;
                case 3:
                    type = Terraria.ID.ProjectileID.RocketFireworkGreen;
                    break;
                case 4:
                    type = Terraria.ID.ProjectileID.RocketFireworksBoxYellow;
                    break;
            }
            int p = Projectile.NewProjectile(new EntitySource_DebugCommand(), Player.TPlayer.position.X, Player.TPlayer.position.Y - 64f, 0f, -8f, type, 0, 0);
            TSPlayer.All.SendData(PacketTypes.ProjectileNew,"",p);
        }
        public void Join(int roomid) 
        {
            CurrentRoomID = roomid;
            BackUp = new MiniPack(Name,1);
            BackUp.CopyFromPlayer(Player);
        }
        public void Leave() 
        {
            CurrentRoomID = 0;
            BackUp.RestoreCharacter(Player);
            BackUp = null;
            Player.SaveServerCharacter();
            SelectPackID = 0;
            IsReady = false;
            Status = PlayerStatus.Waiting;
            SendBoardMsg("");
            ClearRecord();
        }
        public void SetDifficulty(byte flag) 
        {
            Player.TPlayer.difficulty = flag;
            TSPlayer.All.SendData(PacketTypes.PlayerInfo,"",Player.Index);
        }
    }
}
