namespace Project.GameLogic.Systems
{
    /// <summary>
    /// Singleton interface that lets itself be called anywhere inside the code base.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISingleton<T> where T : ISingleton<T>, new()
    {
        protected static T instance;

        // Get the current instance of class T.
        public static T Instance()
        {
            if (instance == null)
            {
                instance = new T();
            }

            return instance;
        }
    }
}
