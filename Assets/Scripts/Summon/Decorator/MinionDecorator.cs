namespace Project.Summon.Decorator
{
    public abstract class MinionDecorator
    {
        public int Damage { get; set; }
        public int Defense { get; set; }

        public MinionDecorator(int damage = 1, int defense = 1)
        {
            Damage = damage;
            Defense = defense;
        }

        public abstract Minion Decorate(Minion minion);
    }
}
