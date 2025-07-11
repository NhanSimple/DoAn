using System.Threading.Tasks;

namespace XChess.Infrastructure.EmailSender
{
    public interface IEmailSender
    {
        string GenerateVerificationCode(string email);
        bool VerifyCode(string email, string inputCode);
        Task<string> SendVerificationEmailAsync(string toEmail, string verificationCode);
    }
}