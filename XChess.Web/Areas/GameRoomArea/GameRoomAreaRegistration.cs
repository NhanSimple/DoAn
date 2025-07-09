using System.Web.Mvc;

namespace XChess.Areas.GameRoomArea
{
    public class GameRoomAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "GameRoomArea";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "GameRoomArea_default",
                "GameRoomArea/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}