using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour 
{
    //Variables and references are declerade as SerializedField for testing purpose
    [SerializeField] public int damage; //Range values to apply a random damage value for the weapon
    [SerializeField] GameObject player;
    [SerializeField] PlayerHealth playerHealth;
    [SerializeField] float lifeTime = 3;
    public bool shooted;

    private void Start()
    {    
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerHealth = player.GetComponent<PlayerHealth>();
        }
    }

    private void Update()
    {
        if (shooted)
        {
            LifeTime();
        }
    }

    private void OnTriggerEnter(Collider other)
    {        
        player = other.gameObject;
        if (player.tag == "Player")
        {
            playerHealth.TakeDamage(damage);
            Destroy(this.gameObject);
        }
    }

    void LifeTime()
    {
        lifeTime -= Time.deltaTime;
        print(lifeTime);
        if(lifeTime <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
