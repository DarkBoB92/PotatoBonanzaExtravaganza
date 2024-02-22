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
    Weapon weapon;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar = GetComponent<EnemyHealthBar>();
        health = GetComponent<PlayerHealth>();
        weapon = GetComponent<Weapon>();
    }

    private void Update()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            health.TakeDamage(2);
        }
        else if (other.gameObject.CompareTag("Weapon"))
        {
            health.TakeDamage(4);
        }
    }
}
