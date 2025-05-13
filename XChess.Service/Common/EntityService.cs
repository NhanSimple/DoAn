using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using XChess.Repository.Common;

namespace XChess.Service.Common
{
    public class EntityService<T> : IEntityService<T> where T : class
    {
        IUnitOfWork _unitOfWork;
        IGenericRepository<T> _repository;
        public EntityService(IUnitOfWork unitOfWork, IGenericRepository<T> repository)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        protected EntityService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public virtual void Create(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _repository.Add(entity);
            _unitOfWork.Commit();
        }

        public void Delete(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            _repository.Delete(entity);
            _unitOfWork.Commit();
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _repository.DeleteRange(entities);
            _unitOfWork.Commit();
        }

        public IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            return _repository.GetAll();
        }

        public T GetById(object id)
        {
            return _repository.GetById(id);
        }

        public T GetEmptyIfNullById(object id)
        {
            throw new NotImplementedException();
        }

        public void InsertRange(IEnumerable<T> entities)
        {
            _repository.InsertRange(entities);
            _unitOfWork.Commit();
        }

        public void Save(T entity)
        {
            var id = typeof(T).GetProperty("Id").GetValue(entity);
            if (id.ToString().Equals("0"))
            {
                _repository.Add(entity);
            }
            else
            {
                _repository.Edit(entity);
            }
            _unitOfWork.Commit();
        }

        public void SoftDelete(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            _repository.SoftDelete(entity);
            _unitOfWork.Commit();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
