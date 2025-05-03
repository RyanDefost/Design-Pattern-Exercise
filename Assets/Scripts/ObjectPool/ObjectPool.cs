using System;
using System.Collections.Generic;

namespace Project.ObjectPool
{
    public class ObjectPool<T> where T : IPoolable
    {
        private List<T> _activePool = new List<T>();
        private List<T> _inactivePool = new List<T>();

        public T AddObject()
        {
            T instance = (T)Activator.CreateInstance(typeof(T));
            _inactivePool.Add(instance);

            return instance;
        }

        public T ActivateObject(T item)
        {
            item.OnEnableObject();
            item.Active = true;

            if (_inactivePool.Contains(item))
            {
                _inactivePool.Remove(item);
            }
            _activePool.Add(item);
            return item;
        }

        public T DeactivateObject(T item)
        {
            item.OnDisableObject();
            item.Active = false;

            if (_activePool.Contains(item))
            {
                _activePool.Remove(item);
            }
            _inactivePool.Add(item);
            return item;
        }

        public T RequestObject()
        {
            if (_inactivePool.Count > 0)
            {
                return ActivateObject(_inactivePool[0]);
            }
            return ActivateObject(AddObject());
        }

        public List<T> GetAllItems()
        {
            List<T> ItemList = new List<T>();
            ItemList.AddRange(_activePool);
            ItemList.AddRange(_inactivePool);

            return ItemList;
        }
    }
}
