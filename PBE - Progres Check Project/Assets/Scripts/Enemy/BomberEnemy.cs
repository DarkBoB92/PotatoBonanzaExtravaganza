using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberEnemy : MonoBehaviour
{
    // Pre-Requisites -----------------------------------------------------------------------------
    [SerializeField] float overlapRadius = 3.0f;
    [SerializeField] private int maxHealth;
    [SerializeField] private int damageToPlayer;
    int currentHealth;

    Health health;
    EnemyHealthBar healthBarScript;
    SpawnPoints spawnPoints;
    Throwable throwable;

    // Main Loops ---------------------------------------------------------------------------------
    void Start()
    {
        currentHealth = maxHealth;

        healthBarScript = GetComponent<EnemyHealthBar>();
        health = GetComponent<Health>();
        spawnPoints = FindObjectOfType<SpawnPoints>();
        throwable = GetComponent<Throwable>();
    }

    // Update is called once per frame
    void Update()
    {
        OverlappingTest();
    }

    // Functions ----------------------------------------------------------------------------------

    public void OverlappingTest()
    {
        Collider[] objectsInRange = Physics.OverlapSphere(transform.position, overlapRadius);
        foreach (Collider obj in objectsInRange)
        {
            Health playerHealth = obj.GetComponent<Health>();

            if (obj.GetType() == typeof(CapsuleCollider) && obj.CompareTag("Player")) // Trying to make it so it avoids BoxCollider!! :(
            {
                playerHealth.TakeDamage(damageToPlayer);
                Destroy(gameObject);
            }
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        healthBarScript.UpdateHealthBar(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            // spawnPoints.RemoveEnemyFromList(gameObject);
            Destroy(gameObject);
        }
        //   throwable.RemoveItemFromList(gameObject); //New added line
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Weapon"))
        {
            TakeDamage(4);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, overlapRadius);
    }
}
