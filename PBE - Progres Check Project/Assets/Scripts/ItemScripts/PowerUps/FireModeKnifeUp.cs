using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireModeKnifeUp : MonoBehaviour
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
            FireModeUp();
            Destroy(this.gameObject);
        }
    }
    void FireModeUp()
    {
        if (player != null)
        {
            player.knifeFireMode++;
            if(player.knifeFireMode >= player.bulletSpawnPoint.Length - 1)
            {
                player.knifeFireMode = player.bulletSpawnPoint.Length - 1;
            }            
        }
    }
}
