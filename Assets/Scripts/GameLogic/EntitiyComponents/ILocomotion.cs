using Project.GameLogic.EntityComponents;

namespace Project.GameLogic
{
    public interface ILocomotion
    {
        public Entity Entity { get; }
        public CollisionComponent CollisionComponent { get; }
    }
}