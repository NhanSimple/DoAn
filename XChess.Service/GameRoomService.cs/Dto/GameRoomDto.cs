using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace XChess.Service.GameRoomService
{
    public class GameRoomDto
    {
        public string RoomId { get; set; }

        public long? WhitePlayer { get; set; }
        public long? BlackPlayer { get; set; }

    }   
}
