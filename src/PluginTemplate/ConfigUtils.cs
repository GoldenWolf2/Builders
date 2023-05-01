using System;
using System.Collections.Generic;
using System.IO;
using MiniGamesAPI.Core;
using Newtonsoft.Json;
using TShockAPI;

namespace BuildMaster
{
    public static class ConfigUtils
    {
        public static void LoadConfig()
        {
            if (!Directory.Exists(ConfigUtils.configDir))
            {
                Directory.CreateDirectory(ConfigUtils.configDir);
                File.WriteAllText(ConfigUtils.roomsPath, JsonConvert.SerializeObject(ConfigUtils.rooms, 1));
                File.WriteAllText(ConfigUtils.evaluatePath, JsonConvert.SerializeObject(ConfigUtils.evaluatePack, 1));
                File.WriteAllText(ConfigUtils.defaultPath, JsonConvert.SerializeObject(ConfigUtils.defaultPack, 1));
                File.WriteAllText(ConfigUtils.configPath, JsonConvert.SerializeObject(ConfigUtils.config, 1));
                return;
            }
            if (File.Exists(ConfigUtils.roomsPath))
            {
                ConfigUtils.rooms = JsonConvert.DeserializeObject<List<BuildRoom>>(File.ReadAllText(ConfigUtils.roomsPath));
            }
            else
            {
                File.WriteAllText(ConfigUtils.roomsPath, JsonConvert.SerializeObject(ConfigUtils.rooms, 1));
            }
            if (File.Exists(ConfigUtils.evaluatePath))
            {
                ConfigUtils.evaluatePack = JsonConvert.DeserializeObject<MiniPack>(File.ReadAllText(ConfigUtils.evaluatePath));
            }
            else
            {
                File.WriteAllText(ConfigUtils.evaluatePath, JsonConvert.SerializeObject(ConfigUtils.evaluatePack, 1));
            }
            if (File.Exists(ConfigUtils.defaultPath))
            {
                ConfigUtils.defaultPack = JsonConvert.DeserializeObject<MiniPack>(File.ReadAllText(ConfigUtils.defaultPath));
            }
            else
            {
                File.WriteAllText(ConfigUtils.defaultPath, JsonConvert.SerializeObject(ConfigUtils.defaultPack, 1));
            }
            if (File.Exists(ConfigUtils.configPath))
            {
                ConfigUtils.config = JsonConvert.DeserializeObject<BuildConfig>(File.ReadAllText(ConfigUtils.configPath));
                return;
            }
            File.WriteAllText(ConfigUtils.configPath, JsonConvert.SerializeObject(ConfigUtils.config, 1));
        }

        public static void UpdateRooms(int id)
        {
            List<BuildRoom> list = JsonConvert.DeserializeObject<List<BuildRoom>>(File.ReadAllText(ConfigUtils.roomsPath));
            int num = list.FindIndex((BuildRoom r) => r.ID == id);
            list[num] = ConfigUtils.rooms.Find((BuildRoom r) => r.ID == id);
            File.WriteAllText(ConfigUtils.roomsPath, JsonConvert.SerializeObject(ConfigUtils.rooms, 1));
        }

        public static void AddRoom(MiniRoom room)
        {
            JsonConvert.DeserializeObject<List<BuildRoom>>(File.ReadAllText(ConfigUtils.roomsPath)).Add(room);
            File.WriteAllText(ConfigUtils.roomsPath, JsonConvert.SerializeObject(ConfigUtils.rooms, 1));
        }

        public static void RemoveRoom(int id)
        {
            JsonConvert.DeserializeObject<List<BuildRoom>>(File.ReadAllText(ConfigUtils.roomsPath)).RemoveAll((BuildRoom r) => r.ID == id);
            File.WriteAllText(ConfigUtils.roomsPath, JsonConvert.SerializeObject(ConfigUtils.rooms, 1));
        }

        public static MiniRoom GetRoomByID(int id)
        {
            return ConfigUtils.rooms.Find((BuildRoom r) => r.ID == id);
        }

        public static MiniRoom GetRoomByIDFromLocal(int id)
        {
            return JsonConvert.DeserializeObject<List<BuildRoom>>(File.ReadAllText(ConfigUtils.roomsPath)).Find((BuildRoom r) => r.ID == id);
        }

        public static BuildPlayer GetPlayerByName(string name)
        {
            return ConfigUtils.players.Find((BuildPlayer p) => p.Name == name);
        }

        public static void UpdatePack()
        {
            File.WriteAllText(ConfigUtils.defaultPath, JsonConvert.SerializeObject(ConfigUtils.defaultPack, 1));
            File.WriteAllText(ConfigUtils.evaluatePath, JsonConvert.SerializeObject(ConfigUtils.evaluatePack, 1));
        }

        public static void ReloadConfig()
        {
            ConfigUtils.config = JsonConvert.DeserializeObject<BuildConfig>(File.ReadAllText(ConfigUtils.configPath));
        }

        public static readonly string configDir = TShock.SavePath + "/BuildMaster";
        public static readonly string roomsPath = ConfigUtils.configDir + "/rooms.json";
        public static readonly string evaluatePath = ConfigUtils.configDir + "/eva.json";
        public static readonly string defaultPath = ConfigUtils.configDir + "/default.json";
        public static readonly string configPath = ConfigUtils.configDir + "/config.json";
        public static List<MiniRoom> rooms = new List<Miniroom>();
        public static List<MiniPlayer> players = new List<MiniPlayer>();
        public static MiniPack defaultPack = new MiniPack("基础套", 2);
        public static MiniPack evaluatePack = new MiniPack("评分套", 3);
        public static BuildConfig config = new BuildConfig();
    }
}
