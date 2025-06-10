using Project.GameInput;
using Project.GameLogic;
using Project.Summon;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Player
{
    public class CastingComponent
    {
        private InputQueue inputQueue;
        private MinionCreator minionCreator;

        private MinionManager MinionManager = ISingleton<MinionManager>.Instance();

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

        public void UpdateCasting()
        {
            inputQueue.UpdateInputQueue();
        }

        public void Cast()
        {
            this.MinionManager.CreateMinion(this.minionCreator, spawnPositions);
        }

        public void Destroy()
        {
            UnSubscribeToOnSetCurrentQueue();
        }

        private void SubscribeToOnSetCurrentQueue() => this.inputQueue.OnSetCurrentQueue += Cast;
        private void UnSubscribeToOnSetCurrentQueue() => this.inputQueue.OnSetCurrentQueue -= Cast;

    }
}
