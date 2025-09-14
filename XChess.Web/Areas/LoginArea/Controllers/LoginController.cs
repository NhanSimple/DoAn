using AutoMapper;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using XChess.Areas.LoginArea.Model;
using XChess.Infrastructure.EmailSender;
using XChess.Infrastructure.PasswordHasher;
using XChess.Model.Entities;
using XChess.Service.UserService;

namespace XChess.Areas.LoginArea.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly IUserService _UserService;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher _PassworHasher;
        private readonly IEmailSender _EmailSender;

        public LoginController(IUserService userService, IMapper mapper, IPasswordHasher passwordHasher, IEmailSender emailSender)
        {
            _UserService = userService;
            _mapper = mapper;
            _PassworHasher = passwordHasher;
            _EmailSender = emailSender;
        }

        public ActionResult Index() => View();

        [HttpPost]
        public ActionResult Login( AuthPageVM vm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Error = "Dữ liệu không hợp lệ!";
                return View("Index");
            }

            var isVl = _UserService.TryGetByEmail(vm.Login.Email, out User user);
            if (!isVl)
            {
                ViewBag.Error = "Sai tên đăng nhập !";
                return View("Index");
            }

            var isValidPassword = _PassworHasher.Verify(vm.Login.Password, user.PasswordHash);
            if (!isValidPassword)
            {
                ViewBag.Error = "Sai mật khẩu!";
                return View("Index");
            }
            var userDataJson = JsonConvert.SerializeObject(new
            {
                Id = user.Id,
                Role = user.UserRoles,
                Name = user.UserName
            });
            FormsAuthentication.SetAuthCookie(userDataJson, false);
            return RedirectToAction("Index", "Player", new { area = "PlayerArea" });
        }

        [HttpPost]
        public async Task<JsonResult> Register(AuthPageVM vm)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, message = "Dữ liệu không hợp lệ" });

            if (_UserService.IsEmailTaken(vm.Register.Email))
                return Json(new { success = false, message = "Email đã tồn tại!" });

            var code = _EmailSender.GenerateVerificationCode(vm.Register.Email);
            var result = await _EmailSender.SendVerificationEmailAsync(vm.Register.Email, code);

            return Json(new
            {
                success = result == "Success",
                message = result == "Success" ? "Mã xác minh đã được gửi!" : result
            });
        }

        [HttpPost]
        public JsonResult VerifyCode(AuthPageVM vm)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, message = "Dữ liệu không hợp lệ" });

            if (!_EmailSender.VerifyCode(vm.Register.Email, vm.VerifyCode.Code))
                return Json(new { success = false, message = "Mã xác minh không đúng!" });

            try
            {
                var user = _mapper.Map<User>(vm.Register);
                user.PasswordHash = _PassworHasher.Hash(vm.Register.Password);
                _UserService.Create(user);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<JsonResult> ForgotPassword(AuthPageVM vm)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, message = "Dữ liệu không hợp lệ" });

            if (!_UserService.IsEmailTaken(vm.ForgotPassword.Email))
                return Json(new { success = false, message = "Email không tồn tại!" });

            var code = _EmailSender.GenerateVerificationCode(vm.ForgotPassword.Email);
            var result = await _EmailSender.SendVerificationEmailAsync(vm.ForgotPassword.Email, code);

            return Json(new
            {
                success = result == "Success",
                message = result == "Success" ? "Mã xác minh đã được gửi!" : result
            });
        }

        [HttpPost]
        public JsonResult ConfirmReset(AuthPageVM vm)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, message = "Dữ liệu không hợp lệ" });

            if (!_EmailSender.VerifyCode(vm.ForgotPassword.Email, vm.RsPassCode.Code))
                return Json(new { success = false, message = "Mã xác minh không đúng!" });

            if (!_UserService.TryGetByEmail(vm.ForgotPassword.Email, out var user))
                return Json(new { success = false, message = "Email không tồn tại!" });

            user.PasswordHash = _PassworHasher.Hash(vm.ForgotPassword.Password);
            _UserService.Update(user);

            return Json(new { success = true });
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }
    }
}
