namespace Project.GameLogic
{
    public interface ISingleton<T> where T : ISingleton<T>, new()
    {
        protected static T instance;

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
