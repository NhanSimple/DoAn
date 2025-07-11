using XChess.Repository.Common;
using XChess.Model.Entities;

namespace XChess.Repository.UserRepository
{
    public interface IUserRepository:IGenericRepository<User>
    {
        User GetById(int id);
        string Helloworld();
        bool ExistsByEmail(string email);
        bool TryGetByEmail(string email, out User user);
    }
}
