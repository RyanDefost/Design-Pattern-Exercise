using System;
using UnityEngine;

namespace Project.GameLogic.EntityComponents
{
    /// <summary>
    /// A component for Entities that adds health and the functionally to change the amount. 
    /// </summary>
    public class HealthComponent
    {
        public Action OnDie;

        private float health;

        public HealthComponent(float health = 10)
        {
            this.health = health;
        }

        // Adds health based on the given amount.
        public void AddHealth(float health)
        {
            this.health += health;
        }

        // Removes health based on the given amount.
        public void RemoveHealth(float health)
        {
            Debug.Log(this.health + " :HEALTH");

            this.health -= health;

            if (this.health <= 0)
            {
                Die();
            }
        }

        // Sets the health based on the given amount.
        public void SetHealth(float health)
        {
            this.health = health;
        }

        // Sets health to 0 and handles the logic for dying Entities.
        public void Die()
        {
            this.health = 0;

            OnDie?.Invoke();
        }

        // Gets the current amount of health.
        public float GetHealth() => this.health;
    }
}
