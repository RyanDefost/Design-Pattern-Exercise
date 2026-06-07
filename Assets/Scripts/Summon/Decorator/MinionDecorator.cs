namespace Project.Summon.Decorator
{
    /// <summary>
    /// Base class for decorators that set values and references to the minionData.
    /// </summary>
    public abstract class MinionDecorator
    {
        protected MinionType minionType;

        public MinionDecorator(MinionType minionType)
        {
            this.minionType = minionType;
        }

        //Decorates the MinionData with the correlation values.
        public abstract MinionData Decorate(MinionData minionData);
    }
}
