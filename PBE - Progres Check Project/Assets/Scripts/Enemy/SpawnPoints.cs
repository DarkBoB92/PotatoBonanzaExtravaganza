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
    [HideInInspector] public int waveValue;
    public int difficultyMultiplier;
    private bool countdownPrinted = false;
    private bool stopWaveIncrement = true;

    // Spawn Variables -----------------------------------------------------------------------------
    public int waveDuration; // How long the wave should last before moving on to next.
    private float waveCountdown; // Countdown timer, how long is left.
    [SerializeField] public float spawnRate; // Enemy spawn rate.
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
        spawnEnemies.Clear();
        spawnedEnemies.Clear();
        currentWave = 1;
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
            spawnDelay -= Time.fixedDeltaTime; // Decrease spawnDelay by a fixed time.
        }


        waveCountdown -= Time.fixedDeltaTime;
        waveCountdown = Mathf.Max(0, waveCountdown);

        float roundedCountdown = Mathf.Round(waveCountdown);

        if (roundedCountdown >= 0 || !countdownPrinted)
        {
            countdownPrinted = true;
        }

        if (roundedCountdown <= 0 && spawnedEnemies.Count == 0 && stopWaveIncrement)
        {
            currentWave++;
            if (currentWave <= 3)
            {
                CreatingWave();
            }
            else
            {
                currentWave = 1;
                stopWaveIncrement = false;
                Debug.Log("Max wave reached!");
                CreatingWave();
                Debug.Log("Restarting waves!");
            }
        }
    }
    // Functions ----------------------------------------------------------------------------------

    public void CreatingWave()
    {
        stopWaveIncrement = true;
        waveValue = currentWave * difficultyMultiplier; // Scaling of the waves, on wave 2 there will be 20 points to spend and so on. Adjust the multiplier for a harder / easier difficulty curve.
        SpawningEnemies();

        if (spawnEnemies.Count > 0)
        {
            float enemySpawnRate = spawnRate;
            waveCountdown = waveDuration;
        }

        spawnedEnemies.Clear();
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
    /// <summary>
    /// Function to instantiate enemies into a list to 'ready' them for spawning. Afterwards they will be added to a spawnedEnemies list where they are actively being tracked until death.
    /// Note: the randomIndex simply refers to the amount of possible spawnPoints that it has. If the game has 3, the randomIndex will generate from 0-3; where they are all the spawnPoints.
    /// </summary>
    void SpawnEnemy()
    {   
        if (spawnEnemies.Count > 0)
        {
            int randomIndex = GenerateRandomIndex();
            // Add functionality so that it cannot spawn in the same place after it has already spawned. Has to choose from the other two SP's before spawning again. Then loop.

            if (randomIndex < spawnPoint.Length) // If the generated random spawnIndex from 0-spawnPoint length is greater than or equal to 0 but less than the amount of spawnPoints.
            {
                
                Vector3 spawnPosition = spawnPoint[randomIndex].position; // Retrieval of the position of the spawn at the index generated.

                GameObject enemy = Instantiate(spawnEnemies[0], spawnPosition, Quaternion.identity); // Creates an enemy object using the first element of the spawnEnemies list at the specified position.

                if (enemy != null)
                {
                    enemy.transform.parent = enemiesParent.transform; // Adds all instantiated enemies under the Enemy parent 
                    spawnedEnemies.Add(enemy);
                    spawnEnemies.RemoveAt(0); // Remove the first element from the spawnEnemies list.
                    spawnDelay = spawnRate;
                }
            }
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

    public int GenerateRandomIndex()
    {
        int randomSpawnIndex = Random.Range(0, spawnPoint.Length);
        return randomSpawnIndex;
    }
}
