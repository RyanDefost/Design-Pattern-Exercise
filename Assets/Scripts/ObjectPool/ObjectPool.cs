using System;
using System.Collections.Generic;

namespace Project.ObjectPool
{
    /// <summary>
    /// Class for efficiently storing instances of T.
    /// </summary>
    /// <typeparam name="T"> Type of the stored instances.</typeparam>
    public class ObjectPool<T> where T : IPoolable
    {
        private List<T> activePool = new List<T>();
        private List<T> inactivePool = new List<T>();

        // Adds a new T instance to the ObjectPool.
        public T AddObject()
        {
            T instance = (T)Activator.CreateInstance(typeof(T));
            this.inactivePool.Add(instance);

            return instance;
        }

        // Activates an object from the inactivePool.
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

        // Deactivates an object from the activePool.
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

        // Checks if there are any instances of T to activate and makes a new one when needed.
        public T RequestObject()
        {
            if (this.inactivePool.Count > 0)
            {
                return ActivateObject(this.inactivePool[0]);
            }
            return ActivateObject(AddObject());
        }

        // Gets a list of all active and deactivate items.
        public List<T> GetAllItems()
        {
            List<T> ItemList = new List<T>();
            ItemList.AddRange(this.activePool);
            ItemList.AddRange(this.inactivePool);

            return ItemList;
        }
    }
}
