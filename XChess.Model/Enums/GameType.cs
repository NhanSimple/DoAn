using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XChess.Model.Enums
{
    public enum GameType
    {
        PvP,        // Player vs Player (dù là guest hay user)
        PvE,        // Player vs Engine (vs Bot, ví dụ Stockfish)
        Tutorial    // Chế độ hướng dẫn / tập luyện
    }
}
