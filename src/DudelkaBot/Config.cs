using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DudelkaBot
{
    public class Config
    {
        private const string filename = "config.json";

        [JsonProperty("Login")]
        public static string Login { get; set; }
        [JsonProperty("Token")]
        public static string Token { get; set; }
        [JsonProperty("Host")]
        public static string Host { get; set; }
        [JsonProperty("Port")]
        public static int Port { get; set; }


        static Config()
        {
            if(!File.Exists(filename))
                throw new FileNotFoundException($"Нужен файл конфига '{filename}'");

            var jr = new JsonTextReader(new StringReader(File.ReadAllText(filename)));
            var serializer = new JsonSerializer();

            serializer.Deserialize<Config>(jr);
        }
    }
}