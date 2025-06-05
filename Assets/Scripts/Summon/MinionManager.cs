using Project.GameLogic;
using Project.ObjectPool;
using System.Collections.Generic;

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

        public void ActivateMinion(MinionCreator minionCreator)
        {
            Minion minion = this.objectPool.RequestObject();

            minionCreator.TrySetAttributes(minion);
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
