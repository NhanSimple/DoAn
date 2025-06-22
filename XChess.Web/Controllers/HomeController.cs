using System;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web.Mvc;
using XChess.Model;
using XChess.Repository;
using XChess.Repository.UserRepository;
using XChess.Service.GameTimerService;

namespace XChess.Controllers
{
    public class HomeController : Controller
    {
       private readonly IUserRepository _userRepository;
        public HomeController(IUserRepository UserRepository)
        {

            _userRepository = UserRepository;


        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult SendEmail()
        {

            if (ModelState.IsValid)
            {

                //var fromEmail = "phamchinhan555@gmail.com";
                //var fromPassword = "rkuw pqgp ruph bayr";
                //SendEmailMeThod(fromEmail, fromPassword , "Xin Chào", "đây là thư gửi từ dự án của tôi");
               
            }
            return View();
        }
        [HttpPost]
        //public ActionResult SendEmail(FormModel model)
        //{

        //    if (ModelState.IsValid)
        //    {

        //        SendEmailMeThod("phamchinhan555@gmail.com", "rkuw pqgp ruph bayr", model.TextInput, model.TextArea + _userRepository.Helloworld());

        //    }
        //    return View();
        //}

        public static void SendEmailMeThod(string fromEmail, string fromPassword, string subject, string body)
        {
            var toEmail = "phamchinhan333@gmail.com";
            //var fromEmail = "phamchinhan555@gmail.com";
            //var fromPassword = "rkuw pqgp ruph bayr"; // Không dùng password thật, dùng App Password!

            var smtp = new SmtpClient()
            {
                Host="smtp.gmail.com",
                Port =587,
                EnableSsl=true,
                Credentials = new NetworkCredential(fromEmail,fromPassword)
            };



            //var smtp = new SmtpClient
            //{
            //    Host = "smtp.gmail.com",
            //    Port = 587,
            //    EnableSsl = true,
            //    Credentials = new NetworkCredential(fromEmail, fromPassword)
            //};
            MailMessage mailMessage = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body
            };
            smtp.Send(mailMessage);
           
        }
    }



}