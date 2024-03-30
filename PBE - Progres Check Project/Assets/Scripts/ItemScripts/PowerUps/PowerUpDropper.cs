using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpDropper : MonoBehaviour
{
    SpawnPoints wave;
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] GameObject[] powerUps; // Array content order: 0 = RestoreHealth, 1 = DamageUp, 2 = FireRate, 3 = ThrowForce, 4 = ExplosionDelay, 5 = KnifeFireMode, 6 = EggFireMode
    [SerializeField] bool spawning, changed;

    private void Start()
    {
        wave = GameObject.FindGameObjectWithTag("Spawner").GetComponent<SpawnPoints>();
    }

    private void Update()
    {
        if(wave != null && wave.inBreak)
        {
            CheckWave();
        }
        else
        {
            changed = false;
        }
        Drop();
    }

    void Drop()
    {
        if (wave != null && spawning && changed)
        {
            GameObject powerUp;
            switch (wave.waveCounter)
            {
                case 3:
                    powerUp = Instantiate(powerUps[0], spawnPoints[0].position, spawnPoints[0].rotation);
                    powerUp = Instantiate(powerUps[1], spawnPoints[1].position, spawnPoints[1].rotation);
                    powerUp = Instantiate(powerUps[2], spawnPoints[2].position, spawnPoints[2].rotation);
                    powerUp = Instantiate(powerUps[3], spawnPoints[3].position, spawnPoints[3].rotation);
                    powerUp = Instantiate(powerUps[4], spawnPoints[4].position, spawnPoints[4].rotation);
                    powerUp = Instantiate(powerUps[5], spawnPoints[5].position, spawnPoints[5].rotation);
                    powerUp = Instantiate(powerUps[6], spawnPoints[6].position, spawnPoints[6].rotation);
                    break;
                case 6:
                    powerUp = Instantiate(powerUps[0], spawnPoints[0].position, spawnPoints[0].rotation);
                    powerUp = Instantiate(powerUps[1], spawnPoints[1].position, spawnPoints[1].rotation);
                    powerUp = Instantiate(powerUps[2], spawnPoints[2].position, spawnPoints[2].rotation);
                    powerUp = Instantiate(powerUps[3], spawnPoints[3].position, spawnPoints[3].rotation);
                    powerUp = Instantiate(powerUps[4], spawnPoints[4].position, spawnPoints[4].rotation);
                    powerUp = Instantiate(powerUps[5], spawnPoints[5].position, spawnPoints[5].rotation);
                    powerUp = Instantiate(powerUps[6], spawnPoints[6].position, spawnPoints[6].rotation);
                    break;
                case 9:
                    powerUp = Instantiate(powerUps[0], spawnPoints[0].position, spawnPoints[0].rotation);
                    powerUp = Instantiate(powerUps[1], spawnPoints[1].position, spawnPoints[1].rotation);
                    powerUp = Instantiate(powerUps[2], spawnPoints[2].position, spawnPoints[2].rotation);
                    powerUp = Instantiate(powerUps[3], spawnPoints[3].position, spawnPoints[3].rotation);
                    powerUp = Instantiate(powerUps[4], spawnPoints[4].position, spawnPoints[4].rotation);
                    powerUp = Instantiate(powerUps[5], spawnPoints[5].position, spawnPoints[5].rotation);
                    powerUp = Instantiate(powerUps[6], spawnPoints[6].position, spawnPoints[6].rotation);
                    break;
                case 12:
                    powerUp = Instantiate(powerUps[0], spawnPoints[0].position, spawnPoints[0].rotation);
                    powerUp = Instantiate(powerUps[1], spawnPoints[1].position, spawnPoints[1].rotation);
                    powerUp = Instantiate(powerUps[2], spawnPoints[2].position, spawnPoints[2].rotation);
                    powerUp = Instantiate(powerUps[3], spawnPoints[3].position, spawnPoints[3].rotation);
                    powerUp = Instantiate(powerUps[4], spawnPoints[4].position, spawnPoints[4].rotation);
                    powerUp = Instantiate(powerUps[5], spawnPoints[5].position, spawnPoints[5].rotation);
                    powerUp = Instantiate(powerUps[6], spawnPoints[6].position, spawnPoints[6].rotation);
                    break;
                case 15:
                    powerUp = Instantiate(powerUps[0], spawnPoints[0].position, spawnPoints[0].rotation);
                    powerUp = Instantiate(powerUps[1], spawnPoints[1].position, spawnPoints[1].rotation);
                    powerUp = Instantiate(powerUps[2], spawnPoints[2].position, spawnPoints[2].rotation);
                    powerUp = Instantiate(powerUps[3], spawnPoints[3].position, spawnPoints[3].rotation);
                    powerUp = Instantiate(powerUps[4], spawnPoints[4].position, spawnPoints[4].rotation);
                    break;
                default:
                    powerUp = Instantiate(powerUps[0], spawnPoints[0].position, spawnPoints[0].rotation);
                    break;
            }
            spawning = false;
        }
    }

    void CheckWave()
    {
        if (wave != null && wave.inBreak && !changed)
        {
            switch (wave.waveCounter)
            {
                case 3:
                    changed = true;
                    spawning = true;
                    break;
                case 6:
                    changed = true;
                    spawning = true;
                    break;
                case 9:
                    changed = true;
                    spawning = true;
                    break;
                case 12:
                    changed = true;
                    spawning = true;
                    break;
                case 15:
                    changed = true;
                    spawning = true;
                    break;
                default:
                    spawning = true;
                    changed = true;
                    break;
            }
        }
    }
}
