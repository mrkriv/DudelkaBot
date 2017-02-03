using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using DudelkaBot.Models;
using System.Data.SqlClient;
using System.Data.Common;
using System.Text;

namespace DudelkaBot
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CultureInfo.CurrentCulture = new CultureInfo("en-US");
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("   ╦╗ ║║ ╦╗ ╔═ ║ ║╔ ╔╗   ╦╗ ╔╗ ╦ \n\r" +
                              "   ║║ ║║ ║║ ╠═ ║ ╠╣ ╠╣   ╠╣ ║║ ║ \n\r" +
                              "   ╩╝ ╚╝ ╩╝ ╚═ ╚ ║╚ ║║   ╩╝ ╚╝ ║ \n\r");

            try
            {
                var db = new DudelkaContext();

                foreach (var channel in db.Channels.Select(c => c.Name))
                    Bot.Run(db, channel);

                Console.WriteLine("Я родился!");

                while (true)
                {
                    var cmd = Console.ReadLine().Split(" ".ToArray(), 2);
                    switch (cmd[0])
                    {
                        case "add":
                        case "добавить":
                            var ch = new Channel() { Name = cmd[1] };
                            db.Channels.Add(ch);
                            db.SaveChanges();
                            Bot.Run(db, ch.Name);
                            break;
                        default:
                            Console.WriteLine("Not define command.");
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e);
            }
        }
    }
}