using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TShockAPI;

namespace MiniGamesAPI.Core
{
    public class MiniItem
    {
        public int Slot { get; set; }
        public byte Prefix { get; set; }
        public int NetID { get; set; }
        public int Stack { get; set; }
        public MiniItem(int slot, byte prefix, int netid, int stack)
        {
            Slot = slot;
            Prefix = prefix;
            NetID = netid;
            Stack = stack;
        }
        public NetItem ToNetItem()
        {
            var item = new NetItem(NetID, Stack, Prefix);
            return item;
        }
    }
}
