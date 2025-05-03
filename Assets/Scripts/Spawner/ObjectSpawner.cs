using UnityEngine;

namespace Project
{
    /// <summary>
    /// DOES NOT WORK YET! Component values aren't set to the GameObject.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ObjectSpawner<T> where T : ISpawnable
    {
        private GameObject baseObject;

        public GameObject SpawnObject(T component, Vector3 position)
        {
            GameObject gameObject = new GameObject();

            gameObject.transform.position = position;
            gameObject.AddComponent(component.GetType());

            return gameObject;
        }
    }
}
