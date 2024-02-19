using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberEnemy : MonoBehaviour
{
    EnemyTypes enemyType;
    [SerializeField] float overlapRadius = 3.0f;

    void Update()
    {
        OverlappingTest();
    }

    public void OverlappingTest()
    {
        Debug.Log("Current enemyType: " + enemyType);
        Collider[] objectsInRange = Physics.OverlapSphere(transform.position, overlapRadius);
        foreach (Collider obj in objectsInRange)
        {
            Health playerHealth = obj.GetComponent<Health>();
            if (obj.CompareTag("Player"))
            {
                playerHealth.TakeDamage(7);
                Destroy(gameObject);
            }
        }
    }
}
       
