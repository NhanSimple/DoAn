using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XChess.Infrastructure.StockfishEngine;
using XChess.Model.RealTimeModel;
using XChess.Service.Common;
using XChess.Store.Common;
using XChess.Store.EngineSessionStore;
namespace XChess.Service.EngineSessionService
{
    public class EngineSessionService : GenericStoreService<EngineSession>, IEngineSessionService
    {
        private readonly IEngineSessionStore _engineSessionStore;
        private readonly Func<IStockfishEngine> _engineFactory;

        public EngineSessionService(IEngineSessionStore engineSessionStore, Func<IStockfishEngine> engineFactory)
            : base(engineSessionStore)
        {
            _engineSessionStore = engineSessionStore;
            _engineFactory = engineFactory;
        }

        public EngineSession Create(long matchId, int level)
        {
            // Tạo mới session và khởi động engine riêng
            var session = new EngineSession
            {
                MatchId = matchId,
                Level = level,
                Engine = CreateNewEngine()
            };

            session.Engine.Start();

            TryAdd(matchId.ToString(), session);

            return session;
        }

        private IStockfishEngine CreateNewEngine()
        {
            // Tạo instance mới StockfishEngine - nếu factory đơn giản hoặc từ DI
            return _engineFactory();
        }

        public async Task<string> GetBestMoveAsync(long matchId, string fen)
        {
            if (TryGet(matchId.ToString(), out var session))
            {
                return await session.Engine.GetBestMove(fen);
            }
            return null;
        }

        public async Task<bool> IsMoveLegalAsync(long matchId, string fen, string moveUci)
        {
            if (TryGet(matchId.ToString(), out var session))
            {
                return await session.Engine.IsMoveLegal(fen, moveUci);
            }
            return false;
        }

        public async Task<bool> ApplyMoveAsync(long matchId, string fen, string moveUci)
        {
            if (TryGet(matchId.ToString(), out var session))
            {
                return await session.Engine.ApplyMoveAsync(fen, moveUci);
            }
            return false;
        }

        public void Remove(long matchId)
        {
            var id = matchId.ToString();
            if (TryGet(id, out var session))
            {
                session.Engine.Stop();
                session.Engine.Dispose();
                TryRemove(id);
            }
        }
        public bool TryGet(string id, out EngineSession session)
        {
            // Giả sử _store là IGenericStore<EngineSession> và có TryGet method
            return _engineSessionStore.TryGet(id, out session);
        }
    }

}

