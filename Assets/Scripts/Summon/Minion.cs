using Project.GameLogic;
using Project.ObjectPool;
using Project.Spawner;
using UnityEngine;

namespace Project.Summon
{
    public class Minion : Entity, IPoolable, ISpawnable
    {
        public bool Active { get; set; }

        public Color teamColor;

        public int Damage = 1;
        public int Defense = 1;
        public MinionType minionTypes;

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

        public void SetColor(Color color)
        {
            this.spriteRenderer.color = color;
        }
    }
}
