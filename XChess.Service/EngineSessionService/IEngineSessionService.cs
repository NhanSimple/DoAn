using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XChess.Model.RealTimeModel;
using XChess.Service.Common;

namespace XChess.Service.EngineSessionService
{
    public interface IEngineSessionService : IGenericStoreService<EngineSession>
    {
        EngineSession Create(long matchId , int level);
        Task<string> GetBestMoveAsync(long matchId, string fen);
        Task<bool> IsMoveLegalAsync(long matchId, string fen, string moveUci);
        Task<bool> ApplyMoveAsync(long matchId, string fen, string moveUci);
        void Remove(long matchId);
        bool TryGet(string id, out EngineSession session);
    }
}
