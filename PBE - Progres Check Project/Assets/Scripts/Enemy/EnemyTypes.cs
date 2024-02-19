using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static EnemyTypes;

public class EnemyTypes : MonoBehaviour
{
    [SerializeField] EnemyType enemyType;
    private int damage;
    private int maxHealth;

    Health health;
    BomberEnemy bomber;

    


    void Start()
    {
        bomber = GetComponent<BomberEnemy>();
        health = GetComponent<Health>();
    }

    private void Update()
    {
        
    }

    void TypeCheck()
    {
        switch (enemyType)
        {
            case EnemyType.Bomber:
                break;

            case EnemyType.Melee:
                break;

            case EnemyType.Ranged:
                
                break;
        }
    }

    public enum EnemyType
    {
        Melee, Ranged, Bomber
    }
}