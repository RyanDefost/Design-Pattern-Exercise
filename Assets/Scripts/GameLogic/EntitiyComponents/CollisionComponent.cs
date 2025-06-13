using Project.GameLogic.Systems;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project.GameLogic.EntityComponents
{
    /// <summary>
    /// A component for Entities that checks for any collision bases on the size of its actor's GameObject.
    /// </summary>
    public class CollisionComponent
    {
        public Entity actor { get; }

        public float sizeMultiplier = 1;

        public List<CollisionComponent> colliding = new List<CollisionComponent>();

        public Action<CollisionComponent> OnHit;

        private CollisionSystem collisionSystem = ISingleton<CollisionSystem>.Instance();
        private bool active = true;

        public CollisionComponent(Entity actor)
        {
            this.actor = actor;
        }

        // Checks for possible collision with the give collider, adds the collider to colliding when true.
        public bool CheckCollision(CollisionComponent collider)
        {
            if (collider.active == false) return false;

            Vector2 pos1 = collider.actor.GetPosition();
            Vector2 pos2 = actor.GetPosition();

            //AABB-collision
            if (pos1.x < pos2.x + collider.actor.GetScale().x / 2 &&
                pos1.x + actor.GetScale().x / 2 > pos2.x &&
                pos1.y < pos2.y + collider.actor.GetScale().y / 2 &&
                pos1.y + actor.GetScale().y / 2 > pos2.y)
            {
                this.colliding.Add(collider);
                return true;
            }

            this.colliding.Remove(collider);
            return false;
        }

        private void SetColliding(CollisionComponent collision, bool state)
        {
            if (state = true && !colliding.Contains(collision))
            {
                this.colliding.Add(collision);
            }

            if (state = false && colliding.Contains(collision))
            {
                this.colliding.Remove(collision);
            }
        }

        // Deactivates the collider so it won't be checked by the CollisionSystem.
        public void Deactivate()
        {
            collisionSystem.UnSubscribeCollider(this);
            this.active = false;

            colliding.Clear();
        }

        // Activates the collider so it can be checked by the CollisionSystem.
        public void Activate()
        {
            collisionSystem.SubscribeCollider(this);
            this.active = true;
        }
    }
}
