using MiniGamesAPI.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniGamesAPI.Hooks
{
    public static class HookManager
    {
        public delegate void GameSecondD(GameSecondArgs args);
        public delegate void JoinRoomD(JoinRoomArgs args);
        public delegate void LeaveRoomD(LeaveRoomArgs args);
        public static GameSecondD GameSecond;
        public static JoinRoomD JoinRoom;
        public static LeaveRoomD LeaveRoom;


        public static void OnGameSecond(MiniPlayer player)
        {
            if (GameSecond == null) return;
            GameSecond(new GameSecondArgs(player));
        }
    }
}
