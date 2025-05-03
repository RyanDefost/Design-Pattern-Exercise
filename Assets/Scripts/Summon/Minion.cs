using Project.ObjectPool;
using UnityEngine;

namespace Project.Summon
{
    public class Minion : MonoBehaviour, IPoolable, ISpawnable
    {
        public int Damage = 1;
        public int Defense = 1;

        public MinionType minionTypes;

        private GameObject gameObject = new GameObject();
        public bool Active { get; set; }

        public Minion()
        {
            gameObject.name = "Minion";
        }

        public void OnDisableObject()
        {
            this.gameObject.SetActive(false);

            this.Damage = 1;
            this.Defense = 1;
            //Reset TYPE
        }

        public void OnEnableObject()
        {
            this.gameObject.SetActive(true);
        }
    }
}
