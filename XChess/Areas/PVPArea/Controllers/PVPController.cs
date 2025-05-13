using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using XChess.Service.UserService;

namespace XChess.Areas.PVPArea.Controllers
{
    public class PVPController : Controller
    {

        public PVPController()
        {
           
        }
        public ActionResult Index()
        {
            return View();
        }
    }
}