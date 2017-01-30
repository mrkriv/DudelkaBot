using DudelkaBot.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DudelkaBot
{
    public class WebUsers
    {
        private static Dictionary<string, WebUsers> Users = new Dictionary<string, WebUsers>();
        private static string TokenMaster = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        private static string Cookie_TokenName = "auth_token";
        private static Random rand = new Random();

        public int ID { get; set; }
        public string Login { get; set; }
        public string Token { get; set; }
        public string Channel { get; set; }
        public readonly BotConfig BotConfig = new BotConfig();

        private WebUsers(int id, string login, string channel)
        {
            ID = id;
            Login = login;
            Channel = channel;

            var token = new char[24];

            for (int i = 0; i < token.Length; i++)
                token[i] = TokenMaster[rand.Next(token.Length)];

            Token = new string(token);
        }

        public static WebUsers Authorization(HttpContext context, int id, string login, string channel)
        {
            UnLogin(context);

            var user = new WebUsers(id, login, channel);

            context.Response.Cookies.Append(Cookie_TokenName, user.Token);

            Users.Add(user.Token, user);

            return user;
        }

        public static WebUsers Get(HttpContext context)
        {
            if (!context.Request.Cookies.ContainsKey(Cookie_TokenName))
                return null;

            var token = context.Request.Cookies[Cookie_TokenName];

            if (!Users.ContainsKey(token))
                return null;

            return Users[token];
        }

        public static void UnLogin(HttpContext context)
        {
            var user = Get(context);

            if (context.Request.Cookies.ContainsKey(Cookie_TokenName))
                context.Response.Cookies.Delete(Cookie_TokenName);

            if (user != null)
                Users.Remove(user.Token);
        }
    }
}