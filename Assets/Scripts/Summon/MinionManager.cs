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

        [SerializeField] private List<KeyCode> spawnCode = new List<KeyCode>();

        private ObjectPool<Minion> objectPool;
        private MinionCreator minionCreator;
        private InputQueue inputQueue;

        public MinionManager()
        {
            this.inputQueue = new InputQueue();
            this.objectPool = new ObjectPool<Minion>();
            this.minionCreator = new MinionCreator(this.inputQueue);
        }

        public void Awake()
        {
            this.inputQueue.OnSetCurrentQueue += CheckQueue;
        }

        private void Update()
        {
            this.inputQueue.UpdateQueue();
        }

        private void CheckQueue()
        {
            if (minionCreator.CheckValidInput(inputQueue.CurrentQueue))
            {
                ActivateMinion();
            }
        }

        public void ActivateMinion()
        {
            Minion minion = this.objectPool.RequestObject();

            minion = this.minionCreator.TrySetAttributes(minion);

            minion._gameObject.name = (minion.minionTypes + "_Minion" + " AT: " + minion.Damage + " DEF: " + minion.Defense); //TEMP
            StartCoroutine(test(minion)); //TEMMP
        }

        //REMOVE FUNCTION
        private IEnumerator test(Minion minion)
        {
            yield return new WaitForSeconds(5);
            DeactivateMinion(minion);
        }

        public void DeactivateMinion(Minion minion)
        {
            this.objectPool.DeactivateObject(minion);

            Debug.Log("DEACTIVATE: " + minion.Damage);
        }

        public void GetActiveMinion(Minion minion)
        {
            this.objectPool.GetAllItems();
        }
    }
}
