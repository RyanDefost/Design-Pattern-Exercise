using Project.ObjectPool;
using UnityEngine;

namespace Project.Summon
{
    public class Minion : IPoolable, ISpawnable
    {
        public bool Active { get; set; }

        public int Damage = 1;
        public int Defense = 1;
        public MinionType minionTypes;

        public GameObject gameObject = new GameObject();

        public Minion()
        {
            this.gameObject.name = "Minion";
        }

        public void UpdateMinion()
        {
            this.gameObject.name = (this.minionTypes + "_Minion" + " AT: " + this.Damage + " DEF: " + this.Defense);
        }

        public void OnDisableObject()
        {
            this.gameObject.SetActive(false);

            this.Damage = 1;
            this.Defense = 1;
            this.minionTypes = MinionType.NONE;
            this.gameObject.name = "Minion";
        }

        public void OnEnableObject()
        {
            this.gameObject.SetActive(true);
        }
    }
}
