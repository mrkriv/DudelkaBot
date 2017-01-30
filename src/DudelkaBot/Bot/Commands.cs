using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace DudelkaBot.Bot
{
    partial class CommandShell
    {
        static CommandShell()
        {
            commands = new Dictionary<string, CommandShell>();

            Add("help", (cl, username, args) =>
            {
                cl.Brodcast("Список команд: ");
                foreach (var cmd in commands)
                    cl.Brodcast("  !" + cmd.Key);
            });
        }
    }
}