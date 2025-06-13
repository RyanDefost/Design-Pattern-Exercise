using Project.GameLogic.Systems;
using Project.Player;
using UnityEngine;

namespace Project.Summon.Abilities
{
    /// <summary>
    /// Ability that heals the player that is close enough around the current minion.
    /// </summary>
    public class MinionWaterAbility : IAbility
    {
        private CollisionSystem collisionSystem = ISingleton<CollisionSystem>.Instance();

        // Activates the logic for the current Ability.
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
