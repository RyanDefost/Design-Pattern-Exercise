using System.Collections.Generic;

namespace Project.Summon.Decorator
{
    public class MinionBodyDecorator : MinionDecorator
    {
        private Dictionary<MinionType, MinionBodyData> bodyValues = new Dictionary<MinionType, MinionBodyData>()
        {
            {MinionType.WATER,  new MinionBodyData(4,4,5)},
            {MinionType.EARTH,  new MinionBodyData(4,6,3)},
            {MinionType.FIRE,   new MinionBodyData(6,2,5)},
            {MinionType.AIR,    new MinionBodyData(2,4,7)}
        };

        public MinionBodyDecorator(MinionType minionType) : base(minionType) { }

        public override MinionData Decorate(MinionData minionData)
        {
            minionData = SetBodyValues(minionData);

            return minionData;
        }

        public MinionData SetBodyValues(MinionData minionData)
        {
            foreach (var data in bodyValues)
            {
                if (this.minionType == data.Key)
                {
                    minionData.Damage = data.Value.Damage;
                    minionData.Defense = data.Value.Defense;
                    minionData.Speed = data.Value.Speed;
                }
            }

            return minionData;
        }
    }

    public struct MinionBodyData
    {
        private readonly float damage;
        public float Damage { get { return damage; } }

        private readonly float defense;
        public float Defense { get { return defense; } }

        private readonly float speed;
        public float Speed { get { return speed; } }


        public MinionBodyData(float damage, float defense, float speed)
        {
            this.damage = damage;
            this.defense = defense;
            this.speed = speed;
        }
    }
}
