using System.Collections.Generic;

namespace Project.Summon.Decorator
{
    public class MinionAmountDecorator : MinionAbilityDecorator
    {
        private Dictionary<MinionType, MinionAmountData> amountValues = new Dictionary<MinionType, MinionAmountData>()
        {
            {MinionType.WATER,  new MinionAmountData(4, 0.8f, 0.8f)},
            {MinionType.EARTH,  new MinionAmountData(1, 1.2f, 1.2f)},
            {MinionType.FIRE,   new MinionAmountData(2, 1.0f, 1.0f)},
            {MinionType.AIR,    new MinionAmountData(4, 0.8f, 0.8f)}
        };

        public MinionAmountDecorator(MinionType minionType) : base(minionType) { }

        public override MinionData Decorate(MinionData minionData)
        {
            minionData = SetAmountValues(minionData);
            minionData = SetSizeOffset(minionData);

            return minionData;
        }

        private MinionData SetAmountValues(MinionData minionData)
        {
            foreach (var data in amountValues)
            {
                if (this.minionType == data.Key)
                {
                    minionData.platoonSize = data.Value.Amount;
                    minionData.size = data.Value.Size;
                    minionData.areaOfEffect = data.Value.AreaOfEffect;
                }
            }

            return minionData;
        }

        private MinionData SetSizeOffset(MinionData minionData)
        {
            minionData.Damage = GetIncrementalProcentage(minionData.Damage, minionData.platoonSize);
            minionData.Defense = GetIncrementalProcentage(minionData.Defense, minionData.platoonSize);
            minionData.Speed = GetIncrementalProcentage(minionData.Speed, minionData.platoonSize);
            minionData.size = GetIncrementalProcentage(minionData.size, minionData.platoonSize);
            minionData.areaOfEffect = GetIncrementalProcentage(minionData.areaOfEffect, minionData.platoonSize);

            return minionData;
        }

        private float GetIncrementalProcentage(float startValue, int amount)
        {
            float finalAmount = amount;
            for (int i = 0; i < amount; i++)
            {
                finalAmount = (float)(startValue * (1 - 0.25));
            }

            return finalAmount;
        }
    }

    public struct MinionAmountData
    {
        private readonly int amount;
        public int Amount { get { return amount; } }

        private readonly float size;
        public float Size { get { return size; } }

        private readonly float areaOfEffect;
        public float AreaOfEffect { get { return areaOfEffect; } }


        public MinionAmountData(int amount, float size, float areaOfEffect)
        {
            this.amount = amount;
            this.size = size;
            this.areaOfEffect = areaOfEffect;
        }
    }
}
