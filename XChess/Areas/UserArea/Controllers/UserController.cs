using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XChess.Areas.UserArea.Model;
using XChess.Service.Common;
using XChess.Service.UserService;
using XChess.Model.Entities;

namespace XChess.Areas.UserArea.Controllers
{
    public class UserController : Controller
    {
        IUserService _userService;
        public UserController(IUserService userService) {
            _userService=userService;
        }
        // GET: UserArea/User
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Create()
        {
            var model = new CreateVM();
        
            return PartialView("_CreatePartial", model);
        }

        [HttpPost]
        public JsonResult Create(CreateVM model)
        {
            var result = new JsonResultBO(true, "tạo mới User thành công");
            try
            {
                if (ModelState.IsValid)
                {
                    var  user = new User();
                    //var EntityModel = model;
                    user.Username = model.Username;
                    user.PasswordHash = model.Password;
                    user.FirtName = model.FirtName;
                    user.Email = model.Email; 
                    user.LastName = model.LastName;
                    user.CreatedAt = DateTime.Now;
                    _userService.Create(user);
                }
            }
            catch (Exception ex)
            {
                result.MessageFail(ex.Message);
            }

            return Json(result);
        }
    }
}