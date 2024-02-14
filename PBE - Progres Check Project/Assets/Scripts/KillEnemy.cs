using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillEnemy : MonoBehaviour
{
    private SpawnPoints spawnPoints;
    private Throwable throwable;
    private void Start()
    {
        spawnPoints = FindObjectOfType<SpawnPoints>();
        throwable = GameObject.FindGameObjectWithTag("Player").GetComponent<Throwable>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Weapon")
        {
            if(spawnPoints != null)
            {
                spawnPoints.RemoveEnemyFromList(gameObject);
            }
            throwable.RemoveItemFromList(gameObject);

            Destroy(gameObject);
        }
    }
}
