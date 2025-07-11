﻿using System.Security.Policy;
using System.Web.Mvc;

namespace XChess.Areas.UserArea
{
    public class UserAreaAreaRegistration : AreaRegistration
    {

        public override string AreaName
        {
            get
            {
                return "UserArea";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute("UserArea_default",
                            "UserArea/{controller}/{action}/{id}",
                            new {action= "Index" ,id=UrlParameter.Optional}
                );
        }
        //    public override string AreaName 
        //    {
        //        get 
        //        {
        //            return "UserArea";
        //        }
        //    }

        //    public override void RegisterArea(AreaRegistrationContext context) 
        //    {
        //        context.MapRoute(
        //            "UserArea_default",
        //            "UserArea/{controller}/{action}/{id}",
        //            new { action = "Index", id = UrlParameter.Optional }
        //        );
        //    }
        //}

    }
}