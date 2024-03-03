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
    [SerializeField] LayerMask damagingObjects;

    PlayerHealth playerHealth;
    PlayerHealth enemyHealth;
    EnemyHealthBar healthBarScript;
    SpawnPoints spawnPoints;

    // Main Loops ---------------------------------------------------------------------------------
    void Start()
    {
        currentHealth = maxHealth;

        healthBarScript = GetComponent<EnemyHealthBar>();
        spawnPoints = FindObjectOfType<SpawnPoints>();        
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Functions ----------------------------------------------------------------------------------
 
    void DistanceCheck()
    {
        //TODO: Write a distance check -> delay when it's reached [animation] -> after delay, call OverlappingPlayer() [It'll check for anything in the range and then do damage]. 
    }

    public void OverlappingPlayer()
    {
        Collider[] objectsInRange = Physics.OverlapSphere(transform.position, overlapRadius, damagingObjects);
        
        if(objectsInRange.Length > 0)
        {
            foreach (Collider obj in objectsInRange)
            {
                if (obj.CompareTag("Player")) // Trying to make it so it avoids BoxCollider!! :(
                {
                    playerHealth = obj.GetComponent<PlayerHealth>();
                    playerHealth.TakeDamage(damageToPlayer);
                }
                if (obj.CompareTag("Enemy"))
                {
                    enemyHealth = obj.GetComponent<PlayerHealth>();
                    enemyHealth.TakeDamage(2);
                }
            }
            Destroy(gameObject);
        }
    }

    public void OverlappingEnemy()
    {
        Collider[] objectsInRange = Physics.OverlapSphere(transform.position, overlapRadius);
        foreach (Collider obj in objectsInRange)
        {
            enemyHealth = obj.GetComponent<PlayerHealth>();

            if (obj.CompareTag("Enemy")) // Trying to make it so it avoids BoxCollider!! :(
            {
                enemyHealth.TakeDamage(4);
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
            spawnPoints.RemoveEnemyFromList(gameObject);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Weapon"))
        {
            TakeDamage(4);
            //if (currentHealth <= 0)
            //{
            //    OverlappingEnemy();
            //}
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, overlapRadius);
    }
}
