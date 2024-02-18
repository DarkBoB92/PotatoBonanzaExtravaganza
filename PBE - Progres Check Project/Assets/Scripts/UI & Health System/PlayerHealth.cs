using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private PlayerHealthBar healthBarScript;
    [SerializeField] int damage;
    [SerializeField] int maxHealth = 10;
    int currentHealth;

    private SpawnPoints spawnPoints;
    private Throwable throwable;


    // Main Loops --------------------------------------------------------------------------------- 

    void Start()
    {
        currentHealth = maxHealth;
        spawnPoints = FindObjectOfType<SpawnPoints>();
        throwable = GameObject.FindGameObjectWithTag("Player").GetComponent<Throwable>(); //Reference to the Throwable Script attached to the Player
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Weapon") || other.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(damage);
        }
    }



    // Functions ---------------------------------------------------------------------------------- 

    void TakeDamage(int amount)
    {
        currentHealth -= amount;
        healthBarScript.UpdateHealthBar(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            spawnPoints.RemoveEnemyFromList(gameObject);
            Destroy(gameObject);
        }
        throwable.RemoveItemFromList(gameObject); //New added line
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
