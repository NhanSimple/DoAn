using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XChess.Areas.GameRoomArea.Model
{
    public class PlayerCardVM
    {
        public string UserName { get; set; } = string.Empty;
        public string AvatarUrl { get; set; } = "/Content/Images/anhtest.jpg";
        public bool IsReady { get; set; }
        public bool IsMe { get; set; }
    }
}