using Project.GameLogic.ServiceLocator;
using Project.Player;
using Project.Summon;
using System;
using UnityEngine;

namespace Project.GameLogic.Systems
{
    /// <summary>
    /// Starting point that sets initial values and updates every frame.
    /// </summary>
    public class GameMaster : MonoBehaviour
    {
        public Action OnUpdate;

        private MinionManager minionManager;
        private PlayerManager playerManager;


        private void Start()
        {
            MultiServiceLocator.Provide<GameMaster>(this);

            InstaniatieScripts();
        }

        private void Update() => this.OnUpdate?.Invoke();


        private void InstaniatieScripts()
        {
            this.minionManager = new MinionManager();
            this.playerManager = new PlayerManager();

            //Expand ...
        }
    }
}