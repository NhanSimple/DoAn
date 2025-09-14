using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XChess.Infrastructure.StockfishEngine;
namespace XChess.Model.RealTimeModel
{
    public class EngineSession
    {
        public long MatchId { get; set; }
        public int Level { get; set; }
        public IStockfishEngine Engine { get; set; }
    }
}
