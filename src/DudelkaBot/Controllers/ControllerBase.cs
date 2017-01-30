using DudelkaBot.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text;

namespace DudelkaBot.Controllers
{
    public abstract class ControllerBase : Controller
    {
        private static bool isFirst = true;
        protected const string mySqlDateFormat = "yyyy-MM-dd HH:mm:ss";
        protected DataBaseInterface db;
        protected Config Config;

        public ControllerBase(IOptions<Config> config)
        {
            Config = config.Value;
            Config.Instance = Config;

            db = new DataBaseInterface(
                Config.DataBase.Host,
                Config.DataBase.Port,
                Config.DataBase.Database,
                Config.DataBase.Login,
                Config.DataBase.Password
            );

            if (isFirst)
            {
                OnFirstLoad();
                isFirst = false;
            }
        }

        virtual protected void OnFirstLoad()
        {
            foreach (var channel in db.GetArray<string>("select channel from users where botactive=1"))
                Bot.TwitchBot.Run(channel);
        }

        public static string GetMD5(string data)
        {
            StringBuilder sb = new StringBuilder();
            byte[] encoded = new UTF8Encoding().GetBytes(data);
            byte[] hashBytes = MD5.Create().ComputeHash(encoded);

            for (int i = 0; i < hashBytes.Length; i++)
                sb.Append(hashBytes[i].ToString("X2"));

            return sb.ToString();
        }
    }
}