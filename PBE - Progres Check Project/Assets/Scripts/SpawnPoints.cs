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

    // Spawn Variables -----------------------------------------------------------------------------
    public int waveDuration; // How long the wave should last before moving on to next.
    private float waveCountdown; // Countdown timer, how long is left.
    private float spawnRate; // Enemy spawn rate.
    private float spawnDelay; // Enemy spawn delay.

    [SerializeField] private GameObject enemiesParent;

    [System.Serializable]
    public class Enemies
    {
        public GameObject enemyP; // EnemyPrefab
        public int enemyValue; // "Cost" of enemy. Think of it as a shop.

    }


    // Main Loops ---------------------------------------------------------------------------------

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
            waveCountdown -= Time.fixedDeltaTime;
        }
    }
    // Functions ----------------------------------------------------------------------------------


    public void CreatingWave()
    {
        waveValue = currentWave * difficultyMultiplier; // Scaling of the waves, on wave 2 there will be 20 points to spend and so on. Adjust the multiplier for a harder / easier difficulty curve.
        SpawningEnemies();

        spawnRate = waveDuration / spawnEnemies.Count;
        waveCountdown = waveDuration;
    }

    public void SpawningEnemies()
    {
        while (waveValue > 0 && enemies.Count > 0)
        {
            int randomEnemy = Random.Range(0, enemies.Count); // Random generation from 0 to the length of the List.
            int randomEnemyValue = enemies[randomEnemy].enemyValue; // Extracting the value of the enemy based on the enemy spawned.
            int remainingWaveValue = waveValue - randomEnemyValue;


            if (remainingWaveValue >= 0)
            {
                spawnedEnemies.Add(enemies[randomEnemy].enemyP);
                waveValue -= randomEnemyValue;
            }

            else if (waveValue <= 0)
            {
                break;
            }
        }
        spawnEnemies.Clear(); // Enemies dead.
        spawnEnemies = spawnedEnemies;
    }

    void SpawnEnemy()
    {
        if (spawnEnemies.Count > 0)
        {
            int randomSpawnIndex = Random.Range(0, spawnPoint.Length);

            if (randomSpawnIndex < spawnPoint.Length) // If the generated random spawnIndex from 0-spawnPoint length is greater than or equal to 0 but less than the amount of spawnPoints.
            {
                Vector3 spawnPosition = spawnPoint[randomSpawnIndex].position; // Create a new spawnPosition and assign it to the spawnPositions position based on the indexed spawnPoint.

                GameObject enemy = Instantiate(spawnEnemies[0], spawnPosition, Quaternion.identity); // instantiation with a 0 rotation, directed at the concluded spawnPosition based on enemyPrefab.

                enemy.transform.parent = enemiesParent.transform; // Adds all instantiated enemies under the Enemy parent 

                spawnEnemies.RemoveAt(0); // Remove it from the list. Does not remove from scene.
                spawnDelay = spawnRate;

            }
            else
            {
                waveCountdown = 0;
            }
        }
    }
}
