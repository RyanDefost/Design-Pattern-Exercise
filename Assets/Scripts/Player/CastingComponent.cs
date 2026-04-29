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

        private readonly List<List<KeyCode>> spells = new()
        {
            new List<KeyCode> {KeyCode.UpArrow,    KeyCode.DownArrow,  KeyCode.UpArrow,    KeyCode.UpArrow},    // WATER
            new List<KeyCode> {KeyCode.DownArrow,  KeyCode.LeftArrow,  KeyCode.RightArrow, KeyCode.RightArrow}, // EARTH
            new List<KeyCode> {KeyCode.LeftArrow,  KeyCode.LeftArrow,  KeyCode.RightArrow, KeyCode.LeftArrow},  // FIRE
            new List<KeyCode> {KeyCode.RightArrow, KeyCode.RightArrow, KeyCode.LeftArrow,  KeyCode.LeftArrow}   // WIND
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
            if(!CheckValidCasting()) return;
            
            Debug.Log("Casting!!!!!");
            this.MinionManager.CreateMinion(this.minionCreator, spawnPositions);
        }

        private bool CheckValidCasting()
        {
            foreach (List<KeyCode> spell in spells)
            {
                bool isValid = false;
                for (int i = 0; i < inputQueue.CurrentQueue.Count; i++)
                {
                    if (inputQueue.CurrentQueue[i] == spell[i]) isValid = true;
                    else
                    {
                        isValid = false;
                        break;
                    }
                }

                if (isValid) return true;
            }
            
            return false;
        }

        public void AddSpell(List<KeyCode> spell)
        {
            if(spells.Contains(spell)) return;
            if (spells.Count > 3)
            {
                Debug.Log($"Spell Input {spells} is more than 3 inputs long");
                return;
            }
            
            spells.Add(spell);
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
