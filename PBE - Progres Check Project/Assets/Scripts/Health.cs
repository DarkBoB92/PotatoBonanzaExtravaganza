using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    // Pre-Requisites -----------------------------------------------------------------------------

    [SerializeField] private EnemyHealthBar healthBarScript;
    [SerializeField] int damage;
    [SerializeField] int maxHealth = 10;
    int currentHealth;



    // Main Loops ---------------------------------------------------------------------------------

    void Start()
    {
        currentHealth = maxHealth;
    }

    private void OnTriggerEnter(Collider other)
    {
        TakeDamage(damage);
    }



    // Functions ----------------------------------------------------------------------------------

    void TakeDamage(int amount)
    {
        currentHealth -= amount;
        healthBarScript.UpdateHealthBar(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    void Heal(int amount)
    {
        currentHealth += amount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
}
