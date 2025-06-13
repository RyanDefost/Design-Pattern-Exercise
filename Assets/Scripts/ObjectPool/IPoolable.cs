namespace Project.ObjectPool
{
    /// <summary>
    /// Identifier for classes that are able to be stored in an ObjectPool.
    /// </summary>
    public interface IPoolable
    {
        bool Active { get; set; }

        void OnEnableObject();
        void OnDisableObject();

    }
}
