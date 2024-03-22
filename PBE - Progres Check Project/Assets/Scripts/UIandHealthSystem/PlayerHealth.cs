using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // Pre-Requisites ----------------------------------------------------------------------------- 

    [SerializeField] private PlayerHealthBar healthBarScript;
    [SerializeField] private EnemyHealthBar enemyBarScript;
    [SerializeField] private GameObject weapon;
    [SerializeField] int damage;
    [SerializeField] int maxHealth = 10;
    [SerializeField] private float damageCooldown = 1.5f;
    int currentHealth;
    private float delayBetweenDamage;

    private SpawnPoints spawnPoints;

    // Main Loops --------------------------------------------------------------------------------- 

    void Start()
    {
        delayBetweenDamage = Time.time;
        currentHealth = maxHealth;
        spawnPoints = FindObjectOfType<SpawnPoints>();
    }

    // Functions ---------------------------------------------------------------------------------- 
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) // Refers to any trigger entering the Player's collider. Note: ensure enemies that are not meant to be triggered by this are accounted for.
        {
            if (Time.time >= delayBetweenDamage)
            {
                PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
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
        if (gameObject.tag == "Player")
        {
            healthBarScript.UpdateHealthBar(currentHealth, maxHealth);
        }
        else if (gameObject.tag == "Enemy" || gameObject.tag == "Bomber")
        {
            enemyBarScript.UpdateHealthBar(currentHealth, maxHealth);

            if (currentHealth <= 0)
            {
                spawnPoints.RemoveEnemyFromList(gameObject);
                Destroy(gameObject);
                GameObject spawnAmmo = Instantiate(weapon, transform.position + transform.up * 1, Quaternion.identity);
            }
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
