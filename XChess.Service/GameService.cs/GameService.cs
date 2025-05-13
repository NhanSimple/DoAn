using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XChess.Model.Entities;
using XChess.Repository.Common;
using XChess.Repository.GameRepository;
using XChess.Service.Common;

namespace XChess.Service.GameService.cs
{
    public class GameService : EntityService<Game>, IGameService
    {
        IUnitOfWork _UnitOfWork;
        IGameRepository _GameRepository;
        public GameService(IUnitOfWork unitOfWork, IGameRepository gameRepository):base(unitOfWork, gameRepository)
        {
            _UnitOfWork=unitOfWork;
            _GameRepository=gameRepository; 
        }
        public Game GetById(int id)
        {
            return _GameRepository.GetAllAsQueryable().FirstOrDefault();
        }
    }
}
