using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFireRateUp : Collectible
{
    NewPlayerShoot player;
    float maxFireRate = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<NewPlayerShoot>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            FireRateUp();
            Destroy(this.gameObject);
        }
    }
    void FireRateUp()
    {
        if (player != null)
        {
            player.fireRate -= 0.1f;
            if (Mathf.Abs(player.fireRate) < maxFireRate) //player.fireRate >= 0 or >= maxFireRate were not working so changed in this new statement by considering the absolute value of  
            {                                             //player.fireRate after the subtraction, so by ignoring his sign we take in consideration any number that are minor than maxIncrease.  
                player.fireRate = maxFireRate;
            }            
        }
    }
}
