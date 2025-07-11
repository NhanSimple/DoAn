using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace XChess.Areas.PlayerArea.Controllers
{
    public class PlayerController : Controller
    {
        // GET: PlayerArea/Player
        public ActionResult Index()
        {
            return View();
        }
    }
}