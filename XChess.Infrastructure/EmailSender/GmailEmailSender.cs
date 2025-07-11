using System;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace XChess.Infrastructure.EmailSender
{
    public class GmailEmailSender : IEmailSender
    {
        private static readonly ConcurrentDictionary<string, VerificationCodeInfo> _verificationCodes
            = new ConcurrentDictionary<string, VerificationCodeInfo>();

        private readonly string _fromEmail = "phamchinhan333@gmail.com";
        private readonly string _fromName = "Xác minh tài khoản";
        private readonly string _smtpServer = "smtp.gmail.com";
        private readonly int _smtpPort = 587;
        private readonly string _smtpUsername = "phamchinhan333@gmail.com";
        private readonly string _smtpPassword = "kjfd mkfk kqyv uzrd"; // Gmail app password

        public string GenerateVerificationCode(string email)
        {
            CleanupExpiredCodes();
            using (var rng = RandomNumberGenerator.Create())
            {
                var bytes = new byte[4];
                rng.GetBytes(bytes);
                int code = Math.Abs(BitConverter.ToInt32(bytes, 0)) % 1000000;
                var resultCode = Math.Abs(code).ToString("D6");

                _verificationCodes[email] = new VerificationCodeInfo
                {
                    Code = resultCode,
                    GeneratedAt = DateTime.UtcNow
                };

                return resultCode;
            }
        }

        public async Task<string> SendVerificationEmailAsync(string toEmail, string verificationCode)
        {
            try
            {
                var message = new MailMessage
                {
                    From = new MailAddress(_fromEmail, _fromName),
                    Subject = "Mã xác minh tài khoản",
                    Body = $"Mã xác minh của bạn là: {verificationCode}. Mã có hiệu lực trong 1 phút.",
                    IsBodyHtml = false
                };
                message.To.Add(toEmail);

                using (var smtpClient = new SmtpClient(_smtpServer, _smtpPort))
                {
                    smtpClient.Credentials = new NetworkCredential(_smtpUsername, _smtpPassword);
                    smtpClient.EnableSsl = true;

                    await smtpClient.SendMailAsync(message);
                }

                return "Success";
            }
            catch (Exception ex)
            {
                return $"Lỗi gửi email: {ex.Message}";
            }
        }

        public bool VerifyCode(string email, string inputCode)
        {
            if (_verificationCodes.TryGetValue(email, out var info))
            {
                var expired = DateTime.UtcNow - info.GeneratedAt > TimeSpan.FromMinutes(1);
                if (expired) return false;

                return info.Code == inputCode;
            }

            return false;
        }

        private void CleanupExpiredCodes()
        {
            var now = DateTime.UtcNow;
            foreach (var kvp in _verificationCodes)
            {
                if ((now - kvp.Value.GeneratedAt) > TimeSpan.FromMinutes(5))
                {
                    _verificationCodes.TryRemove(kvp.Key, out _);
                }
            }
        }
    }
}
