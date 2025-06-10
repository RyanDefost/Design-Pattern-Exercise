using Project.GameLogic;
using Project.Player;
using UnityEngine;

namespace Project.Summon.Abilities
{
    public class MinionWaterAbility : IAbility
    {
        private CollisionSystem collisionSystem = ISingleton<CollisionSystem>.Instance();

        public void Activate(Minion minion)
        {
            SetHealing(minion);
        }

        public void SetHealing(Minion minion)
        {
            foreach (var collider in collisionSystem.Colliders)
            {
                float distance = Vector2.Distance(minion.GetPosition(), collider.actor.GetPosition());

                if (distance >= minion.minionData.areaOfEffect
                    || collider == minion.CollisionComponent) continue;

                if (collider.actor is IHealth)
                {
                    var component = (IHealth)collider.actor;

                    component.HealthSystem.AddHealth(minion.Damage);
                }
            }
        }
    }
}
