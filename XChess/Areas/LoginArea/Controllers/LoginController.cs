using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XChess.Areas.LoginArea.Model;
using XChess.Service.Common;

namespace XChess.Areas.LoginArea.Controllers
{
    public class LoginController : Controller
    {
        // GET: LoginArea/Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            if (!Request.IsAjaxRequest())
            {
                return RedirectToAction("Index", "Home", new { area = "" }); // hoặc 404
            }
            return PartialView("_LoginPartial");
        }
        [HttpPost]
        public JsonResult Login(AccoutInfo accoutInfo)
        {
            var result = new JsonResultBO(true, "Đăng nhập thành công");
            //1 hàm check login ở đây

            //

            try
            {
                if (ModelState.IsValid)
                {
                    if (accoutInfo.userName == "abc" && accoutInfo.password == "123")
                    {
                        return Json(result);
                    }
                    else
                    {
                        throw new Exception("Sai tên đăng nhập hoặc mật khẩu!");
                    }
                }else
                {
                    throw new Exception("dữ liệu không hợp lệ");
                }
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                result.MessageFail(ex.Message);
                return Json(result);
            }


        }
    }
}