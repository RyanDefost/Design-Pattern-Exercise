using UnityEngine;

namespace Project.GameLogic
{
    public class LocomotionComponent<T> where T : ILocomotion
    {
        private T actor;
        private Entity target;

        private float speed;

        public LocomotionComponent(T actor, Entity target = null, float speed = 1)
        {
            this.actor = actor;
            this.target = target;

            this.speed = speed;
        }

        public void UpdateMovement()
        {
            if (this.target == null) return;

            Vector2 step = CalculateNextStep(this.actor.Entity.GetPosition(), this.target);
            actor.Entity.GameObject.transform.Translate(step);

            if (actor.CollisionComponent.colliding.Count > 0)
            {
                Debug.Log("TRY MOVE");
                actor.Entity.GameObject.transform.Translate(-step);
            }
        }

        private Vector2 CalculateNextStep(Vector2 position, Entity target)
        {
            Vector2 direction = (target.GameObject.transform.position - (Vector3)position);
            direction.Normalize();

            Vector2 step = direction * (speed * Time.deltaTime);
            return step;
        }

        public void SetTarget(Entity target) => this.target = target;
        public void SetSpeed(float speed) => this.speed = speed;
        public void SetActor(T actor) => this.actor = actor;
    }
}
