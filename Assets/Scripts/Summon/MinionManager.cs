using Project.GameLogic.ServiceLocator;
using Project.GameLogic.Systems;
using Project.ObjectPool;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Summon
{
    public class MinionManager : GameBehaviour
    {
        public List<Minion> minions { get; private set; }

        private ObjectPool<Minion> objectPool = new ObjectPool<Minion>();

        public MinionManager()
        {
            MultiServiceLocator.Provide<MinionManager>(this);
        }

        public override void Update()
        {
            UpdateMinions();
        }

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

        public void ActivateMinion(MinionData minionData)
        {
            Minion minion = this.objectPool.RequestObject();
            minion.SetData(minionData);
        }

        public void DeactivateMinion(Minion minion)
        {
            this.objectPool.DeactivateObject(minion);
        }

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
