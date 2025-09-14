using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using XChess.Service.EngineSessionService;

namespace XChess.Areas.Chess.Controllers
{
    public class ChessController:Controller
    {
        private readonly IEngineSessionService _engine;

        public ChessController(IEngineSessionService engine)
        {
            _engine = engine;
        }
        [HttpPost]
        public async Task<ActionResult> MakeMove(string fen, string move)
        {
            _engine.Create(123, 5);
            var result = await _engine.IsMoveLegalAsync(123, fen, move); // kiểm tra hợp lệ
            return Json(new { legal = result }); // trả về JSON
        }
    }
}