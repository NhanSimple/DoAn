using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
namespace XChess.Repository.Common
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected DbContext _entities;
        protected readonly IDbSet<T> _dbset;
     
        public GenericRepository(DbContext context)
        {
            _entities = context;
            _dbset = context.Set<T>();
        }

        public DbContext GetContext()
        {
            return _entities;
        }

        public virtual IDbSet<T> DBSet()
        {
            return _dbset;
        }

        public virtual T Add(T entity)
        {
            return _dbset.Add(entity);
        }

        public virtual T Delete(T entity)
        {
            return _dbset.Remove(entity);
        }

        public IEnumerable<T> FindBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {

            IEnumerable<T> query = _dbset.Where(predicate).AsEnumerable();
            return query;
        }


        public void Save()
        {
            _entities.SaveChanges();
        }

        public IQueryable<T> GetAllAsQueryable()
        {
            return _dbset.AsQueryable<T>();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _dbset.AsEnumerable<T>();
        }

        public virtual void SoftDelete(T entity)
        {
           throw new NotImplementedException();
        }
        public virtual void Edit(T entity)
        {
            _entities.Entry(entity).State = EntityState.Modified;
        }
        public virtual void DeleteRange(IEnumerable<T> entities)
        {
            foreach(var item in entities)
            {
                if(_entities.Entry(item).State == EntityState.Detached)
                {
                    _dbset.Attach(item);
                }
                _dbset.Remove(item);
            }
        }
        public void InsertRange(IEnumerable<T> entities)
        {
            _entities.Set<T>().AddRange(entities);
        }
        public T GetById(object id)
        {
            return _dbset.Find(id);
        }

        public T GetEmptyIfNullById(object id)
        {
            var entity = this._dbset.Find(id) ?? Activator.CreateInstance(typeof(T)) as T;
            return entity;
        }
        public T FindEmptyIfNullByExp(Expression<Func<T, bool>> predicate)
        {
            var entity = _entities.Set<T>().Where(predicate).FirstOrDefault() ?? Activator.CreateInstance(typeof(T)) as T;
            return entity;
        }

    }

}
