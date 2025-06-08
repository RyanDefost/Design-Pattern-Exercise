using UnityEngine;

namespace Project.Summon.Abilities
{
    public class MinionAirAbility : IAbility
    {
        public void Activate(Minion minion)
        {
            Debug.Log("Activate AIR ability");
        }
    }
}
