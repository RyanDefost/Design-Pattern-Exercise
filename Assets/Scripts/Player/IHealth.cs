using Project.GameLogic.EntityComponents;

namespace Project.Player
{
    public interface IHealth
    {
        public float Health { get; }

        public HealthComponent HealthSystem { get; }
    }
}
