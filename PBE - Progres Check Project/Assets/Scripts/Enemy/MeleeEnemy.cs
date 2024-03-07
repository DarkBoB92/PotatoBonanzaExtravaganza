using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int damage;
    int currentHealth;

    EnemyHealthBar healthBar;
    PlayerHealth health;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar = GetComponent<EnemyHealthBar>();
        health = GetComponent<PlayerHealth>();
    }

    private void Update()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {        
        Weapon weapon = other.GetComponent<Weapon>();
        if (other.gameObject.CompareTag("Player"))
        {
            health.TakeDamage(2);
        }
        else if (other.gameObject.CompareTag("Weapon") && weapon.shooted)
        {
            if (weapon.isGranade)
            {
                weapon.OverlappingPlayer();
                Destroy(other.gameObject);
            }
            else
            {
                health.TakeDamage(4);
                Destroy(other.gameObject);
            }
        }
    }
}