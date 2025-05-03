namespace Project.Summon.Decorator
{
    public class MinionWaterDecorator : MinionAirDecorator
    {
        public MinionWaterDecorator(int damage, int defense) : base(damage, defense) { }

        public override Minion Decorate(Minion minion)
        {
            minion.Damage = this.Damage;
            minion.Defense = this.Defense;
            minion.minionTypes |= MinionType.WATER;

            return minion;
        }
    }
}
