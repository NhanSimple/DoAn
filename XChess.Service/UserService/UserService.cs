using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XChess.Model.Entities;
using XChess.Repository.Common;
using XChess.Repository.UserRepository;
using XChess.Service.Common;

namespace XChess.Service.UserService
{
    public class UserService : EntityService<User>, IUserService
    {
        IUserRepository _UserRepository;
        IUnitOfWork _UnitOfWork;
        public UserService(IUnitOfWork unitOfWork, IUserRepository userRepository):base(unitOfWork, userRepository)
        {
            _UserRepository = userRepository;
            _UnitOfWork = unitOfWork;
        }
        public User GetById(int id)
        {
           return _UserRepository.GetAllAsQueryable().FirstOrDefault();

           
        }
    }
}
