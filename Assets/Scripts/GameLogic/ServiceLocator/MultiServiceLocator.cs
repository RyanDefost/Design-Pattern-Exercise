using System.Collections.Generic;

namespace Project.GameLogic.ServiceLocator
{
    public static class MultiServiceLocator
    {
        private static Dictionary<System.Type, object> services = new Dictionary<System.Type, object>();

        public static T GetService<T>()
        {
            if (services.ContainsKey(typeof(T)))
            {
                return (T)services[typeof(T)];
            }
            return default(T);
        }

        public static void Provide<T>(T value)
        {
            if (!services.ContainsKey(typeof(T)))
            {
                services.Add(typeof(T), value);
            }
            services[typeof(T)] = value;
        }

        public static void ClearAllServices()
        {
            services.Clear();
        }

    }
}
