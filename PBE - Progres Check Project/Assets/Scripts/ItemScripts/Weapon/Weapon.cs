using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collectible 
{
    //Variables and references are declerade as SerializedField for testing purpose
    public float explosionRadius, explosionTimer;    
    [SerializeField] GameObject player;
    [SerializeField] NewPlayerShoot ammo;
    [SerializeField] PlayerHealth playerHealth, enemyHealth;
    [SerializeField] LayerMask damagingObjects;   // Use this for referencing any enemies that NEED to be damaged by this enemy AS LONG as they have different tags.
    [SerializeField] float lifeTime = 3;
    public int knifeDMG, tomatoDMG;
    public bool shooted, isGranade;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = FindObjectOfType<PlayerHealth>();
        if (player != null)
        {
            ammo = player.GetComponent<NewPlayerShoot>();
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
            if (isGranade)
            {
                ammo.AddGrenade();
            }
            else
            {
                ammo.AddAmmo();
            }
            Destroy(this.gameObject);
        }

        if (other.gameObject.tag == "Ground" && !isGranade)
        {
            Destroy(this.gameObject);
        }        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground" && isGranade || collision.gameObject.tag == "Enemy" && isGranade)
        {
            Explode();
        }
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
        StartCoroutine(Detonation());
    }

    IEnumerator Detonation()
    {
        yield return new WaitForSeconds(explosionTimer);
        OverlappingPlayer();
        Destroy(this.gameObject);
    }

    public void DamageUp(int amount)
    {
        if (ammo != null && ammo.damageIncrement >= ammo.maxIncrement)
        {
            ammo.damageIncrement = ammo.maxIncrement;
        }
        if (!isGranade)
        {
            knifeDMG += amount;
        }
        else
        {
            tomatoDMG += amount;
        }
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
                    enemyHealth.TakeDamage(tomatoDMG); // Modify this value for damaging the enemies. Can do more if needed.
                }
            }
        }
    }
}
