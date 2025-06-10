using Project.GameLogic;

namespace Project.Player
{
    public interface IHealth
    {
        public float Health { get; }

        public HealthSystem HealthSystem { get; }
    }
}
