using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XChess.Areas.GameRoomArea.Model
{
    public class GameRoomVM
    {
        public string RoomId { get; set; } = string.Empty;
        public PlayerCardVM PlayerLeft { get; set; } 
        public PlayerCardVM PlayerRight { get; set; } 
        public bool IsMeLeft { get; set; }
    }
}