using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XChess.Repository.Common;
using XChess.Model.Entities;
using System.Runtime.Remoting.Contexts;
using XChess.Model.Common;
namespace XChess.Repository.UserRepository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context)
        {   

        }
        public User GetById(int id)
        {
            return FindBy(x => x.Id == id).FirstOrDefault();
        }

        public string Helloworld()
        {
            return "hello world";
        }
        public bool ExistsByEmail(string email)
        {
            var rs= Any(x=>x.Email == email);
            return rs;
        }
        public bool TryGetByEmail(string email, out User user)
        {
            user = _entities.Set<User>().FirstOrDefault(u => u.Email == email);
            return user != null;
        }
    }
}
