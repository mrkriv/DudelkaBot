using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using DudelkaBot.Models;

namespace DudelkaBot.Bot
{
    public enum BotState
    {
        None,
        Run,
        Loading,
        Error,
        Stoped,
    }

    public class TwitchBot
    {
        private static Dictionary<string, TwitchBot> instances = new Dictionary<string, TwitchBot>();
        private static Listener Listener;
        
        private Config.CTwitch config;

        public BotState State { get; private set; }
        public readonly string Channel;

        private TwitchBot(string channel)
        {
            Channel = channel;
            config = Config.Instance.Twitch;

            if (Listener == null)
            {
                Listener = new Listener(config);
                Listener.Run();
            }
        }

        public static TwitchBot Get(string channel)
        {
            if (instances.ContainsKey(channel))
                return instances[channel];

            return null;
        }

        public static void Run(string channel)
        {
            if (instances.ContainsKey(channel))
                return;

            var bot = new TwitchBot(channel);
            instances.Add(channel, bot);

            Console.WriteLine("Try join to channel '{0}'", channel);
        }

        public void ResiveMessage(string user, string msg)
        {
            if (msg.StartsWith("!"))
            {
                if (!CommandShell.Eval(this, user, msg.Substring(1, msg.Length - 1)))
                    Brodcast("Такой команды нет");
            }
        }

        public void ResiveSystemMessage(string msg)
        {

        }

        public void Brodcast(string format, params object[] args)
        {
            Listener.PrintToChannel(Channel, format, args);
        }

        public void Stop()
        {
            instances.Remove(Channel);
            State = BotState.Stoped;
        }
    }
}