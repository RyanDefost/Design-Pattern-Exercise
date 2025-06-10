using System;

namespace Project.GameLogic
{
    public class HealthSystem
    {
        public Action OnDie;

        private float health;

        public HealthSystem(float health = 10)
        {
            this.health = health;
        }

        public void AddHealth(float health)
        {
            this.health += health;
        }

        public void RemoveHealth(float health)
        {
            this.health -= health;

            if (this.health <= 0)
            {
                Die();
            }
        }

        public void SetHealth(float health)
        {
            this.health = health;
        }


        public void Die()
        {
            this.health = 0;

            OnDie?.Invoke();
        }
        public float GetHealth() => this.health;
    }
}
