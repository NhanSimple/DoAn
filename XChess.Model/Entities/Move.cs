using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XChess.Model.Entities
{
    public class Move
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public int MoveNumber { get; set; }
        public string From { get; set; } 
        public string To { get; set; }
    }
}
