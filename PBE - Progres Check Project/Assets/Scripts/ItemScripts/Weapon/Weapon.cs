using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collectible 
{
    //Variables and references are declerade as SerializedField for testing purpose
    [SerializeField] float minPower, maxPower, explosionRadius; //Range values to apply a random damage value for the weapon
    [SerializeField] int potatoPower, acutalPower, damageToEnemies; //Temporary variable to check functionality of the script
    [SerializeField] GameObject player;
    [SerializeField] PlayerShoot ammo;
    [SerializeField] PlayerHealth playerHealth, enemyHealth;
    [SerializeField] LayerMask damagingObjects;   // Use this for referencing any enemies that NEED to be damaged by this enemy AS LONG as they have different tags.
    [SerializeField] float lifeTime = 3;
    public bool shooted, isGranade;

    private void Start()
    {        
        acutalPower = (int)Random.Range(minPower, maxPower);
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = FindObjectOfType<PlayerHealth>();
        if (player != null)
        {
            ammo = player.GetComponent<PlayerShoot>();
        }
    }

    private void Update()
    {        
        if (shooted && !isGranade)
        {
            LifeTime();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Used player for logic connection
        player = other.gameObject;
        if (player.tag == "Player")
        {
            Collected();
            ammo.AddAmmo();
            Destroy(this.gameObject);
        }

        if (other.gameObject.tag == "Ground")
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && !shooted)
        {
            ammo.AddGrenade();
            Destroy(this.gameObject);
        }

        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Enemy")
        {
            Explode();
        }
    }

    void Collected()
    {
        //potatoPower += acutalPower;
        //Debug.Log($"potatoPower is {potatoPower}");
    }

    void LifeTime()
    {
        lifeTime -= Time.deltaTime;
        if(lifeTime <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    void Explode()
    {
        OverlappingPlayer();
        Destroy(this.gameObject);
        Debug.Log("BOOOOOOOOOOOM!");
    }

    public void OverlappingPlayer()
    {
        Collider[] objectsInRange = Physics.OverlapSphere(transform.position, explosionRadius, damagingObjects);

        if (objectsInRange.Length > 0)
        {
            foreach (Collider obj in objectsInRange)
            {

                if (obj.CompareTag("Player") && playerHealth != null) // Null checks to make sure no NRE's happen.
                {
                    playerHealth = obj.GetComponent<PlayerHealth>();
                    playerHealth.TakeDamage(2); // Inspector dependent variable. Can be removed if you want to internally modify [Enemy damage right below]
                }
                if (obj.CompareTag("Enemy") && playerHealth != null)
                {
                    enemyHealth = obj.GetComponent<PlayerHealth>();
                    enemyHealth.TakeDamage(damageToEnemies); // Modify this value for damaging the enemies. Can do more if needed.
                }
            }
        }
    }
}
