using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BomberEnemy : MonoBehaviour
{
    // Pre-Requisites -----------------------------------------------------------------------------
    [SerializeField] float initialOverlapRadius = 3.0f;
    [SerializeField] float increasedOverlapRadius = 6.0f;
    [SerializeField] private int maxHealth;
    [SerializeField] private int damageToPlayer;
    [SerializeField] float detonation;
    int currentHealth;
    [SerializeField] LayerMask damagingObjects;   // Use this for referencing any enemies that NEED to be damaged by this enemy AS LONG as they have different tags.

    PlayerHealth playerHealth;
    PlayerHealth enemyHealth;
    EnemyHealthBar healthBarScript;
    SpawnPoints spawnPoints;
    GameObject potato;
    Transform potatoPos;

    // Main Loops ---------------------------------------------------------------------------------
    void Start()
    {
        FindObjectOfType<GameplayAudio>().AudioTrigger(GameplayAudio.SoundFXCat.Spawn, transform.position, 1f);
        currentHealth = maxHealth;

        healthBarScript = GetComponent<EnemyHealthBar>();
        spawnPoints = FindObjectOfType<SpawnPoints>();
        playerHealth = FindObjectOfType<PlayerHealth>();
        potato = GameObject.FindGameObjectWithTag("Player");
        potatoPos = potato.transform;
    }

    void Update()
    {
        DistanceCheck();
    }

    // Functions ----------------------------------------------------------------------------------
 
    void DistanceCheck()
    {
        if (potato != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, potatoPos.position);

            if (distanceToPlayer <= initialOverlapRadius) // if the distance to the player is less than the radius of the sphere then start the coroutine.
            {
                StartCoroutine(DelayBeforeDamage());
            }
        }
    }

    IEnumerator DelayBeforeDamage()
    {
        yield return new WaitForSeconds(detonation); // Modify that value for an increased or decreased delay.

        OverlappingPlayer();

        if(gameObject != null)
        {
            FindObjectOfType<GameplayAudio>().AudioTrigger(GameplayAudio.SoundFXCat.Death, transform.position, 0.5f);
            initialOverlapRadius = increasedOverlapRadius;
            spawnPoints.RemoveEnemyFromList(gameObject);
            Destroy(gameObject);
        }
    }
    /// <summary>
    /// Function to create a surrounding damage to the enemies within it. The radius is increased after the explosion to simulate a proper explosion. 
    /// If you require to change which enemies can be damaged. Modify the damagingObjects layer on the enemy - OTHERWISE IT WILL NOT WORK!!
    /// </summary>
    public void OverlappingPlayer()
    {
        Collider[] objectsInRange = Physics.OverlapSphere(transform.position, increasedOverlapRadius, damagingObjects);
        
        if(objectsInRange.Length > 0)
        {
            foreach (Collider obj in objectsInRange)
            {

                if (obj.CompareTag("Player") && playerHealth != null) // Null checks to make sure no NRE's happen.
                {
                    playerHealth = obj.GetComponent<PlayerHealth>();
                    playerHealth.TakeDamage(damageToPlayer); // Inspector dependent variable. Can be removed if you want to internally modify [Enemy damage right below]
                }
                if (obj.CompareTag("Enemy") && playerHealth != null)
                {
                    enemyHealth = obj.GetComponent<PlayerHealth>();
                    enemyHealth.TakeDamage(2); // Modify this value for damaging the enemies. Can do more if needed.
                }
            }
        }
    }
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        healthBarScript.UpdateHealthBar(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            FindObjectOfType<GameplayAudio>().AudioTrigger(GameplayAudio.SoundFXCat.Death, transform.position, 0.5f);
            spawnPoints.RemoveEnemyFromList(gameObject);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Weapon weapon = other.GetComponent<Weapon>();

        if (weapon != null)
        {
            if (other.gameObject.CompareTag("Weapon") && weapon.shooted)
            {
                TakeDamage(4);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, initialOverlapRadius);
    }
}
