using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
namespace XChess.Repository.Common
{
    public interface IGenericRepository<T> where T : class
    {
        DbContext GetContext();
        IDbSet<T> DBSet();
        T Add(T entity);
        T Delete(T entity);
        IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetAllAsQueryable();
        IEnumerable<T> GetAll();
        void SoftDelete(T entity);
        void Save();
        void Edit(T entity);
        void DeleteRange(IEnumerable<T> entities);
        void InsertRange(IEnumerable<T> entities);
        T GetById(object id);
        T GetEmptyIfNullById(object id);
        T FindEmptyIfNullByExp(Expression<Func<T, bool>> predicate);
    }
}
