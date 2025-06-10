using Project.GameInput;
using Project.GameInput.MovementInput;
using Project.GameLogic;
using Project.Summon;
using UnityEngine;

namespace Project.Player
{
    public class Player : Entity, IInputReceiver, ICaster, IHealth
    {
        public PlayerData playerData;

        public Color Team { get => playerData.team; }
        public Vector2 Position { get => this.gameObject.transform.position; }
        public float Health { get => HealthSystem.GetHealth(); }
        public float CastArea { get; set; }

        public HealthSystem HealthSystem { get; }
        public CollisionComponent CollisionComponent { get; }
        public CastingComponent CastingComponent { get; }

        private MinionManager MinionManager = ISingleton<MinionManager>.Instance();
        private PlayerManager playerManager = ISingleton<PlayerManager>.Instance();

        private InputHandler inputHandler;
        private MoveCommand MoveUpCommand = new MoveCommand(Vector2.up);
        private MoveCommand MoveDownCommand = new MoveCommand(Vector2.down);
        private MoveCommand MoveLeftCommand = new MoveCommand(Vector2.left);
        private MoveCommand MoveRightCommand = new MoveCommand(Vector2.right);


        public Player(PlayerData data)
        {
            this.playerData = data;

            this.gameObject.name = data.Name;
            this.spriteRenderer.color = data.team;

            this.HealthSystem = new HealthSystem(playerData.health);
            this.CastingComponent = new CastingComponent(this, this.playerData.castInput);
            this.CollisionComponent = new CollisionComponent(this);

            this.inputHandler = new InputHandler(this);
            this.inputHandler.BindInputToCommand(data.movementInput[0], MoveUpCommand, true);
            this.inputHandler.BindInputToCommand(data.movementInput[1], MoveDownCommand, true);
            this.inputHandler.BindInputToCommand(data.movementInput[2], MoveLeftCommand, true);
            this.inputHandler.BindInputToCommand(data.movementInput[3], MoveRightCommand, true);

            this.SubscribeToOnHit();
            this.SubscribeToOnDie();
        }

        public void UpdatePlayer()
        {
            this.inputHandler.HandleInput();
            this.CastingComponent.UpdateCasting();

            this.UpdateEntity();
        }

        private void SetHit(CollisionComponent collider)
        {
            if (collider.actor is IDamager)
            {
                foreach (var minion in MinionManager.GetAllMinions())
                {
                    if (minion.CollisionComponent == collider)
                    {
                        if (minion.minionData.caster == this) return;

                        minion.HealthSystem.Die();
                        this.HealthSystem.RemoveHealth(minion.Damage);
                    }
                }
            }
        }

        private void Destroy()
        {
            this.UnSubscribeToOnHit();
            this.UnSubscribeToOnDie();

            this.CastingComponent.Destroy();

            gameObject.SetActive(false);
            playerManager.DeSpawnPlayer(this);
        }


        private void SubscribeToOnHit() => this.CollisionComponent.OnHit += SetHit;
        private void SubscribeToOnDie() => this.HealthSystem.OnDie += Destroy;

        private void UnSubscribeToOnHit() => this.CollisionComponent.OnHit -= SetHit;
        private void UnSubscribeToOnDie() => this.HealthSystem.OnDie -= Destroy;
    }

    public struct PlayerData : IEntityData
    {
        public string Name;
        public Color team;
        public float health;
        public Vector2 spawnPosition;

        public KeyCode[] movementInput;
        public KeyCode[] castInput;
    }
}
