using Project.GameLogic;
using Project.ObjectPool;
using Project.Player;
using Project.Spawner;
using Project.Summon.Abilities;
using System;
using UnityEngine;

namespace Project.Summon
{
    public class Minion : Entity, IPoolable, ISpawnable
    {
        public bool Active { get; set; }
        public MinionData minionData;

        public Minion()
        {
            this.gameObject.name = "minion";
        }

        public void UpdateMinion()
        {
            this.gameObject.name = (
                this.minionData.caster.PlayerData.Name + "_Minion"
                + " AT: " + this.minionData.Damage
                + " DEF: " + this.minionData.Defense
            );
        }

        public void OnDisableObject()
        {
            this.gameObject.SetActive(false);

            this.gameObject.name = "minion";
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

    public struct MinionData
    {
        public ICaster caster;
        public int platoonSize;

        public float Damage;
        public float Defense;
        public float Speed;
        public float size;
        public float areaOfEffect;

        public IAbility ability;

        public float timer;
        public Action<Minion> OnDeath;
        public Action<Minion> OnSpawn;
    }
}
