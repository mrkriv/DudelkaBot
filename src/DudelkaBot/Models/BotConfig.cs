using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DudelkaBot.Models
{
    public class BotConfig
    {
        public string Brodcast { get; set; }
        public int BrodcastTime { get; set; }
        public bool MSubscriderTime { get; set; }
        public bool MSubscriderMessage { get; set; }
        public bool MFolover { get; set; }
        public bool MViews { get; set; }
    }
}
