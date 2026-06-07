using Project.GameLogic.EntityComponents;

namespace Project.GameLogic
{
    /// <summary>
    /// Interface to make sure all needed functionality is added to an Entity for the LocomotionComponent.
    /// </summary>
    public interface ILocomotion
    {
        public Entity Entity { get; }
        public CollisionComponent CollisionComponent { get; }
    }
}