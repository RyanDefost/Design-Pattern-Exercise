namespace Project.Summon.Abilities
{
    /// <summary>
    /// Identifier for classes that can be assigned as abilities.
    /// </summary>
    public interface IAbility
    {
        // Activates the logic for the current Ability.
        public void Activate(Minion minion);
    }
}
