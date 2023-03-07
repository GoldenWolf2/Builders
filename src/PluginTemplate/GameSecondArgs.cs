using MiniGamesAPI.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniGamesAPI.Hooks
{
    public class GameSecondArgs
    {
        public MiniPlayer Player { get; set; }
        public GameSecondArgs(MiniPlayer player) { Player = player; }
    }
}
