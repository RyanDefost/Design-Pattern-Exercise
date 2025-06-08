using UnityEngine;

namespace Project.Summon.Abilities
{
    public class MinionWaterAbility : IAbility
    {
        public void Activate(Minion minion)
        {
            Debug.Log("Activate WATER ability");
        }
    }
}
