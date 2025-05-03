namespace Project.Summon.Decorator
{
    public class MinionEarthDecorator : MinionDecorator
    {
        public MinionEarthDecorator(int damage, int defense) : base(damage, defense) { }

        public override Minion Decorate(Minion minion)
        {
            minion.Damage = this.Damage;
            minion.Defense = this.Defense;
            minion.minionTypes |= MinionType.EARTH;

            return minion;
        }
    }
}
