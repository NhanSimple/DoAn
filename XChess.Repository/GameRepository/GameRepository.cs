using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XChess.Model.Entities;
using XChess.Repository.Common;
using XChess.Repository.UserRepository;

namespace XChess.Repository.GameRepository
{
    public class GameRepository : GenericRepository<Game>, IGameRepository
    {
        public GameRepository(DbContext context) : base(context)
        {

        }
        public Game GetById(int id)
        {
            return FindBy(x => x.Id == id).FirstOrDefault();
        }
    }
}
