using Project.GameInput;
using Project.ObjectPool;
using Summon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Summon
{

    public class MinionManager : MonoBehaviour
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

        private void Update()
        {
            this.inputQueue.UpdateQueue();
            UpdateMinions();
        }


        public void ActivateMinion()
        {
            Minion minion = this.objectPool.RequestObject();

            minion = this.minionCreator.TrySetAttributes(minion);

            StartCoroutine(test(minion)); //TEMMP
        }


        public void DeactivateMinion(Minion minion)
        {
            this.objectPool.DeactivateObject(minion);

            Debug.Log("DEACTIVATE: " + minion.Damage);
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

        //REMOVE FUNCTION
        private IEnumerator test(Minion minion)
        {
            yield return new WaitForSeconds(5);
            DeactivateMinion(minion);
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
