using Project.GameLogic;
using UnityEngine;

namespace Project.Summon.Abilities
{
    public class MinionAirAbility : IAbility
    {
        private CollisionComponent collisionComponent;

        public void Activate(Minion minion)
        {
            collisionComponent = new CollisionComponent(minion);
            collisionComponent.sizeMultiplier = minion.minionData.areaOfEffect;

            GetCollidersInArea(collisionComponent);

            Debug.Log("Activate AIR ability");
        }

        private void GetCollidersInArea(CollisionComponent collider)
        {
            collider.CheckCollision();
        }
    }
}
