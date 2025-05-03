namespace Project.Summon.Decorator
{
    public class MinionAirDecorator : MinionDecorator
    {
        public MinionAirDecorator(int damage, int defense) : base(damage, defense) { }

        public override Minion Decorate(Minion minion)
        {
            minion.Damage = this.Damage;
            minion.Defense = this.Defense;
            minion.minionTypes |= MinionType.AIR;

            return minion;
        }
    }
}
