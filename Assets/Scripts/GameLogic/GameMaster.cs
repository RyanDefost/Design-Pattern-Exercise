using Project.Summon;
using System;
using UnityEngine;

namespace Project.GameLogic
{
    public class GameMaster : MonoBehaviour, ISingleton<GameMaster>
    {
        public Action OnUpdate;
        public Action OnStart;

        private MinionManager minionManager;

        private void Start()
        {
            ISingleton<GameMaster>.instance = this;

            InstaniatieScripts();
        }

        private void Update() => this.OnUpdate?.Invoke();


        private void InstaniatieScripts()
        {
            this.minionManager = new MinionManager();

            //Expand ...
        }
    }
}