using System;
using System.Collections.Generic;

namespace Project.ObjectPool
{
    public class ObjectPool<T> where T : IPoolable
    {
        private List<T> activePool = new List<T>();
        private List<T> inactivePool = new List<T>();

        public T AddObject()
        {
            T instance = (T)Activator.CreateInstance(typeof(T));
            this.inactivePool.Add(instance);

            return instance;
        }

        public T ActivateObject(T item)
        {
            item.OnEnableObject();
            item.Active = true;

            if (this.inactivePool.Contains(item))
            {
                this.inactivePool.Remove(item);
            }
            this.activePool.Add(item);
            return item;
        }

        public T DeactivateObject(T item)
        {
            item.OnDisableObject();
            item.Active = false;

            if (this.activePool.Contains(item))
            {
                this.activePool.Remove(item);
            }
            this.inactivePool.Add(item);
            return item;
        }

        public T RequestObject()
        {
            if (this.inactivePool.Count > 0)
            {
                return ActivateObject(this.inactivePool[0]);
            }
            return ActivateObject(AddObject());
        }

        public List<T> GetAllItems()
        {
            List<T> ItemList = new List<T>();
            ItemList.AddRange(this.activePool);
            ItemList.AddRange(this.inactivePool);

            return ItemList;
        }
    }
}
