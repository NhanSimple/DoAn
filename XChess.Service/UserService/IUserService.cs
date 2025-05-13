using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XChess.Service.Common;
using XChess.Model.Entities;

namespace XChess.Service.UserService
{
    public  interface IUserService:IEntityService<User>
    {
        User GetById(int id);

    }
}
