using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    // Pre-Requisites ----------------------------------------------------------------------------- 

    [SerializeField] private EnemyHealthBar healthBarScript;
    [SerializeField] int damage;
    [SerializeField] int maxHealth = 10;
    [SerializeField] private float damageCooldown = 1.5f;
    int currentHealth;
    private float delayBetweenDamage;

    private SpawnPoints spawnPoints;
    private Throwable throwable;

    // Main Loops --------------------------------------------------------------------------------- 

    void Start()
    {
        delayBetweenDamage = Time.time;
        currentHealth = maxHealth;
        spawnPoints = FindObjectOfType<SpawnPoints>();
        throwable = GameObject.FindGameObjectWithTag("Player").GetComponent<Throwable>(); //Reference to the Throwable Script attached to the Player
    }

    // Functions ---------------------------------------------------------------------------------- 

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Time.time >= delayBetweenDamage)
            {
                Health playerHealth = other.gameObject.GetComponent<Health>();
                if (playerHealth != null)
                {
                    Debug.Log("Damaged player!");
                    playerHealth.TakeDamage(2);
                }
                delayBetweenDamage = Time.time + damageCooldown;
            }
        }
    }

    public void TakeDamage(int amount)
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
