using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XChess.Infrastructure.StockfishEngine
{
   public interface IStockfishEngine : IDisposable
    {
        void Start();
        void SendCommand(string command);
        Task<string> ReadOutputAsync();
        Task<bool> IsMoveLegal(string fen, string moveUci);
        Task<string> GetBestMove(string fen, int depth = -1);
        Task<bool> ApplyMoveAsync(string fen, string moveUci);
        void Stop();
    }
}
