using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyTypes : MonoBehaviour
{

    // private float blinkTimer;
    [SerializeField] EnemyType enemyType;
    [SerializeField] float overlapRadius = 3.0f;
    private int damage;
    private int maxHealth;

    Health health;


    void Start()
    {
        health = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        OverlappingTest();
    }
    
    public void OverlappingTest()
    {
        Debug.Log("Current enemyType: " + enemyType);
        Collider[] objectsInRange = Physics.OverlapSphere(transform.position, overlapRadius);
        foreach(Collider obj in objectsInRange)
        {
            Health playerHealth = obj.GetComponent<Health>();
            if (obj.CompareTag("Player"))
            {
                switch(enemyType)
                {
                    case EnemyType.Bomber:
                        playerHealth.TakeDamage(7);
                        Destroy(gameObject);
                        break;

                    case EnemyType.Melee:
                        playerHealth.TakeDamage(1);
                        Destroy(gameObject);
                        break;

                    case EnemyType.Ranged:
                        playerHealth.TakeDamage(2);
                        Destroy(gameObject);
                        break;
                }
            }
        }
    }


    public class MeleeEnemy : MonoBehaviour
    {
        private int damage = 1;
        private int maxHealth = 10;

        EnemyTypes enemyTypes;

        private void Start()
        {
            enemyTypes = GetComponent<EnemyTypes>();
        }
        

    }



    public enum EnemyType
    {
        Melee, Ranged, Bomber
    }
}
