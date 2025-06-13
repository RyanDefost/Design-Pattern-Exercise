namespace Project.Summon
{
    /// <summary>
    /// Identifier for classes that can damage other colliding objects.
    /// </summary>
    public interface IDamager
    {
        public float Damage { get; }
    }
}