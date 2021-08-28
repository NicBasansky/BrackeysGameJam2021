using System;
using UnityEngine;

namespace NicLib.Health
{
    [RequireComponent(typeof(DeathHandler))]
    public class Health : MonoBehaviour
    {
        [SerializeField] float maxHealth = 20f;
        [SerializeField] float health;

        bool isDead = false;
        
        DeathHandler deathHandler;
        ScoreCalculator scoreCalculator;

        public event Action onDeath;
        public event Action onHealthUpdated;

        void Awake()
        {
            deathHandler = GetComponent<DeathHandler>();
            scoreCalculator = GetComponent<ScoreCalculator>();
        }

        void Start()
        {
            health = maxHealth;
            if (onHealthUpdated != null)
            {
                onHealthUpdated();
            }
           
        }

        public void AffectHealth(float delta)
        {
            if (isDead) return;

            AffectScore(delta);
            
            health += delta;
            health = Mathf.Clamp(health, 0, maxHealth);

            if (onHealthUpdated != null)
            {
                onHealthUpdated();
            }

            if (health <= 0)
            {
                Die();
                return;
            }
              
        }

        private void AffectScore(float delta)
        {
            float damage = delta * -1; // incoming damage is a negative number, this makes it positive.
            scoreCalculator.AddToScore_Damage(damage);
        }

        public float GetHealthFraction()
        {
            if (maxHealth <= 0)
            {
                Debug.Log("Max Health of " + transform.gameObject.name + " is 0");
                maxHealth = 1;
            }

            return health / maxHealth;
        }

        public bool GetIsDead()
        {
            return isDead;
        }
        
        // if AI they must have an AIController component
        private void Die()
        {
            isDead = true;
            

            if (onDeath != null)
            {
                onDeath();
            }
            
            if (deathHandler)
            {
                deathHandler.Kill();     
            }

            
        }

    }
}
