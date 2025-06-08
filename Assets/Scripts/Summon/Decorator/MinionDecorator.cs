namespace Project.Summon.Decorator
{
    public abstract class MinionDecorator
    {
        protected MinionType minionType;

        public MinionDecorator(MinionType minionType)
        {
            this.minionType = minionType;
        }

        public abstract MinionData Decorate(MinionData minionData);
    }
}
