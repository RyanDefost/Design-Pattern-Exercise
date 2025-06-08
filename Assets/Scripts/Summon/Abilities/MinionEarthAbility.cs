using UnityEngine;

namespace Project.Summon.Abilities
{
    public class MinionEarthAbility : IAbility
    {
        public void Activate(Minion minion)
        {
            Debug.Log("Activate EARTH ability");
        }
    }
}
