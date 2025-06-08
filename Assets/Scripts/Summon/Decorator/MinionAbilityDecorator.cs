using Project.Summon.Abilities;
using System.Collections.Generic;

namespace Project.Summon.Decorator
{
    public class MinionAbilityDecorator : MinionDecorator
    {
        private Dictionary<MinionType, IAbility> abilities = new Dictionary<MinionType, IAbility>()
        {
            {MinionType.WATER,  new MinionWaterAbility()},
            {MinionType.EARTH,  new MinionEarthAbility()},
            {MinionType.FIRE,   new MinionFireAbility()},
            {MinionType.AIR,    new MinionAirAbility()}
        };

        public MinionAbilityDecorator(MinionType minionType) : base(minionType) { }

        public override MinionData Decorate(MinionData minionData)
        {
            minionData = SetAbility(minionData);

            return minionData;
        }

        public MinionData SetAbility(MinionData minionData)
        {
            foreach (var ability in abilities)
            {
                if (this.minionType == ability.Key)
                {
                    minionData.ability = ability.Value;
                }
            }

            return minionData;
        }
    }
}
