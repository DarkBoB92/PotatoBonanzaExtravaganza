using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowPowerUp : Collectible
{
    NewPlayerShoot player;
    float maxThrowForce = 150;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<NewPlayerShoot>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ThrowPwrUp();
            Destroy(this.gameObject);
        }
    }
    void ThrowPwrUp()
    {
        if (player != null)
        {
            player.throwForce += 10;
            if (player.throwForce >= maxThrowForce)
            {
                player.throwForce = maxThrowForce;
            }
        }
    }
}
