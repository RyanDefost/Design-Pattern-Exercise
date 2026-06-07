using UnityEngine;

namespace Project.Spawner
{
    /// <summary>
    /// Spawns a new gameObject with the given component attached.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ObjectSpawner<T> where T : ISpawnable
    {
        private GameObject baseObject;

        // Spawns given component in the scene.
        public GameObject SpawnObject(T component, Vector3 position)
        {
            GameObject gameObject = new GameObject();

            gameObject.transform.position = position;
            gameObject.AddComponent(component.GetType());

            return gameObject;
        }
    }
}
