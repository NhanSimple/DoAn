using System.Web.Mvc;

namespace XChess.Areas.LoginArea
{
    public class LoginAreaRegistration : AreaRegistration 
    {
        public override string AreaName => "LoginArea";

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "LoginArea_default",
                "LoginArea/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}