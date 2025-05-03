using Project.GameInput;
using Project.ObjectPool;
using Project.Summon.Decorator;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Summon
{

    public class MinionManager : MonoBehaviour
    {
        public List<Minion> minions { get; private set; }

        [SerializeField] private List<KeyCode> _spawnCode = new List<KeyCode>();

        private InputQueue _inputQueue = new InputQueue();
        private ObjectPool<Minion> objectPool = new ObjectPool<Minion>();
        private ObjectSpawner<Minion> objectSpawner = new ObjectSpawner<Minion>();

        public void Awake()
        {
            _inputQueue.OnSetCurrentQueue += CheckQueue;
        }

        private void Update()
        {
            _inputQueue.UpdateQueue();
        }

        private void CheckQueue()
        {
            for (int i = 0; i < _inputQueue.CurrentQueue.Count - 1; i++)
            {
                if (_inputQueue.CurrentQueue[i] != _spawnCode[i]) return;
            }
            ActivateMinion();
        }

        public void ActivateMinion()
        {
            Minion minion = objectPool.RequestObject();

            MinionAirDecorator airDecorator = new MinionAirDecorator(5, 5);
            minion = airDecorator.Decorate(minion);

            StartCoroutine(test(minion)); //TEMMP
            //objectSpawner.SpawnObject(minion, Vector3.zero);
        }

        //REMOVE FUNCTION
        private IEnumerator test(Minion minion)
        {
            yield return new WaitForSeconds(5);
            DeactivateMinion(minion);
        }

        public void DeactivateMinion(Minion minion)
        {
            objectPool.DeactivateObject(minion);

            Debug.Log("DEACTIVATE: " + minion.Damage);
        }

        public void GetActiveMinion(Minion minion)
        {
            objectPool.GetAllItems();
        }
    }
}
