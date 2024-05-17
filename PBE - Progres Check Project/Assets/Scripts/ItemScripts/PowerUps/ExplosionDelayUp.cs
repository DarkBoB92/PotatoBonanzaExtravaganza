using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDelayUp : Collectible
{
    NewPlayerShoot player;
    float maxDelayReduction = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<NewPlayerShoot>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ReduceDelay();
            Destroy(this.gameObject);
        }
    }
    void ReduceDelay()
    {
        if (player != null)
        {
            player.delayReduction += 0.5f;
            if (player.delayReduction > maxDelayReduction)
            {
                player.delayReduction = maxDelayReduction;
            }
        }
    }
}
