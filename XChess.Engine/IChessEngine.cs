using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XChess.Engine
{
    public interface IChessEngine
    {
        void Start();
        void Stop();
        Task<bool> IsMoveLegal(string fen, string moveUci);
        Task<string> GetBestMove(string fen, int depth = 15);
        void SendCommand(string command);
        Task<string> ReadOutputAsync();
    }
}
