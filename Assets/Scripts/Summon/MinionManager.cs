using Project.GameLogic.ServiceLocator;
using Project.GameLogic.Systems;
using Project.ObjectPool;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Summon
{
    /// <summary>
    /// Manages all Minions in the game and unpdates all the logic.
    /// </summary>
    public class MinionManager : GameBehaviour
    {
        public List<Minion> minions { get; private set; }

        private ObjectPool<Minion> objectPool = new ObjectPool<Minion>();

        public MinionManager()
        {
            MultiServiceLocator.Provide<MinionManager>(this);
        }

        // Updates the Minions.
        public override void Update()
        {
            UpdateMinions();
        }

        // Creates a new instance of MinionData and assigns this to a Minion from the ObjectPool.
        public void CreateMinion(MinionCreator minionCreator, List<Vector2> spawnPositions)
        {
            MinionData minionData = new MinionData();
            minionData = minionCreator.SetValues(minionData);

            for (int i = 0; i < minionData.platoonSize; i++)
            {
                minionData.spawnOffset = spawnPositions[i];
                ActivateMinion(minionData);
            }
        }

        // Activates the minion with the given minionData.
        public void ActivateMinion(MinionData minionData)
        {
            Minion minion = this.objectPool.RequestObject();
            minion.SetData(minionData);
        }

        // Deactivates the given minion.
        public void DeactivateMinion(Minion minion)
        {
            this.objectPool.DeactivateObject(minion);
        }

        // Gets all current minions in the game.
        public List<Minion> GetAllMinions()
        {
            return this.objectPool.GetAllItems();
        }

        private void UpdateMinions()
        {
            foreach (var minion in GetAllMinions())
            {
                if (minion.Active)
                {
                    minion.UpdateMinion();
                }
            }
        }
    }
}
