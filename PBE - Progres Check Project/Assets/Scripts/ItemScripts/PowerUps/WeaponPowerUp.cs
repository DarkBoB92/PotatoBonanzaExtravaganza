using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPowerUp : Collectible
{
    NewPlayerShoot player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<NewPlayerShoot>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            DamageUp();
            Destroy(this.gameObject);
        }
    }
    void DamageUp()
    {
        if(player != null)
        {
            player.damageIncrement++;
        }
    }
}
