using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillEnemy : MonoBehaviour
{
    private SpawnPoints spawnPoints;

    private void Start()
    {
        spawnPoints = FindObjectOfType<SpawnPoints>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if(spawnPoints != null)
            {
                spawnPoints.RemoveEnemyFromList(gameObject);
            }
            Destroy(gameObject);
        }
    }
}
