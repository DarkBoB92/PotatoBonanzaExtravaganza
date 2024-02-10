using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnPoints : MonoBehaviour
{
    // Pre-Requisite Variables --------------------------------------------------------------------

    
    // Wave Variables -----------------------------------------------------------------------------
    public List<Enemies> enemies = new List<Enemies>();
    public List<GameObject> spawnEnemies = new List<GameObject>();
    public List<GameObject> spawnedEnemies = new List<GameObject>(); // GameObject list is used as spawnedEnemies will be of type GameObject therefore tracking it by its type.
    [SerializeField] public Transform[] spawnPoint;
    public int currentWave;
    public int waveValue;
    public int difficultyMultiplier;
    private bool countdownPrinted = false;

    // Spawn Variables -----------------------------------------------------------------------------
    public int waveDuration; // How long the wave should last before moving on to next.
    private float waveCountdown; // Countdown timer, how long is left.
    private float spawnRate; // Enemy spawn rate.
    private float spawnDelay; // Enemy spawn delay.

    [SerializeField] private GameObject enemiesParent;

    [HideInInspector] public KillEnemy collision;

    [System.Serializable]
    public class Enemies
    {
        public GameObject enemyP; // EnemyPrefab
        public int enemyValue; // "Cost" of enemy. Think of it as a shop.

    }


    // Main Loops ---------------------------------------------------------------------------------

    private void Awake()
    {
        ResetLists();
    }

    void Start()
    {
        CreatingWave();
    }

    void FixedUpdate()
    {
        if (spawnDelay <= 0)
        {
            // spawning an enemy
            SpawnEnemy();

        }
        else
        {
            spawnDelay -= Time.fixedDeltaTime;
        }
    
        
        waveCountdown -= Time.fixedDeltaTime;
        waveCountdown = Mathf.Max(0, waveCountdown);

        float roundedCountdown = Mathf.Round(waveCountdown);

        if (roundedCountdown >= 0 || !countdownPrinted)
        {
            Debug.Log(roundedCountdown);
            countdownPrinted = true;
        }

        if (roundedCountdown <= 0 && spawnedEnemies.Count == 0)
        {
            currentWave++;
            CreatingWave();
            roundedCountdown = 0;
            countdownPrinted = false;
        }
    }
    // Functions ----------------------------------------------------------------------------------

    public void CreatingWave()
    {
        waveValue = currentWave * difficultyMultiplier; // Scaling of the waves, on wave 2 there will be 20 points to spend and so on. Adjust the multiplier for a harder / easier difficulty curve.

        if(spawnEnemies.Count > 0)
        {
            spawnRate = waveDuration / spawnEnemies.Count;
            waveCountdown = waveDuration;
        }

        spawnedEnemies.Clear();
        SpawningEnemies();
    }

    private void AttachCollision()
    {
        List<GameObject> enemiesToAddCollision = new List<GameObject>();
        
        foreach (GameObject enemy in spawnedEnemies)
        {
            if (enemy.GetComponent<KillEnemy>() == null)
            {
                KillEnemy collisionInstance = enemy.AddComponent<KillEnemy>();
                enemiesToAddCollision.Add(enemy);
            }
        }

        foreach (GameObject enemy in enemiesToAddCollision)
        {
            
        }
    }

    public void RemoveEnemyFromList(GameObject enemy)
    {
        if (spawnedEnemies.Contains(enemy))
        {
            spawnedEnemies.Remove(enemy);
            Destroy(enemy);
        }
        else
        {
            Debug.LogWarning("Trying to a remove an enemy not in SpawnedEnemy");
        }
    }

    public void SpawningEnemies()
    {
        spawnEnemies.Clear();

        while (waveValue > 0 && enemies.Count > 0)
        {
            int randomEnemy = Random.Range(0, enemies.Count); // Random generation from 0 to the length of the List.
            int randomEnemyValue = enemies[randomEnemy].enemyValue; // Extracting the value of the enemy based on the enemy spawned.
            int remainingWaveValue = waveValue - randomEnemyValue;


            if (remainingWaveValue >= 0)
            {
                spawnEnemies.Add(enemies[randomEnemy].enemyP);
                waveValue -= randomEnemyValue;
            }

            else if (waveValue <= 0)
            {
                break;
            }
        }
    }

    void SpawnEnemy()
    {
        if (spawnEnemies.Count > 0)
        {
            int randomSpawnIndex = Random.Range(0, spawnPoint.Length);

            if (randomSpawnIndex < spawnPoint.Length) // If the generated random spawnIndex from 0-spawnPoint length is greater than or equal to 0 but less than the amount of spawnPoints.
            {
                Vector3 spawnPosition = spawnPoint[randomSpawnIndex].position; // Create a new spawnPosition and assign it to the spawnPositions position based on the indexed spawnPoint.

                GameObject enemy = Instantiate(spawnEnemies[0], spawnPosition, Quaternion.identity); // instantiation with a 0 rotation, directed at the concluded spawnPosition based on enemyPrefab
                enemy.transform.parent = enemiesParent.transform; // Adds all instantiated enemies under the Enemy parent 

                AttachCollision();

                spawnEnemies.RemoveAt(0); // Remove it from the list. Does not remove from scene.
                spawnDelay = spawnRate;

                spawnedEnemies.Add(enemy);
            }
            else
            {
                waveCountdown = 0;
            }
        }
    }
     void ResetLists()
    {
        spawnEnemies.Clear();
        spawnedEnemies.Clear();
        currentWave = 1;
    }
}
