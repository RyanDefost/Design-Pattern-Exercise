using Project.GameLogic.EntityComponents;
using Project.GameLogic.Systems;
using Project.Player;
using UnityEngine;

namespace Project.Summon.Abilities
{
    /// <summary>
    /// Ability that deals damage to surounding Entities bases on the range and Damage.
    /// </summary>
    public class MinionFireAbility : IAbility
    {
        private CollisionSystem collisionSystem = ISingleton<CollisionSystem>.Instance();

        // Activates the logic for the current Ability.
        public void Activate(Minion minion)
        {
            SetDamage(minion);
        }

        public void SetDamage(Minion minion)
        {
            for (int i = 0; i < collisionSystem.Colliders.Count; i++)
            {
                CollisionComponent collider =  collisionSystem.Colliders[i];
                float distance = Vector2.Distance(minion.GetPosition(), collider.actor.GetPosition());

                if (distance >= minion.minionData.areaOfEffect
                    || collider == minion.CollisionComponent) continue;

                if (collider.actor is IHealth)
                {
                    var component = (IHealth)collider.actor;

                    component.HealthSystem.RemoveHealth(minion.Damage);
                }
            }
        }
    }
}
