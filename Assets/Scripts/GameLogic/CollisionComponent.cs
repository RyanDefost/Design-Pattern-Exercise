using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project.GameLogic
{
    public class CollisionComponent
    {
        public bool active = true;
        public bool isTrigger = false;

        public List<CollisionComponent> colliding = new List<CollisionComponent>();

        public float sizeMultiplier = 1;

        public Entity actor { get; }

        public Action<CollisionComponent> OnHit;

        private CollisionSystem collisionSystem = ISingleton<CollisionSystem>.Instance();


        public CollisionComponent(Entity actor)
        {
            this.actor = actor;

            collisionSystem.SubscribeCollider(this);
            this.active = true;
        }

        public bool CheckCollision(CollisionComponent collider)
        {
            if (collider.active == false || collider.isTrigger) return false;

            Vector2 pos1 = collider.actor.GetPosition();
            Vector2 pos2 = actor.GetPosition() * sizeMultiplier;

            //AABB-collision
            if (pos1.x < pos2.x + collider.actor.GetScale().x / 2 &&
                pos1.x + actor.GetScale().x / 2 > pos2.x &&
                pos1.y < pos2.y + collider.actor.GetScale().y / 2 &&
                pos1.y + actor.GetScale().y / 2 > pos2.y)
            {
                return true;
            }

            return false;
        }

        public bool CheckCollision()
        {
            return collisionSystem.CheckCollisions(this);
        }

        public void Destroy()
        {
            collisionSystem.UnsubscribeCollider(this);
        }
    }
}
