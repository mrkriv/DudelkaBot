using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DudelkaBot.Models
{
    public class Config
    {
        public static Config Instance;

        public CDataBase DataBase { get; set; }
        public CTwitch Twitch { get; set; }

        public class CDataBase
        {
            public string Host { get; set; }
            public uint Port { get; set; }
            public string Database { get; set; }
            public string Login { get; set; }
            public string Password { get; set; }
        }

        public class CTwitch
        {
            public string Login { get; set; }
            public string Token { get; set; }
            public string Host { get; set; }
            public int Port { get; set; }
        }
    }
}