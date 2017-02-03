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

namespace DudelkaBot
{
    public enum BotState
    {
        None,
        Run,
        Loading,
        Error,
        Stoped,
    }

    public class Bot
    {
        private static Dictionary<string, Bot> instances = new Dictionary<string, Bot>();
        private static IRCClient Listener;
        
        public BotState State { get; private set; }
        public readonly string Channel;

        private Bot(DudelkaContext db, string channel)
        {
            Channel = channel;

            if (Listener == null)
            {
                Listener = new IRCClient();
                Listener.Run();
            }
        }

        public static Bot Get(string channel)
        {
            if (instances.ContainsKey(channel))
                return instances[channel];

            return null;
        }

        public static void Run(DudelkaContext db, string channel)
        {
            if (instances.ContainsKey(channel))
                return;

            var bot = new Bot(db, channel);
            instances.Add(channel, bot);

            Console.WriteLine(string.Format("Try join to channel '{0}'", channel));
            Listener.Print("JOIN #" + channel);
        }

        public void ResiveMessage(string user, string msg)
        {
            Console.WriteLine(string.Format("> [{2}][{0}]: {1}", user, msg, Channel));

            if (msg.StartsWith("!"))
            {
               // if (!CommandShell.Eval(this, user, msg.Substring(1, msg.Length - 1)))
               //     Brodcast("Такой команды нет");
            }
        }

        public void ConfirmJoin()
        {
            State = BotState.Run;
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