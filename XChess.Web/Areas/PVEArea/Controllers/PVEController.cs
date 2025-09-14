using System.Threading.Tasks;
using System.Web.Mvc;
using XChess.Model.Enums;
using XChess.Service.ChessMatchService;

namespace XChess.Areas.PVEArea.Controllers
{
    public class PVEController : Controller
    {
        private readonly IChessMatchService _chessMatchService;

        public PVEController(IChessMatchService chessMatchService)
        {
            _chessMatchService = chessMatchService;
        }

        // GET: /PVEArea/PVE
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        // Khởi tạo trận PvE – trả về matchId
        [HttpPost]
        public ActionResult StartWithAI(int level, string color)
        {
            var playerColor = color == "white" ? PlayerColor.White : PlayerColor.Black;
            long matchId = _chessMatchService.StartWithAI(level, playerColor, 0);
            return Json(matchId);
        }

        // Máy tính nước đi – nhận FEN hiện tại, trả JSON {fromRow, fromCol, toRow, toCol, promotion}
        [HttpPost]
        public async Task<ActionResult> MakeMoveAI(long matchId, string fen)
        {
            var aiMove = await _chessMatchService.ComputeAIMoveAsync(matchId, fen);
            return Json(aiMove);
        }
    }
}
