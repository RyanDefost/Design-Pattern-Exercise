namespace Project.Summon.Decorator
{
    public class MinionFireDecorator : MinionDecorator
    {
        public MinionFireDecorator(int damage, int defense) : base(damage, defense) { }

        public override Minion Decorate(Minion minion)
        {
            minion.Damage = this.Damage;
            minion.Defense = this.Defense;
            minion.minionTypes |= MinionType.FIRE;

            return minion;
        }
    }
}
