using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestoreHealth : Collectible
{

    PlayerHealth player;
    PlayerHealthBar playerHealthBar;
    [SerializeField] int restoreValue;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        playerHealthBar = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealthBar>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Heal();
            Destroy(this.gameObject);
        }
    }
    void Heal()
    {
        if(player != null && playerHealthBar != null)
        {
            player.currentHealth += restoreValue;
            if(player.currentHealth >= player.maxHealth)
            {
                player.currentHealth = player.maxHealth;
            }
            playerHealthBar.UpdateHealthBar(player.currentHealth, player.maxHealth);
        }
    }
}
