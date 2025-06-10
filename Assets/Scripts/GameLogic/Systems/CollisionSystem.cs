using Project.GameLogic.EntityComponents;
using System.Collections.Generic;

namespace Project.GameLogic.Systems
{
    public class CollisionSystem : GameBehaviour, ISingleton<CollisionSystem>
    {
        private List<CollisionComponent> colliders = new List<CollisionComponent>();
        public List<CollisionComponent> Colliders { get { return colliders; } }

        public CollisionSystem()
        {
            ISingleton<CollisionSystem>.instance = this;
        }

        public override void Update()
        {
            for (int i = 0; i < colliders.Count; i++)
            {
                CheckCollisions(colliders[i]);
            }
        }

        public void SubscribeCollider(CollisionComponent collider)
        {
            this.colliders.Add(collider);
        }

        public void UnSubscribeCollider(CollisionComponent collider)
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
