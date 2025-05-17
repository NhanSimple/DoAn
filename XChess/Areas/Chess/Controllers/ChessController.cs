using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using XChess.Engine;

namespace XChess.Areas.Chess.Controllers
{
    public class ChessController:Controller
    {
        private readonly IChessEngine _engine;

        public ChessController(IChessEngine engine)
        {
            _engine = engine;
        }
        [HttpPost]
        public async Task<ActionResult> MakeMove(string fen, string move)
        {
            _engine.Start();
            var result= await _engine.IsMoveLegal(fen, move);
            _engine.Stop();
            return Json(new { legal = result });
        }
    }
}