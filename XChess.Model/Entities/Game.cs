using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XChess.Model.Entities
{
    public class Game
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string Result { get; set; }
        public int IdWhitePlayer { get; set; }
        public int IdBlackPlayer { get; set; }
        public int IdWinner {  get; set; }

        public virtual ICollection<Move> Moves { get; set; }
    }
}
