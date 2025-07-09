using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XChess.Areas.GameRoomArea.Model;

namespace XChess.Areas.GameRoomArea.Controllers
{
    public class GameRoomController : Controller
    {
        // GET: GameRoom/GameRoom

        public ActionResult Index()
        {
            var player = new PlayerCardVM
            {
                UserName = "Lernath",
                AvatarUrl= "https://randomuser.me/api/portraits/men/31.jpg",
                IsReady = true,
                IsMe = true
            };
            return View(player);
        }
    }
}