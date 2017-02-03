using DudelkaBot.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace DudelkaBot
{
    public enum ListenerState
    {
        Run,
        Loading,
        Error,
        Stoped,
    }

    public class IRCClient
    {
        private static Regex userParser = new Regex(@"@(?<username>[\w]+).tmi.twitch.tv (?<type>[A-Z]+) #(?<channel>\w+) :\s*(?<msg>.*)");
        private static Regex systemParser = new Regex(@"tmi\.twitch\.tv( \d{3} \w+ :)?\s*(?<msg>.+)");
        private const int messageLimitCount = 19;
        private const int messageLimitTime = 30;

        private StreamReader sr;
        private StreamWriter sw;
        private TcpClient tcp;
        private bool needStop = false;
        private int messageCount;
        private ManualResetEvent initEvent;

        public ListenerState State { get; private set; }

        public IRCClient()
        {
            initEvent = new ManualResetEvent(false);
            tcp = new TcpClient(AddressFamily.InterNetwork);

            State = ListenerState.Stoped;
        }

        public void Run()
        {
            Console.WriteLine("Runing IRCClient...");
            State = ListenerState.Loading;

            tcp.ConnectAsync(Config.Host, Config.Port).Wait();
            if (!tcp.Connected)
            {
                Console.WriteLine("Error connect to {0}:{1}", Config.Host, Config.Port);
                State = ListenerState.Error;
                return;
            }

            var stream = tcp.GetStream();
            sr = new StreamReader(stream);
            sw = new StreamWriter(stream);

            sw.WriteLine("PASS oauth:{0}", Config.Token);
            sw.WriteLine("NICK {0}", Config.Login);
            sw.Flush();

            Task.Run((Action)resiveLoop);
            Task.Run((Action)timerLoop);

            initEvent.WaitOne();
            State = ListenerState.Run;

            Console.WriteLine("Listener ready");
        }

        private void resiveLoop()
        {
            while (!needStop)
            {
                string msg = sr.ReadLine();

                if (!string.IsNullOrEmpty(msg))
                    Task.Run(() => resiveMessage(msg));
            }
        }

        private void timerLoop()
        {
            while (!needStop)
            {
                Thread.Sleep(messageLimitTime * 1000);
                messageCount = 0;
            }
        }

        private void resiveMessage(string msg)
        {
            var m = userParser.Match(msg);
            if (m.Success)
            {
                string user = m.Groups["username"].Value;
                string channel = m.Groups["channel"].Value;
                msg = m.Groups["msg"].Value;

                //if (user == config.Login)
                //    return;

                Bot.Get(channel)?.ResiveMessage(user, msg);
            }
            else
            {
                m = systemParser.Match(msg);
                if (m.Success)
                {
                    msg = m.Groups["msg"].Value;

                    Console.WriteLine(string.Format("> IRC: {0}", msg));

                    if (msg == "Your host is tmi.twitch.tv")
                        initEvent.Set();
                    else if (msg.StartsWith("JOIN"))
                        Bot.Get(msg.Split('#')[1])?.ConfirmJoin();
                }
                else
                    Console.WriteLine(string.Format("Undefine IRC message: {0}", msg));
            }
        }

        public void Print(string ircMessage)
        {
            if (messageCount >= messageLimitCount)
                return;

            if (State != ListenerState.Run)
                return;

            sw.WriteLine(ircMessage);
            sw.Flush();

            messageCount++;
        }

        public void PrintToChannel(string channel, string format, params object[] args)
        {
            if (messageCount >= messageLimitCount)
                return;

            if (State != ListenerState.Run)
                return;

            string msg = string.Format(format, args);

            sw.WriteLine(":{0}!{0}@{0}.tmi.twitch.tv PRIVMSG #{1} :{2}", Config.Login, channel, msg);
            sw.Flush();

            messageCount++;
        }
    }
}