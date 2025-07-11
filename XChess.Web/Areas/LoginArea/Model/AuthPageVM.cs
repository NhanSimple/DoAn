using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XChess.Areas.LoginArea.Model
{
    public class AuthPageVM
    {
        public LoginVM Login { get; set; }
        public RegisterVM Register { get; set; }
        public VerifyCodeVM VerifyCode { get; set; }
        public ForgotPasswordVM ForgotPassword { get; set; }
        public RsPassCodeVM RsPassCode { get; set; }
    }
}