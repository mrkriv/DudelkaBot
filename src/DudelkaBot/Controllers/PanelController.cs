using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DudelkaBot.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Text;
using System.Security.Cryptography;

namespace DudelkaBot.Controllers
{
    public class PanelController : ControllerBase
    {
        public PanelController(IOptions<Config> config) : base(config) { }

        public IActionResult Index()
        {
            var user = WebUsers.Get(HttpContext);

            if (user == null)
                return Redirect("/Panel/Login");

            ViewData["user"] = user;
            return View();
        }

        public IActionResult RegNew()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegNew(Login data)
        {
            if (data == null ||
                string.IsNullOrEmpty(data.Email) ||
                string.IsNullOrEmpty(data.Channel) ||
                string.IsNullOrEmpty(data.Password) ||
                string.IsNullOrEmpty(data.Password2))
                return View();

            if (data.Password != data.Password2)
            {
                ViewData["errorMessage"] = "Пароли не совподают";
                return View();
            }

            if (data.Password.Length < 5)
            {
                ViewData["errorMessage"] = "Пароль должен содержать как минимум 5 символов";
                return View();
            }

            bool success;
            int id = db.Get<int>("select id from users where email=@0", out success, data.Email);

            if (success)
            {
                ViewData["errorMessage"] = "Пользователь с такой почтой уже существует";
                return View();
            }

            db.Execute("insert into users (email, password, channel) values (@0, @1, @2)",
                data.Email,
                GetMD5(data.Password),
                data.Channel);

            return Login(data);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Login data)
        {
            if (data != null && data.Email != null && data.Password != null)
            {
                var result = db.Get<int, string>("select id, channel from users where email=@0 and password=@1", data.Email, GetMD5(data.Password));

                if (result != null)
                {
                    var user = WebUsers.Authorization(HttpContext, result.Item1, data.Email, result.Item2);

                    var cfg = db.Get<string, int, bool, bool, bool, bool>(
                        "select Brodcast, BrodcastTime, MSubscriderTime, MSubscriderMessage, MFolover, MViews from users where email=@0", data.Email);

                    user.BotConfig.Brodcast = cfg.Item1;
                    user.BotConfig.BrodcastTime = cfg.Item2;
                    user.BotConfig.MSubscriderTime = cfg.Item3;
                    user.BotConfig.MSubscriderMessage = cfg.Item4;
                    user.BotConfig.MFolover = cfg.Item5;
                    user.BotConfig.MViews = cfg.Item6;

                    return Redirect("/Panel/Index");
                }
            }

            ViewData["errorMessage"] = "Неверный логин или пароль";
            return View();
        }
        
        public IActionResult UnLogin()
        {
            WebUsers.UnLogin(HttpContext);
            return Redirect("/Panel/Index");
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}