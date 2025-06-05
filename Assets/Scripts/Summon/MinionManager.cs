using Project.GameInput;
using Project.GameLogic;
using Project.ObjectPool;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Summon
{
    public class MinionManager : GameBehaviour
    {
        public List<Minion> minions { get; private set; }

        private InputQueue inputQueue = new InputQueue();
        private ObjectPool<Minion> objectPool = new ObjectPool<Minion>();
        private MinionCreator minionCreator;

        public MinionManager()
        {
            this.minionCreator = new MinionCreator(this.inputQueue);
            this.inputQueue.OnSetCurrentQueue += CheckQueue;
        }

        public override void Update()
        {
            this.inputQueue.UpdateInputQueue();
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
            Debug.Log("Recieved!");
            if (minionCreator.CheckValidInput(inputQueue.CurrentQueue))
            {
                ActivateMinion();
            }
        }
    }
}
