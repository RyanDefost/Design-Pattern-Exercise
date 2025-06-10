using Project.GameLogic.Systems;
using Project.Player;
using UnityEngine;

namespace Project.Summon.Abilities
{
    public class MinionFireAbility : IAbility
    {
        private CollisionSystem collisionSystem = ISingleton<CollisionSystem>.Instance();

        public void Activate(Minion minion)
        {
            SetDamage(minion);
        }

        public void SetDamage(Minion minion)
        {
            foreach (var collider in collisionSystem.Colliders)
            {
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
