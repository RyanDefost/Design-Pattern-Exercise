using Project.GameInput;
using Project.Summon.Decorator;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Project.Summon
{
    public class MinionCreator
    {
        private Dictionary<List<KeyCode>, MinionType> bodyType;
        private Dictionary<KeyCode, int> damageType;
        private Dictionary<KeyCode, int> defenseType;

        private InputQueue inputQueue;

        public MinionCreator(InputQueue inputQueue)
        {
            this.inputQueue = inputQueue;

            this.bodyType = new Dictionary<List<KeyCode>, MinionType>
            {
                {new List<KeyCode>() { KeyCode.DownArrow,   KeyCode.DownArrow,  KeyCode.DownArrow   } , MinionType.WATER },
                {new List<KeyCode>() { KeyCode.UpArrow,     KeyCode.UpArrow,    KeyCode.UpArrow     } , MinionType.EARTH },
                {new List<KeyCode>() { KeyCode.LeftArrow,   KeyCode.LeftArrow,  KeyCode.LeftArrow   } , MinionType.FIRE },
                {new List<KeyCode>() { KeyCode.RightArrow,  KeyCode.RightArrow, KeyCode.RightArrow  } , MinionType.AIR },
            };

            this.damageType = new Dictionary<KeyCode, int>
            {
                {KeyCode.UpArrow,    5},
                {KeyCode.DownArrow,  4},
                {KeyCode.LeftArrow,  4},
                {KeyCode.RightArrow, 2},
            };

            this.defenseType = new Dictionary<KeyCode, int>
            {
                {KeyCode.UpArrow,    5},
                {KeyCode.DownArrow,  4},
                {KeyCode.LeftArrow,  4},
                {KeyCode.RightArrow, 2},
            };

        }

        public bool CheckValidInput(List<KeyCode> input)
        {
            if (input.Count != 5) return false;

            //Check Body Input
            bool validInput = false;
            var inputRange = input.GetRange(0, 3);
            foreach (var item in this.bodyType)
            {
                if (Enumerable.SequenceEqual(inputRange, item.Key)) validInput = true;
            }
            if (validInput == false) return false;


            //Check Attack Input
            validInput = false;
            foreach (var item in this.damageType)
            {
                if (input[3] == item.Key) validInput = true;
            }
            if (validInput == false) return false;

            //Check Defense Input
            validInput = false;
            foreach (var item in this.defenseType)
            {
                if (input[4] == item.Key) validInput = true;
            }
            if (validInput == false) return false;

            return true;
        }

        public Minion TrySetAttributes(Minion minion)
        {
            if (!CheckValidInput(this.inputQueue.CurrentQueue)) return minion;


            this.damageType.TryGetValue(this.inputQueue.CurrentQueue[4], out int damage);
            this.defenseType.TryGetValue(this.inputQueue.CurrentQueue[3], out int defense);

            MinionType minionType = MinionType.NONE;
            foreach (var item in bodyType)
            {
                if (Enumerable.SequenceEqual(this.inputQueue.CurrentQueue.GetRange(0, 3), item.Key))
                {
                    minionType = item.Value;
                }
            }

            if (minionType == MinionType.WATER)
            {
                MinionWaterDecorator Waterdecorator = new MinionWaterDecorator(damage, defense);
                minion = Waterdecorator.Decorate(minion);
            }
            if (minionType == MinionType.EARTH)
            {
                MinionEarthDecorator Earthdecorator = new MinionEarthDecorator(damage, defense);
                minion = Earthdecorator.Decorate(minion);
            }
            if (minionType == MinionType.FIRE)
            {
                MinionFireDecorator Firedecorator = new MinionFireDecorator(damage, defense);
                minion = Firedecorator.Decorate(minion);
            }
            if (minionType == MinionType.AIR)
            {
                MinionAirDecorator Airdecorator = new MinionAirDecorator(damage, defense);
                minion = Airdecorator.Decorate(minion);
            }

            return minion;
        }
    }

}
