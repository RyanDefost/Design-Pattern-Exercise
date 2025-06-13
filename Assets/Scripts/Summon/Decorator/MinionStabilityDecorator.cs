using System;
using System.Collections.Generic;

namespace Project.Summon.Decorator
{
    /// <summary>
    /// Assigns the OnDie or OnSpawn effect and timer to the given minionData.
    /// </summary>
    public class MinionStabilityDecorator : MinionDecorator
    {
        private static MinionData baseMinionData = new MinionData();
        private Dictionary<MinionType, MinionStabilityData> stabilityValues = new Dictionary<MinionType, MinionStabilityData>()
        {
            {MinionType.WATER,  new MinionStabilityData(baseMinionData.OnSpawn, 2)},
            {MinionType.EARTH,  new MinionStabilityData(baseMinionData.OnSpawn, 5)},
            {MinionType.FIRE,   new MinionStabilityData(baseMinionData.OnDeath, 2)},
            {MinionType.AIR,    new MinionStabilityData(baseMinionData.OnDeath, 5)}
        };

        public MinionStabilityDecorator(MinionType minionType) : base(minionType) { }

        //Decorates the MinionData with the correlation values.
        public override MinionData Decorate(MinionData minionData)
        {
            minionData = SetStabilityValues(minionData);

            return minionData;
        }

        private MinionData SetStabilityValues(MinionData minionData)
        {
            foreach (var data in stabilityValues)
            {
                if (this.minionType == data.Key)
                {
                    minionData.timer = data.Value.Timer;

                    if (data.Value.Action == minionData.OnDeath)
                    {
                        minionData.OnDeath += minionData.ability.Activate;
                    }

                    if (data.Value.Action == minionData.OnSpawn)
                    {
                        minionData.OnSpawn += minionData.ability.Activate;
                    }
                }
            }

            return minionData;
        }
    }

    public struct MinionStabilityData
    {
        private readonly Action<Minion> action;
        public Action<Minion> Action { get { return action; } }

        private readonly float timer;
        public float Timer { get { return timer; } }

        public MinionStabilityData(Action<Minion> action, float timer)
        {
            this.action = action;
            this.timer = timer;
        }
    }
}
