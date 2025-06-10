using Project.Player;
using Project.Summon;
using System;
using UnityEngine;

namespace Project.GameLogic
{
    public class GameMaster : MonoBehaviour, ISingleton<GameMaster>
    {
        public Action OnUpdate;

        private CollisionSystem collisionSystem;

        private MinionManager minionManager;
        private PlayerManager playerManager;


        private void Start()
        {
            ISingleton<GameMaster>.instance = this;

            InstaniatieScripts();
        }

        private void Update() => this.OnUpdate?.Invoke();


        private void InstaniatieScripts()
        {
            this.collisionSystem = new CollisionSystem();

            this.minionManager = new MinionManager();
            this.playerManager = new PlayerManager();
            //Expand ...
        }
    }
}