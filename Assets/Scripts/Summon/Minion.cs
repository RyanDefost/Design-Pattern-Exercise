using Project.GameLogic;
using Project.GameLogic.EntityComponents;
using Project.GameLogic.ServiceLocator;
using Project.ObjectPool;
using Project.Player;
using Project.Spawner;
using Project.Summon.Abilities;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Summon
{
    public class Minion : Entity, IPoolable, ISpawnable, IDamager, IHealth, ILocomotion
    {
        public MinionData minionData;

        public bool Active { get; set; }
        public Entity Entity { get => this; }

        public float Damage { get => minionData.Damage; }
        public float Health { get => HealthSystem.GetHealth(); }

        public LocomotionComponent<Minion> locomotionComponent;
        public HealthComponent HealthSystem { get; set; }
        public CollisionComponent CollisionComponent { get; }

        private PlayerManager playerManager = MultiServiceLocator.GetService<PlayerManager>();
        private MinionManager minionManager = MultiServiceLocator.GetService<MinionManager>();

        public Minion()
        {
            this.locomotionComponent = new LocomotionComponent<Minion>(this);
            this.CollisionComponent = new CollisionComponent(this);
            this.HealthSystem = new HealthComponent();
        }

        public void SetData(MinionData minionData)
        {
            this.minionData = minionData;

            this.gameObject.name = (
                this.minionData.caster.Team + "_Minion"
                + " AT: " + this.minionData.Damage
                + " DEF: " + this.minionData.Defense
            );

            this.SetColor(minionData.caster.Team);
            this.SetPosition(minionData.caster.Position + minionData.spawnOffset);
            this.SetScale(new Vector2(0.5f, 0.5f));

            this.locomotionComponent.SetTarget(GetTarget());
            this.locomotionComponent.SetSpeed(minionData.Speed);
            this.HealthSystem.SetHealth(minionData.Defense);

            this.SubscribeToOnHit();
            this.SubscribeToOnDie();

            this.minionData.OnSpawn?.Invoke(this);
        }

        public void OnEnableObject()
        {
            this.gameObject.SetActive(true);
            this.CollisionComponent.Activate();
        }

        public void OnDisableObject()
        {
            this.gameObject.SetActive(false);
            this.gameObject.name = "Minion";

            this.minionData.OnDeath?.Invoke(this);
        }

        public void UpdateMinion()
        {
            locomotionComponent.UpdateMovement();
        }

        private void SetHit(CollisionComponent collider)
        {
            if (collider.actor is IDamager)
            {
                var damager = (IDamager)collider.actor;
                this.HealthSystem.RemoveHealth(damager.Damage);
            }
        }

        private Player.Player GetTarget()
        {
            List<Player.Player> players = playerManager.GetPlayers();
            Player.Player currentTarget = null;

            foreach (var player in players)
            {
                if (player.Team != this.minionData.caster.Team)
                {
                    currentTarget = player;
                }
            }

            return currentTarget;
        }

        private void Destroy()
        {
            this.gameObject.SetActive(false);

            this.UnSubscribeToOnHit();
            this.UnSubscribeToOnDie();

            this.CollisionComponent.Deactivate();
            this.minionManager.DeactivateMinion(this);
        }

        private void SubscribeToOnHit() => this.CollisionComponent.OnHit += SetHit;
        private void SubscribeToOnDie() => this.HealthSystem.OnDie += Destroy;

        private void UnSubscribeToOnHit() => this.CollisionComponent.OnHit -= SetHit;
        private void UnSubscribeToOnDie() => this.HealthSystem.OnDie -= Destroy;

    }

    public struct MinionData : IEntityData
    {
        public ICaster caster;

        public int platoonSize;

        public Vector2 spawnOffset;

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
