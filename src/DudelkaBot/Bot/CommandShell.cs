using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace DudelkaBot.Bot
{
    public partial class CommandShell
    {
        public delegate void EvalDelegate(TwitchBot cl, string username, string args);

        private static Dictionary<string, CommandShell> commands;
        private EvalDelegate evalEvent;

        public string Name { get; set; }
        
        public CommandShell(string name, EvalDelegate Event)
        {
            evalEvent = Event;
            Name = name;
        }

        public static void Add(string name, EvalDelegate @Event)
        {
            var cmd = new CommandShell(name, Event);
            commands.Add(name, cmd);
        }

        public static bool Eval(TwitchBot cl, string username, string cmd)
        {
            var sp = cmd.Split(" ".ToArray(), 2);
            cmd = sp[0].ToLower();

            if (!commands.ContainsKey(cmd))
                return false;

            commands[cmd].evalEvent(cl, username, sp.Length == 2 ? sp[1] : "");
            return true;
        }
    }
}