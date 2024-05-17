using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Collectible;

public class RangedEnemy : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    public int damage;
    [SerializeField] int bulletSpeed;
    [SerializeField] float range, fireRate, resetFireRate;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform spawnPoint;
    int currentHealth;

    EnemyHealthBar healthBar;
    PlayerHealth health;
    EnemyKeepDistance engage;

    private void Start()
    {
        FindObjectOfType<GameplayAudio>().AudioTrigger(GameplayAudio.SoundFXCat.Spawn, transform.position, 1f);
        currentHealth = maxHealth;
        healthBar = GetComponent<EnemyHealthBar>();
        health = GetComponent<PlayerHealth>();
        engage = GetComponent<EnemyKeepDistance>();
        range = engage.radius + 5;
        fireRate = resetFireRate;
    }

    private void FixedUpdate()
    {
        if(fireRate <= 0)
        {
            Shoot();
        }
        else
        {
            fireRate -= Time.fixedDeltaTime;
        }
    }

    void Shoot()
    {
        if(engage.distance <= range)
        {
            GameObject bulletSpawned = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);            
            bulletSpawned.GetComponent<Rigidbody>().velocity = bulletSpawned.transform.up * bulletSpeed;
            bulletSpawned.GetComponent<EnemyWeapon>().damage = damage;
            if (bulletSpawned != null)
            {
                fireRate = resetFireRate;
            }
            
        }
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
            if (other.gameObject.CompareTag("Weapon") && weapon.shooted && !weapon.isGranade)
            {
                health.TakeDamage(weapon.knifeDMG);
                Destroy(other.gameObject);                
            }
        }
    }
}
