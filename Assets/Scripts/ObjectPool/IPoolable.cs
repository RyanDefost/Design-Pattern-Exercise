﻿namespace Project.ObjectPool
{
    public interface IPoolable
    {
        bool Active { get; set; }

        void OnEnableObject();
        void OnDisableObject();

    }
}
