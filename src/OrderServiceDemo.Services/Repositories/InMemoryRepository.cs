using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace OrderServiceDemo.Services.Repositories
{
    /// <summary>
    /// To any who venture down here - obviously in production, we don't do things this way. This is a cheap
    /// way to provide a fully 'detached' web api that you can spin up on your own without needing connectivity
    /// to a SQL Server, since not everyone has one of those handy.
    /// 
    /// If you did choose to explore down here and bothered reading this, include a star trek gif link in the comments
    /// of your pull request. Extra credit for those that like to explore.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class InMemoryRepository<T>
    {
        private static List<T> _entities = new List<T>();
        private static Action<T> NO_OP_ACTION => ((x) => { });

        protected virtual Action<T> SetIdentity => NO_OP_ACTION;

        //I hope it is obvious that no production system would ever do this...
        private static SemaphoreSlim _cheapLock = new SemaphoreSlim(1, 1);

        protected IEnumerable<T> Entities => _entities;

        protected async Task<IEnumerable<T>> GetEntities(Predicate<T> selector)
        {
            try
            {
                await _cheapLock.WaitAsync();
                return _entities.FindAll(selector);
            }
            finally
            {
                _cheapLock.Release();
            }
        }

        protected async Task<T> GetEntity(Predicate<T> selector)
        {
            try
            {
                await _cheapLock.WaitAsync();
                return _entities.Find(selector);
            }
            finally
            {
                _cheapLock.Release();
            }
        }

        protected async Task<T> AddEntity(T instance)
        {
            try
            {
                await _cheapLock.WaitAsync();
                SetIdentity(instance);
                _entities.Add(instance);
                return instance;
            }
            finally
            {
                _cheapLock.Release();
            }
            
        }

        protected async Task<T> DeleteEntity(T instance)
        {
            try
            {
                await _cheapLock.WaitAsync();
                _entities.Remove(instance);
                return instance;
            }
            finally
            {
                _cheapLock.Release();
            }
        }

        protected async Task<IEnumerable<T>> DeleteEntities(Predicate<T> selector)
        {
            try
            {
                await _cheapLock.WaitAsync();
                var matches = _entities.FindAll(selector);
                _entities.RemoveAll(selector);
                return matches;
            }
            finally
            {
                _cheapLock.Release();
            }
        }
    }
}
