using System.Collections.Generic;

namespace Project.GameLogic.ServiceLocator
{
    /// <summary>
    /// A central place for universal systems to subscribe and be accessed anywhere in the code base. 
    /// </summary>
    public static class MultiServiceLocator
    {
        private static Dictionary<System.Type, object> services = new Dictionary<System.Type, object>();

        // Checks if a service of type T exists and returns the reference to it.
        public static T GetService<T>()
        {
            if (services.ContainsKey(typeof(T)))
            {
                return (T)services[typeof(T)];
            }
            return default(T);
        }

        // Adds the given class to the services to be accessed by other classes.
        public static void Provide<T>(T value)
        {
            if (!services.ContainsKey(typeof(T)))
            {
                services.Add(typeof(T), value);
            }
            services[typeof(T)] = value;
        }

        // Removes all reverences to the services inside the MSL.
        public static void ClearAllServices()
        {
            services.Clear();
        }
    }
}
