using Project.GameInput;
using Project.Player;
using Project.Summon.Decorator;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Summon
{
    public class MinionCreator
    {
        private ICaster caster;
        private InputQueue inputQueue;

        private Dictionary<KeyCode, MinionType> typing;

        public MinionCreator(InputQueue inputQueue, ICaster caster)
        {
            this.caster = caster;
            this.inputQueue = inputQueue;

            this.typing = new Dictionary<KeyCode, MinionType>
            {
                { KeyCode.UpArrow,      MinionType.WATER },
                { KeyCode.DownArrow,    MinionType.EARTH },
                { KeyCode.LeftArrow,    MinionType.FIRE },
                { KeyCode.RightArrow,   MinionType.AIR }
            };
        }

        public MinionData SetValues(MinionData minionData)
        {
            var QueueOrder = this.inputQueue.CurrentQueue;

            foreach (var item in typing)
            {
                if (QueueOrder[0] == item.Key)
                {
                    var abilityDecorator = new MinionAbilityDecorator(item.Value);
                    minionData = abilityDecorator.Decorate(minionData);
                }
            }

            foreach (var item in typing)
            {
                if (QueueOrder[1] == item.Key)
                {
                    var abilityDecorator = new MinionBodyDecorator(item.Value);
                    minionData = abilityDecorator.Decorate(minionData);
                }
            }

            foreach (var item in typing)
            {
                if (QueueOrder[2] == item.Key)
                {
                    var abilityDecorator = new MinionStabilityDecorator(item.Value);
                    minionData = abilityDecorator.Decorate(minionData);
                }
            }

            foreach (var item in typing)
            {
                if (QueueOrder[3] == item.Key)
                {
                    var abilityDecorator = new MinionAmountDecorator(item.Value);
                    minionData = abilityDecorator.Decorate(minionData);
                }
            }

            minionData.caster = this.caster;

            return minionData;
        }
    }

}
