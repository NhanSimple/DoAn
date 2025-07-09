using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XChess.Repository.Common;
using XChess.Store.Common;

namespace XChess.Service.Common
{
    public class GenericStoreService<T> where T : class
    {
        private readonly IGenericStore<T> _store;
        //private readonly IUnitOfWork _unitOfWork;
        //public GenericStoreService(IUnitOfWork unitOfWork,IGenericStore<T> store)
        //{
        //    _unitOfWork= unitOfWork;
        //    _store = store;
        //} store 0 cần commit
        public GenericStoreService(IGenericStore<T> store)
        {
            _store = store;
        }

        public IEnumerable<T> GetAll()
        {
            return _store.GetAll();
        }

        public T GetById(string id)
        {
            return _store.GetById(id);
        }

        public bool TryGet(string id, out T entity)
        {
            return _store.TryGet(id, out entity);
        }

        public bool TryAdd(string id, T entity)
        {
            return _store.TryAdd(id, entity);
        }

        public bool TryRemove(string id)
        {
            return _store.TryRemove(id);
        }

        public bool Contains(string id)
        {
            return _store.Contains(id);
        }

        public void Clear()
        {
            _store.Clear();
        }

        public void Update(string id, T entity)
        {
            _store.Update(id, entity);
        }

        public IEnumerable<U> GetAllOf<U>() where U : class
        {
            return _store.GetAllOf<U>();
        }
    }
}
