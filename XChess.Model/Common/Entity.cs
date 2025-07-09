using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XChess.Model.Common
{

    public abstract class Entity<T> : IEntity<T>
    {
        public virtual T Id { get; set; }
    }
}
