using Project.GameInput;
using Project.GameLogic;
using Project.ObjectPool;
using Summon;
using System.Collections.Generic;

namespace Project.Summon
{

    public class MinionManager : GameBehaviour
    {
        public List<Minion> minions { get; private set; }

        private ObjectPool<Minion> objectPool;
        private MinionCreator minionCreator;
        private InputQueue inputQueue;

        public MinionManager()
        {
            this.inputQueue = new InputQueue();
            this.objectPool = new ObjectPool<Minion>();
            this.minionCreator = new MinionCreator(this.inputQueue);

            this.inputQueue.OnSetCurrentQueue += CheckQueue;
        }

        public override void Update()
        {
            //this.inputQueue.UpdateQueue();
            UpdateMinions();
        }


        public void ActivateMinion()
        {
            Minion minion = this.objectPool.RequestObject();

            minion = this.minionCreator.TrySetAttributes(minion);
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

        private void CheckQueue()
        {
            if (minionCreator.CheckValidInput(inputQueue.CurrentQueue))
            {
                ActivateMinion();
            }
        }
    }
}
