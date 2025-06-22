using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XChess.Model.Enums
{
    public enum GameStatus
    {
        Ongoing,
        WhiteWon,
        BlackWon,
        Draw,
        Timeout,
        Resigned
    }
}
