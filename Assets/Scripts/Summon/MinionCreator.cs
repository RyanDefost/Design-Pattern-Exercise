using Project.Player;
using Project.Summon.Decorator;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Project.Summon
{
    public class MinionCreator
    {
        private ICaster caster;

        private Dictionary<List<KeyCode>, MinionType> bodyType;
        private Dictionary<KeyCode, int> damageType;
        private Dictionary<KeyCode, int> defenseType;

        //
        private Dictionary<KeyCode, MinionDecorator> typing;

        public MinionCreator(ICaster caster)
        {
            this.caster = caster;

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
                {KeyCode.UpArrow,    5}, //Water
                {KeyCode.DownArrow,  4}, //Earth
                {KeyCode.LeftArrow,  4}, //Fire
                {KeyCode.RightArrow, 2}, //Air
            };


        }

        public Minion SetValues(Minion minion) //FIRST STEP TO CREATE MINION
        {
            foreach (var item in typing)
            {
                if (caster.InputQueue.CurrentQueue[0] == item.Key)
                {
                    item.Value.Decorate(minion);
                }
            }


            return minion;
        }

        public Minion TrySetAttributes(Minion minion)
        {
            this.damageType.TryGetValue(this.caster.InputQueue.CurrentQueue[4], out int damage);
            this.defenseType.TryGetValue(this.caster.InputQueue.CurrentQueue[3], out int defense);

            MinionType minionType = MinionType.NONE;
            foreach (var item in bodyType)
            {
                if (Enumerable.SequenceEqual(this.caster.InputQueue.CurrentQueue.GetRange(0, 3), item.Key))
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

            minion.SetColor(caster.PlayerData.teamColor);
            minion.SetScale(new Vector2(.5f, .5f));
            minion.SetPosition(caster.Position);

            return minion;
        }
    }

}
