using System.Collections.Generic;

namespace Project.GameLogic
{
    public class CollisionSystem : ISingleton<CollisionSystem>
    {
        private List<CollisionComponent> colliders = new List<CollisionComponent>();

        public CollisionSystem()
        {
            ISingleton<CollisionSystem>.instance = this;
        }

        public void SubscribeCollider(CollisionComponent collider)
        {
            this.colliders.Add(collider);
        }

        public void UnsubscribeCollider(CollisionComponent collider)
        {
            this.colliders.Remove(collider);
        }

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
