using Project.GameLogic.EntityComponents;

namespace Project.Player
{
    /// <summary>
    /// Identifier for classes that have an HealthComponent.
    /// </summary>
    public interface IHealth
    {
        public float Health { get; }

        public HealthComponent HealthSystem { get; }
    }
}
