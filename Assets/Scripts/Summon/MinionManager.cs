using Project.GameLogic;
using Project.ObjectPool;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Summon
{
    public class MinionManager : GameBehaviour, ISingleton<MinionManager>
    {
        public List<Minion> minions { get; private set; }

        private ObjectPool<Minion> objectPool = new ObjectPool<Minion>();

        public MinionManager()
        {
            ISingleton<MinionManager>.instance = this;
        }

        public override void Update()
        {
            UpdateMinions();
        }

        public void CreateMinion(MinionCreator minionCreator)
        {
            MinionData minionData = new MinionData();
            minionData = minionCreator.SetValues(minionData);

            Debug.Log(minionData.platoonSize);
            for (int i = 0; i < minionData.platoonSize; i++)
            {
                ActivateMinion(minionData);
            }
        }

        public void ActivateMinion(MinionData minionData)
        {
            Minion minion = this.objectPool.RequestObject();
            minion.minionData = minionData;
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
