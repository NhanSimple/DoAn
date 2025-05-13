using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XChess.Model.Entities;
using XChess.Repository.Common;

namespace XChess.Repository.GameRepository
{
    public interface IGameRepository:IGenericRepository<Game>
    {
        Game GetById(int id);
    }
}
