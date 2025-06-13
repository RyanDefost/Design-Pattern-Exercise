using UnityEngine;

namespace Project.Summon.Abilities
{
    /// <summary>
    /// Ability that spawns a obstacle into the world that has to bee avoided.
    /// </summary>
    public class MinionEarthAbility : IAbility
    {
        // Activates the logic for the current Ability.
        public void Activate(Minion minion)
        {
            Debug.Log("Activate EARTH ability");
        }
    }
}
