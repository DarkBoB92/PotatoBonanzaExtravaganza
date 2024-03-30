using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireModeTomatoUp : MonoBehaviour
{
    PlayerShoot player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerShoot>();
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
            player.tomatoFireMode++;
            if (player.tomatoFireMode >= player.grenadeSpawnPoint.Length - 1)
            {
                player.tomatoFireMode = player.grenadeSpawnPoint.Length - 1;
            }
        }
    }
}
