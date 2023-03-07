using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Localization;
using TShockAPI;

namespace MiniGamesAPI.Core
{
	public class MiniPack
	{
		
		public string Name { get; set; }

		
		public int ID { get; set; }

		
		public int UnlockedBiomeTorches { get; set; }

		
		public int HappyFunTorchTime { get; set; }

		
		public int UsingBiomeTorches { get; set; }

		
		public int QuestsCompleted { get; set; }

		
		public bool[] HideVisuals { get; set; }

		
		public Color? EyeColor { get; set; }

		
		public Color? SkinColor { get; set; }

		
		public Color? ShoeColor { get; set; }

		
		public Color? UnderShirtColor { get; set; }

		
		public Color? ShirtColor { get; set; }

		
		public Color? HairColor { get; set; }

		
		public Color? PantsColor { get; set; }

		
		public int? Hair { get; set; }

		
		public int? SkinVariant { get; set; }

		
		public int? ExtraSlots { get; set; }

		
		public int SpawnY { get; set; }

		
		public int SpawnX { get; set; }

		
		public bool Exists { get; set; }

		
		public int MaxMana { get; set; }

		
		public int Mana { get; set; }

		
		public int MaxHP { get; set; }

		
		public int HP { get; set; }

		
		public byte HairDye { get; set; }

		
		public List<MiniItem> Items { get; set; }

		
		public MiniPack(string name, int id)
		{
			this.Name = name;
			this.ID = id;
			this.Hair = new int?(0);
			this.SkinVariant = new int?(0);
			this.ExtraSlots = new int?(0);
			this.SpawnY = -1;
			this.SpawnX = -1;
			this.Exists = true;
			this.MaxMana = 20;
			this.Mana = 20;
			this.HP = 100;
			this.MaxHP = 100;
			this.HairDye = 0;
			this.HideVisuals = new bool[10];
			this.UnlockedBiomeTorches = 0;
			this.HappyFunTorchTime = 0;
			this.UsingBiomeTorches = 0;
			this.QuestsCompleted = 0;
			this.EyeColor = new Color?(new Color(4283128425U));
			this.SkinColor = new Color?(new Color(4284120575U));
			this.ShoeColor = new Color?(new Color(4282149280U));
			this.UnderShirtColor = new Color?(new Color(4292326560U));
			this.ShirtColor = new Color?(new Color(4287407535U));
			this.HairColor = new Color?(new Color(4287407535U));
			this.PantsColor = new Color?(new Color(4287407535U));
			this.Items = new List<MiniItem>();
		}
		public MiniPack GetCopyNoItems(string name,int id) 
		{
			MiniPack copy = new MiniPack(name,id);
			copy.Exists = this.Exists;
			copy.ExtraSlots = this.ExtraSlots;
			copy.EyeColor = this.EyeColor;
			copy.Hair = this.Hair;
			copy.HairColor = this.HairColor;
			copy.HairDye = this.HairDye;
			copy.HappyFunTorchTime = this.HappyFunTorchTime;
			copy.HideVisuals = this.HideVisuals;
			copy.HP = this.HP;
			copy.Items = new List<MiniItem>();
			copy.Mana = this.Mana;
			copy.MaxHP = this.MaxHP;
			copy.MaxMana = this.MaxMana;
			copy.PantsColor = this.PantsColor;
			copy.QuestsCompleted = this.QuestsCompleted;
			copy.ShirtColor = this.ShirtColor;
			copy.ShoeColor = this.ShoeColor;
			copy.SkinColor = this.SkinColor;
			copy.SkinVariant = this.SkinVariant;
			copy.SpawnX = this.SpawnX;
			copy.SpawnY = this.SpawnY;
			copy.UnderShirtColor = this.UnderShirtColor;
			copy.UnlockedBiomeTorches = this.UnlockedBiomeTorches;
			copy.UsingBiomeTorches = this.UsingBiomeTorches;
			return copy;
		}
		
		public void RestoreCharacter(TSPlayer player)
		{
			ApplyInfoToPlr(player);
			NetDefaultsZeroAllInv(player);
			ApplyItemToPlrAll(player);
			float slot = 0f;
			for (int k = 0; k < NetItem.InventorySlots; k++)
			{
				NetMessage.SendData(5, -1, -1, NetworkText.FromLiteral(Main.player[player.Index].inventory[k].Name), player.Index, slot, (float)Main.player[player.Index].inventory[k].prefix, 0f, 0, 0, 0);
				slot += 1f;
			}
			for (int l = 0; l < NetItem.ArmorSlots; l++)
			{
				NetMessage.SendData(5, -1, -1, NetworkText.FromLiteral(Main.player[player.Index].armor[l].Name), player.Index, slot, (float)Main.player[player.Index].armor[l].prefix, 0f, 0, 0, 0);
				slot += 1f;
			}
			for (int m = 0; m < NetItem.DyeSlots; m++)
			{
				NetMessage.SendData(5, -1, -1, NetworkText.FromLiteral(Main.player[player.Index].dye[m].Name), player.Index, slot, (float)Main.player[player.Index].dye[m].prefix, 0f, 0, 0, 0);
				slot += 1f;
			}
			for (int n = 0; n < NetItem.MiscEquipSlots; n++)
			{
				NetMessage.SendData(5, -1, -1, NetworkText.FromLiteral(Main.player[player.Index].miscEquips[n].Name), player.Index, slot, (float)Main.player[player.Index].miscEquips[n].prefix, 0f, 0, 0, 0);
				slot += 1f;
			}
			for (int k2 = 0; k2 < NetItem.MiscDyeSlots; k2++)
			{
				NetMessage.SendData(5, -1, -1, NetworkText.FromLiteral(Main.player[player.Index].miscDyes[k2].Name), player.Index, slot, (float)Main.player[player.Index].miscDyes[k2].prefix, 0f, 0, 0, 0);
				slot += 1f;
			}
			for (int k3 = 0; k3 < NetItem.PiggySlots; k3++)
			{
				NetMessage.SendData(5, -1, -1, NetworkText.FromLiteral(Main.player[player.Index].bank.item[k3].Name), player.Index, slot, (float)Main.player[player.Index].bank.item[k3].prefix, 0f, 0, 0, 0);
				slot += 1f;
			}
			for (int k4 = 0; k4 < NetItem.SafeSlots; k4++)
			{
				NetMessage.SendData(5, -1, -1, NetworkText.FromLiteral(Main.player[player.Index].bank2.item[k4].Name), player.Index, slot, (float)Main.player[player.Index].bank2.item[k4].prefix, 0f, 0, 0, 0);
				slot += 1f;
			}
			int msgType = 5;
			int remoteClient = -1;
			int ignoreClient = -1;
			NetworkText text = NetworkText.FromLiteral(Main.player[player.Index].trashItem.Name);
			int index9 = player.Index;
			float num = slot;
			slot = num + 1f;
			NetMessage.SendData(msgType, remoteClient, ignoreClient, text, index9, num, (float)Main.player[player.Index].trashItem.prefix, 0f, 0, 0, 0);
			for (int k5 = 0; k5 < NetItem.ForgeSlots; k5++)
			{
				NetMessage.SendData(5, -1, -1, NetworkText.FromLiteral(Main.player[player.Index].bank3.item[k5].Name), player.Index, slot, (float)Main.player[player.Index].bank3.item[k5].prefix, 0f, 0, 0, 0);
				slot += 1f;
			}
			NetMessage.SendData(4, -1, -1, NetworkText.FromLiteral(player.Name), player.Index, 0f, 0f, 0f, 0, 0, 0);
			NetMessage.SendData(42, -1, -1, NetworkText.Empty, player.Index, 0f, 0f, 0f, 0, 0, 0);
			NetMessage.SendData(16, -1, -1, NetworkText.Empty, player.Index, 0f, 0f, 0f, 0, 0, 0);
			slot = 0f;
			for (int k6 = 0; k6 < NetItem.InventorySlots; k6++)
			{
				NetMessage.SendData(5, player.Index, -1, NetworkText.FromLiteral(Main.player[player.Index].inventory[k6].Name), player.Index, slot, (float)Main.player[player.Index].inventory[k6].prefix, 0f, 0, 0, 0);
				slot += 1f;
			}
			for (int k7 = 0; k7 < NetItem.ArmorSlots; k7++)
			{
				NetMessage.SendData(5, player.Index, -1, NetworkText.FromLiteral(Main.player[player.Index].armor[k7].Name), player.Index, slot, (float)Main.player[player.Index].armor[k7].prefix, 0f, 0, 0, 0);
				slot += 1f;
			}
			for (int k8 = 0; k8 < NetItem.DyeSlots; k8++)
			{
				NetMessage.SendData(5, player.Index, -1, NetworkText.FromLiteral(Main.player[player.Index].dye[k8].Name), player.Index, slot, (float)Main.player[player.Index].dye[k8].prefix, 0f, 0, 0, 0);
				slot += 1f;
			}
			for (int k9 = 0; k9 < NetItem.MiscEquipSlots; k9++)
			{
				NetMessage.SendData(5, player.Index, -1, NetworkText.FromLiteral(Main.player[player.Index].miscEquips[k9].Name), player.Index, slot, (float)Main.player[player.Index].miscEquips[k9].prefix, 0f, 0, 0, 0);
				slot += 1f;
			}
			for (int k10 = 0; k10 < NetItem.MiscDyeSlots; k10++)
			{
				NetMessage.SendData(5, player.Index, -1, NetworkText.FromLiteral(Main.player[player.Index].miscDyes[k10].Name), player.Index, slot, (float)Main.player[player.Index].miscDyes[k10].prefix, 0f, 0, 0, 0);
				slot += 1f;
			}
			for (int k11 = 0; k11 < NetItem.PiggySlots; k11++)
			{
				NetMessage.SendData(5, player.Index, -1, NetworkText.FromLiteral(Main.player[player.Index].bank.item[k11].Name), player.Index, slot, (float)Main.player[player.Index].bank.item[k11].prefix, 0f, 0, 0, 0);
				slot += 1f;
			}
			for (int k12 = 0; k12 < NetItem.SafeSlots; k12++)
			{
				NetMessage.SendData(5, player.Index, -1, NetworkText.FromLiteral(Main.player[player.Index].bank2.item[k12].Name), player.Index, slot, (float)Main.player[player.Index].bank2.item[k12].prefix, 0f, 0, 0, 0);
				slot += 1f;
			}
			int msgType2 = 5;
			int index10 = player.Index;
			int ignoreClient2 = -1;
			NetworkText text2 = NetworkText.FromLiteral(Main.player[player.Index].trashItem.Name);
			int index11 = player.Index;
			float num2 = slot;
			slot = num2 + 1f;
			NetMessage.SendData(msgType2, index10, ignoreClient2, text2, index11, num2, (float)Main.player[player.Index].trashItem.prefix, 0f, 0, 0, 0);
			for (int k13 = 0; k13 < NetItem.ForgeSlots; k13++)
			{
				NetMessage.SendData(5, player.Index, -1, NetworkText.FromLiteral(Main.player[player.Index].bank3.item[k13].Name), player.Index, slot, (float)Main.player[player.Index].bank3.item[k13].prefix, 0f, 0, 0, 0);
				slot += 1f;
			}
			NetMessage.SendData(4, player.Index, -1, NetworkText.FromLiteral(player.Name), player.Index, 0f, 0f, 0f, 0, 0, 0);
			NetMessage.SendData(42, player.Index, -1, NetworkText.Empty, player.Index, 0f, 0f, 0f, 0, 0, 0);
			NetMessage.SendData(16, player.Index, -1, NetworkText.Empty, player.Index, 0f, 0f, 0f, 0, 0, 0);
			for (int k14 = 0; k14 < 22; k14++)
			{
				player.TPlayer.buffType[k14] = 0;
			}
			NetMessage.SendData(50, -1, -1, NetworkText.Empty, player.Index, 0f, 0f, 0f, 0, 0, 0);
			NetMessage.SendData(50, player.Index, -1, NetworkText.Empty, player.Index, 0f, 0f, 0f, 0, 0, 0);
			NetMessage.SendData(76, player.Index, -1, NetworkText.Empty, player.Index, 0f, 0f, 0f, 0, 0, 0);
			NetMessage.SendData(76, -1, -1, NetworkText.Empty, player.Index, 0f, 0f, 0f, 0, 0, 0);
			NetMessage.SendData(39, player.Index, -1, NetworkText.Empty, 400, 0f, 0f, 0f, 0, 0, 0);
		}


		public void ApplyItemToPlrExceptExtra(TSPlayer player)
		{
			for (int j = 0; j < this.Items.Count; j++)
			{
				MiniItem item = this.Items[j];
				Item trItem = TShock.Utils.GetItemById(item.NetID);
				trItem.stack = item.Stack;
				trItem.prefix = item.Prefix;
				if (item.Slot >= 0 && item.Slot <= 58)
				{
					player.TPlayer.inventory[item.Slot] = trItem;
				}
				else if (item.Slot >= 59 && item.Slot <= 78)
				{
					player.TPlayer.armor[item.Slot - 59] = trItem;
				}
				else if (item.Slot >= 79 && item.Slot <= 88)
				{
					player.TPlayer.dye[item.Slot - 79] = trItem;
				}
				else if (item.Slot >= 89 && item.Slot <= 93)
				{
					player.TPlayer.miscEquips[item.Slot - 89] = trItem;
				}
				else if (item.Slot >= 94 && item.Slot <= 98)
				{
					player.TPlayer.miscDyes[item.Slot - 94] = trItem;
				}
				else
				{
					player.TPlayer.trashItem = trItem;
				}
			}
		}
		public void ApplyItemToPlrAll(TSPlayer player) 
		{
			for (int j = 0; j < this.Items.Count; j++)
			{
				MiniItem item = this.Items[j];
				Item trItem = TShock.Utils.GetItemById(item.NetID);
				trItem.stack = item.Stack;
				trItem.prefix = item.Prefix;
				if (item.Slot >= 0 && item.Slot <= 58)
				{
					player.TPlayer.inventory[item.Slot] = trItem;
				}
				else if (item.Slot >= 59 && item.Slot <= 78)
				{
					player.TPlayer.armor[item.Slot - 59] = trItem;
				}
				else if (item.Slot >= 79 && item.Slot <= 88)
				{
					player.TPlayer.dye[item.Slot - 79] = trItem;
				}
				else if (item.Slot >= 89 && item.Slot <= 93)
				{
					player.TPlayer.miscEquips[item.Slot - 89] = trItem;
				}
				else if (item.Slot >= 94 && item.Slot <= 98)
				{
					player.TPlayer.miscDyes[item.Slot - 94] = trItem;
				}
				else if (item.Slot >= 99 && item.Slot <= 138)
				{
					player.TPlayer.bank.item[item.Slot - 99] = trItem;
				}
				else if (item.Slot >= 139 && item.Slot <= 178)
				{
					player.TPlayer.bank2.item[item.Slot - 139] = trItem;
				}
				else if (item.Slot >= 180 && item.Slot <= 219)
				{
					player.TPlayer.bank3.item[item.Slot - 180] = trItem;
				}
				else if (item.Slot >= 220 && item.Slot <= 259)
				{
					player.TPlayer.bank4.item[item.Slot - 220] = trItem;
				}
				else
				{
					player.TPlayer.trashItem = trItem;
				}
			}


		}

		public void OnlySendInvData(TSPlayer player) 
		{
            for (int i = 0; i < NetItem.InventorySlots; i++)
            {
                player.SendData(PacketTypes.PlayerSlot, player.TPlayer.inventory[i].Name, player.Index, i, player.TPlayer.inventory[i].prefix);
                //NetMessage.SendData((int)PacketTypes.PlayerSlot, -1, -1, NetworkText.FromLiteral(player.TPlayer.inventory[i].Name),player.Index,i, player.TPlayer.inventory[i].prefix);
            }
			for (int i = 0; i < NetItem.ArmorSlots; i++)
			{
				player.SendData(PacketTypes.PlayerSlot, player.TPlayer.armor[i].Name, player.Index, i+59, player.TPlayer.armor[i].prefix);
				//NetMessage.SendData((int)PacketTypes.PlayerSlot, -1, -1, NetworkText.FromLiteral(player.TPlayer.armor[i].Name), player.Index, i+59, player.TPlayer.armor[i].prefix);
			}
			for (int i = 0; i < NetItem.DyeSlots; i++)
			{
				player.SendData(PacketTypes.PlayerSlot, player.TPlayer.dye[i].Name, player.Index, i+79, player.TPlayer.dye[i].prefix);
				//NetMessage.SendData((int)PacketTypes.PlayerSlot, -1, -1, NetworkText.FromLiteral(player.TPlayer.dye[i].Name), player.Index, i+79, player.TPlayer.dye[i].prefix);
			}
			for (int i = 0; i < NetItem.MiscDyeSlots; i++)
			{
				player.SendData(PacketTypes.PlayerSlot, player.TPlayer.miscDyes[i].Name, player.Index, i+94, player.TPlayer.miscDyes[i].prefix);
				//NetMessage.SendData((int)PacketTypes.PlayerSlot, -1, -1, NetworkText.FromLiteral(player.TPlayer.miscDyes[i].Name), player.Index, i+94, player.TPlayer.miscDyes[i].prefix);
			}
			for (int i = 0; i < NetItem.MiscEquipSlots; i++)
			{
				player.SendData(PacketTypes.PlayerSlot, player.TPlayer.miscEquips[i].Name, player.Index, i+89, player.TPlayer.miscEquips[i].prefix);
				//NetMessage.SendData((int)PacketTypes.PlayerSlot, -1, -1, NetworkText.FromLiteral(player.TPlayer.miscEquips[i].Name), player.Index, i+89, player.TPlayer.miscEquips[i].prefix);
			}
			player.SendData(PacketTypes.PlayerSlot, player.TPlayer.trashItem.Name, player.Index, 179, player.TPlayer.trashItem.prefix);
		}

		public void UpdatePlayerInv(TSPlayer player) 
		{
			NetDefaultsZeroExceptExtra(player);
			ApplyItemToPlrExceptExtra(player);
			OnlySendInvData(player);
		}

		public void UpdatePlayerInfo(TSPlayer player)
		{
			ApplyInfoToPlr(player);
			SendPlayerInfo(player);
		
		}

		public void SendPlayerInfo(TSPlayer player) 
		{
			NetMessage.SendData(4, -1, -1, NetworkText.FromLiteral(player.Name), player.Index, 0f, 0f, 0f, 0, 0, 0);
			NetMessage.SendData(42, -1, -1, NetworkText.Empty, player.Index, 0f, 0f, 0f, 0, 0, 0);
			NetMessage.SendData(16, -1, -1, NetworkText.Empty, player.Index, 0f, 0f, 0f, 0, 0, 0);
			NetMessage.SendData(4, player.Index, -1, NetworkText.FromLiteral(player.Name), player.Index, 0f, 0f, 0f, 0, 0, 0);
			NetMessage.SendData(42, player.Index, -1, NetworkText.Empty, player.Index, 0f, 0f, 0f, 0, 0, 0);
			NetMessage.SendData(16, player.Index, -1, NetworkText.Empty, player.Index, 0f, 0f, 0f, 0, 0, 0);
			NetMessage.SendData(50, -1, -1, NetworkText.Empty, player.Index, 0f, 0f, 0f, 0, 0, 0);
			NetMessage.SendData(50, player.Index, -1, NetworkText.Empty, player.Index, 0f, 0f, 0f, 0, 0, 0);
			NetMessage.SendData(76, player.Index, -1, NetworkText.Empty, player.Index, 0f, 0f, 0f, 0, 0, 0);
			NetMessage.SendData(76, -1, -1, NetworkText.Empty, player.Index, 0f, 0f, 0f, 0, 0, 0);
			NetMessage.SendData(39, player.Index, -1, NetworkText.Empty, 400, 0f, 0f, 0f, 0, 0, 0);
		}


		public void ApplyInfoToPlr(TSPlayer player) 
		{
			player.IgnoreSSCPackets = true;
			player.TPlayer.statLife = this.HP;
			player.TPlayer.statLifeMax = this.MaxHP;
			player.TPlayer.statMana = this.Mana;
			player.TPlayer.statManaMax = this.MaxMana;
			player.TPlayer.SpawnX = this.SpawnX;
			player.TPlayer.SpawnY = this.SpawnY;
			player.sX = this.SpawnX;
			player.sY = this.SpawnY;
			player.TPlayer.hairDye = this.HairDye;
			player.TPlayer.anglerQuestsFinished = this.QuestsCompleted;
			if (this.ExtraSlots != null)
			{
				player.TPlayer.extraAccessory = (this.ExtraSlots.Value == 1);
			}
			if (this.SkinVariant != null)
			{
				player.TPlayer.skinVariant = this.SkinVariant.Value;
			}
			if (this.Hair != null)
			{
				player.TPlayer.hair = this.Hair.Value;
			}
			if (this.HairColor != null)
			{
				player.TPlayer.hairColor = this.HairColor.Value;
			}
			if (this.PantsColor != null)
			{
				player.TPlayer.pantsColor = this.PantsColor.Value;
			}
			if (this.ShirtColor != null)
			{
				player.TPlayer.shirtColor = this.ShirtColor.Value;
			}
			if (this.UnderShirtColor != null)
			{
				player.TPlayer.underShirtColor = this.UnderShirtColor.Value;
			}
			if (this.ShoeColor != null)
			{
				player.TPlayer.shoeColor = this.ShoeColor.Value;
			}
			if (this.SkinColor != null)
			{
				player.TPlayer.skinColor = this.SkinColor.Value;
			}
			if (this.EyeColor != null)
			{
				player.TPlayer.eyeColor = this.EyeColor.Value;
			}
			if (this.HideVisuals != null)
			{
				player.TPlayer.hideVisibleAccessory = this.HideVisuals;
			}
			else
			{
				player.TPlayer.hideVisibleAccessory = new bool[player.TPlayer.hideVisibleAccessory.Length];
			}
		}



		public void NetDefaultsZeroAllInv(TSPlayer player)
		{
			for (int i = 0; i < NetItem.MaxInventory; i++)
			{
				if (i < 59)
				{
					player.TPlayer.inventory[i].netDefaults(0);
				}
				else if (i < 79)
				{
					int index = i - NetItem.ArmorIndex.Item1;
					player.TPlayer.armor[index].netDefaults(0);
				}
				else if (i < 89)
				{
					int index2 = i - NetItem.DyeIndex.Item1;
					player.TPlayer.dye[index2].netDefaults(0);
				}
				else if (i < 94)
				{
					int index3 = i - NetItem.MiscEquipIndex.Item1;
					player.TPlayer.miscEquips[index3].netDefaults(0);
				}
				else if (i < 99)
				{
					int index4 = i - NetItem.MiscDyeIndex.Item1;
					player.TPlayer.miscDyes[index4].netDefaults(0);
				}
				else if (i < 139)
				{
					int index5 = i - NetItem.PiggyIndex.Item1;
					player.TPlayer.bank.item[index5].netDefaults(0);
				}
				else if (i < 179)
				{
					int index6 = i - NetItem.SafeIndex.Item1;
					player.TPlayer.bank2.item[index6].netDefaults(0);
				}
				else if (i < 220)
				{
					if (i == 179)
					{
						player.TPlayer.trashItem.netDefaults(0);
					}
					else
					{
						int index7 = i - NetItem.ForgeIndex.Item1;
						player.TPlayer.bank3.item[index7].netDefaults(0);
					}
				}
				else
				{
					int index8 = i - NetItem.VoidIndex.Item1;
					player.TPlayer.bank4.item[index8].netDefaults(0);
				}
			}


		}
		public void NetDefaultsZeroExceptExtra(TSPlayer player) 
		{
			for (int i = 0; i < NetItem.MaxInventory; i++)
			{
				if (i < 59)
				{
					player.TPlayer.inventory[i].netDefaults(0);
				}
				else if (i < 79)
				{
					int index = i - NetItem.ArmorIndex.Item1;
					player.TPlayer.armor[index].netDefaults(0);
				}
				else if (i < 89)
				{
					int index2 = i - NetItem.DyeIndex.Item1;
					player.TPlayer.dye[index2].netDefaults(0);
				}
				else if (i < 94)
				{
					int index3 = i - NetItem.MiscEquipIndex.Item1;
					player.TPlayer.miscEquips[index3].netDefaults(0);
				}
				else if (i < 99)
				{
					int index4 = i - NetItem.MiscDyeIndex.Item1;
					player.TPlayer.miscDyes[index4].netDefaults(0);
				}
				else if (i == 179)
				{
					player.TPlayer.trashItem.netDefaults(0);
				}
			}
		}


		public void CopyFromPlayer(TSPlayer plr)
		{
			if (plr == null)
			{
				return;
			}
			this.CopyFromPlayer(plr.TPlayer);
		}

		
		public void CopyFromPlayer(Player plr)
		{
			if (plr == null)
			{
				return;
			}
			this.MaxHP = plr.statLifeMax;
			this.HP = plr.statLife;
			this.Mana = plr.statMana;
			this.MaxMana = plr.statManaMax;
			this.ExtraSlots = new int?(plr.extraAccessorySlots);
			this.EyeColor = new Color?(plr.eyeColor);
			this.Hair = new int?(plr.hair);
			this.HairColor = new Color?(plr.hairColor);
			this.HairDye = plr.hairDye;
			this.HappyFunTorchTime = plr.happyFunTorchTime.GetHashCode();
			this.HideVisuals = plr.hideVisibleAccessory;
			this.PantsColor = new Color?(plr.pantsColor);
			this.ShoeColor = new Color?(plr.shoeColor);
			this.SkinColor = new Color?(plr.skinColor);
			this.ShirtColor = new Color?(plr.shirtColor);
			this.SkinVariant = new int?(plr.skinVariant);
			this.SpawnX = plr.SpawnX;
			this.SpawnY = plr.SpawnY;
			this.UnderShirtColor = new Color?(plr.underShirtColor);
			this.UnlockedBiomeTorches = plr.unlockedBiomeTorches.GetHashCode();
			this.UsingBiomeTorches = plr.UsingBiomeTorches.GetHashCode();
			this.Exists = true;
			for (int i = 0; i < 59; i++)
			{
				Item tritem = plr.inventory[i];
				if (tritem.netID != 0)
				{
					MiniItem item = new MiniItem(i, tritem.prefix, tritem.netID, tritem.stack);
					this.Items.Add(item);
				}
			}
			for (int j = 0; j < NetItem.ArmorSlots; j++)
			{
				Item tritem2 = plr.armor[j];
				if (tritem2.netID != 0)
				{
					MiniItem item2 = new MiniItem(j + 59, tritem2.prefix, tritem2.netID, tritem2.stack);
					this.Items.Add(item2);
				}
			}
			for (int k = 0; k < NetItem.DyeSlots; k++)
			{
				Item tritem3 = plr.dye[k];
				if (tritem3.netID != 0)
				{
					MiniItem item3 = new MiniItem(k + 79, tritem3.prefix, tritem3.netID, tritem3.stack);
					this.Items.Add(item3);
				}
			}
			for (int l = 0; l < NetItem.MiscEquipSlots; l++)
			{
				Item tritem4 = plr.miscEquips[l];
				if (tritem4.netID != 0)
				{
					MiniItem item4 = new MiniItem(l + 89, tritem4.prefix, tritem4.netID, tritem4.stack);
					this.Items.Add(item4);
				}
			}
			for (int m = 0; m < NetItem.MiscDyeSlots; m++)
			{
				Item tritem5 = plr.miscDyes[m];
				if (tritem5.netID != 0)
				{
					MiniItem item5 = new MiniItem(m + 94, tritem5.prefix, tritem5.netID, tritem5.stack);
					this.Items.Add(item5);
				}
			}
			for (int n = 0; n < NetItem.PiggySlots; n++)
			{
				Item tritem6 = plr.bank.item[n];
				if (tritem6.netID != 0)
				{
					MiniItem item6 = new MiniItem(n + 99, tritem6.prefix, tritem6.netID, tritem6.stack);
					this.Items.Add(item6);
				}
			}
			for (int i2 = 0; i2 < NetItem.SafeSlots; i2++)
			{
				Item tritem7 = plr.bank2.item[i2];
				if (tritem7.netID != 0)
				{
					MiniItem item7 = new MiniItem(i2 + 139, tritem7.prefix, tritem7.netID, tritem7.stack);
					this.Items.Add(item7);
				}
			}
			for (int i3 = 0; i3 < NetItem.ForgeSlots; i3++)
			{
				Item tritem8 = plr.bank3.item[i3];
				if (tritem8.netID != 0)
				{
					MiniItem item8 = new MiniItem(i3 + 180, tritem8.prefix, tritem8.netID, tritem8.stack);
					this.Items.Add(item8);
				}
			}
			for (int i4 = 0; i4 < NetItem.VoidSlots; i4++)
			{
				Item tritem9 = plr.bank4.item[i4];
				if (tritem9.netID != 0)
				{
					MiniItem item9 = new MiniItem(i4 + 220, tritem9.prefix, tritem9.netID, tritem9.stack);
					this.Items.Add(item9);
				}
			}
			this.Items.Add(new MiniItem(179, plr.trashItem.prefix, plr.trashItem.netID, plr.trashItem.stack));
		}

		
		public void RestoreCharacter(MiniPlayer plr)
		{
			if (plr == null) return;
			this.RestoreCharacter(plr.Player);
		}

		
		public PlayerData TransToPlayerData()
		{
			return new PlayerData(new TSPlayer(255))
			{
				exists = this.Exists,
				extraSlot = this.ExtraSlots,
				eyeColor = this.EyeColor,
				hair = this.Hair,
				hairColor = this.HairColor,
				hairDye = this.HairDye,
				happyFunTorchTime = this.HappyFunTorchTime,
				health = this.HP,
				hideVisuals = this.HideVisuals,
				inventory = this.ItemsToOriginInv(),
				mana = this.Mana,
				maxMana = this.MaxMana,
				pantsColor = this.PantsColor,
				questsCompleted = this.QuestsCompleted,
				shirtColor = this.ShirtColor,
				shoeColor = this.ShoeColor,
				skinColor = this.SkinColor,
				skinVariant = this.SkinVariant,
				spawnX = this.SpawnX,
				spawnY = this.SpawnY,
				underShirtColor = this.UnderShirtColor,
				unlockedBiomeTorches = this.UnlockedBiomeTorches,
				usingBiomeTorches = this.UsingBiomeTorches
			};
		}

		
		public NetItem[] ItemsToOriginInv()
		{
			NetItem[] items = new NetItem[NetItem.MaxInventory];
			for (int i = 0; i < this.Items.Count; i++)
			{
				MiniItem item = this.Items[i];
				if (item.Slot == i)
				{
					items[i] = item.ToNetItem();
				}
			}
			return items;
		}
	}
}
