using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XChess.Model.Entities;
using XChess.Service.Common;

namespace XChess.Service.GameService.cs
{
    public  interface IGameService:IEntityService<Game>
    {
        Game GetById(int id);
    }
}
