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
        FindObjectOfType<GameplayAudio>().AudioTrigger(GameplayAudio.SoundFXCat.Spawn, transform.position, 1f);
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
        else if (weapon != null)
        {  
           if(other.gameObject.CompareTag("Weapon") && weapon.shooted && !weapon.isGranade)
           {
               health.TakeDamage(weapon.knifeDMG);
               Destroy(other.gameObject);              
           }
        }
    }
}