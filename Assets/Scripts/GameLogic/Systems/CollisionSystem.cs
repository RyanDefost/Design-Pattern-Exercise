using Project.GameLogic.EntityComponents;
using System.Collections.Generic;

namespace Project.GameLogic.Systems
{
    /// <summary>
    /// A system that checks for any collision each frame and sets the collisions to the hit collider.
    /// </summary>
    public class CollisionSystem : GameBehaviour, ISingleton<CollisionSystem>
    {
        private List<CollisionComponent> colliders = new List<CollisionComponent>();
        public List<CollisionComponent> Colliders { get { return colliders; } }

        public CollisionSystem()
        {
            ISingleton<CollisionSystem>.instance = this;
        }

        // Updates the collisions of every collider.
        public override void Update()
        {
            for (int i = 0; i < colliders.Count; i++)
            {
                CheckCollisions(colliders[i]);
            }
        }

        // Adds a collider to the list of colliders that will be checked for collision.
        public void SubscribeCollider(CollisionComponent collider)
        {
            this.colliders.Add(collider);
        }

        // Removes a collider from the list of colliders.
        public void UnSubscribeCollider(CollisionComponent collider)
        {
            this.colliders.Remove(collider);
        }

        // Checks if the given collider is currently colliding with another collider.
        public bool CheckCollisions(CollisionComponent self)
        {
            bool isColliding = false;

            for (int i = 0; i < colliders.Count; i++)
            {
                if (colliders[i] == self) continue;

                if (self.CheckCollision(colliders[i]))
                {
                    colliders[i].OnHit?.Invoke(self);
                    isColliding = true;
                }
            }

            return isColliding;
        }
    }
}
