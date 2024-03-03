using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collectible 
{
    //Variables and references are declerade as SerializedField for testing purpose
    [SerializeField] float minPower, maxPower; //Range values to apply a random damage value for the weapon
    [SerializeField] int potatoPower, acutalPower; //Temporary variable to check functionality of the script
    [SerializeField] GameObject player;
    [SerializeField] PlayerShoot ammo;
    [SerializeField] float lifeTime = 3;
    public bool shooted, isGranade;

    private void Start()
    {        
        acutalPower = (int)Random.RandomRange(minPower, maxPower);
        player = GameObject.FindGameObjectWithTag("Player");
        ammo = player.GetComponent<PlayerShoot>();
        
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
        //Used player for logic connection, on collision takes the player components PlayerInventory and Throwable
        player = other.gameObject;
        if (player.tag == "Player")
        {
            Collected();
            ammo.AddAmmo();
            Destroy(gameObject);
        }

        if (other.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && !shooted)
        {
            ammo.AddGrenade();
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Ground")
        {
            Explode();
        }
    }

    //This method is to add the weapons damage
    //TODO: Need to pass the damage to the Player once we implement DamageSystem
    void Collected()
    {
        //potatoPower += acutalPower;
        //Debug.Log($"potatoPower is {potatoPower}");
    }

    void LifeTime()
    {
        lifeTime -= Time.deltaTime;
        print(lifeTime);
        if(lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    void Explode()
    {
        Destroy(gameObject);
        Debug.Log("BOOOOOOOOOOOM!");
    }
}
