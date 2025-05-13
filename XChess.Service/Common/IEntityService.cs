using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace XChess.Service.Common
{
    public interface IEntityService<T> : IService where T : class
    {
        void Create(T entity);
        void Delete(T entity);
        void SoftDelete(T entity);
        void DeleteRange(IEnumerable<T> entities);

        void InsertRange(IEnumerable<T> entities);

        IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);
        IEnumerable<T> GetAll();
        void Update(T entity);
        T GetById(object id);
        T GetEmptyIfNullById(object id);
        void Save(T entity);
    }
}
