using Project.GameLogic.Systems;
using UnityEngine;

namespace Project.Summon.Abilities
{
    public class MinionAirAbility : IAbility
    {
        private CollisionSystem collisionSystem = ISingleton<CollisionSystem>.Instance();

        public void Activate(Minion minion)
        {
            SetSpeed(minion);
        }

        public void SetSpeed(Minion minion)
        {
            foreach (var collider in collisionSystem.Colliders)
            {
                float distance = Vector2.Distance(minion.GetPosition(), collider.actor.GetPosition());

                if (distance >= minion.minionData.areaOfEffect
                    || collider == minion.CollisionComponent) continue;

                if (collider.actor is Player.Player)
                {
                    var component = (Player.Player)collider.actor;

                    component.speed += 1.2f;
                }
            }
        }
    }
}
