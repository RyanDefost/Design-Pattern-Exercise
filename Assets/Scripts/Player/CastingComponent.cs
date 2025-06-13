using Project.GameInput;
using Project.GameLogic.ServiceLocator;
using Project.Summon;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Player
{
    /// <summary>
    /// A component for entity that have the ability to create minions based on given input.
    /// </summary>
    public class CastingComponent
    {
        private InputQueue inputQueue;
        private MinionCreator minionCreator;

        private MinionManager MinionManager = MultiServiceLocator.GetService<MinionManager>();

        private List<Vector2> spawnPositions = new List<Vector2>()
        {
            {new Vector2(0.5f, 1f)},
            {new Vector2(0.5f, -1f)},
            {new Vector2(-0.5f, 1f)},
            {new Vector2(-0.5f, -1f)}
        };

        public CastingComponent(ICaster caster, KeyCode[] castInput)
        {
            inputQueue = new InputQueue(castInput);
            minionCreator = new MinionCreator(inputQueue, caster);

            SubscribeToOnSetCurrentQueue();
        }

        // Updates the logic of the InputQueue.
        public void UpdateCasting()
        {
            inputQueue.UpdateInputQueue();
        }

        // Activates the logic of casting and creates a minion.
        public void Cast()
        {
            this.MinionManager.CreateMinion(this.minionCreator, spawnPositions);
        }

        // Removes all subscriptions form the instance.
        public void Destroy()
        {
            UnSubscribeToOnSetCurrentQueue();
        }

        private void SubscribeToOnSetCurrentQueue() => this.inputQueue.OnSetCurrentQueue += Cast;
        private void UnSubscribeToOnSetCurrentQueue() => this.inputQueue.OnSetCurrentQueue -= Cast;

    }
}
