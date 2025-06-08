using UnityEngine;

namespace Project.Summon.Abilities
{
    public class MinionFireAbility : IAbility
    {
        public void Activate(Minion minion)
        {
            Debug.Log("Activate FIRE ability");
        }
    }
}
