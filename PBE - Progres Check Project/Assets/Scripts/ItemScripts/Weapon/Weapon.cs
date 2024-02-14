using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collectible
{
    [SerializeField] float minPower, maxPower; 
    [SerializeField] int potatoPower, acutalPower;
    [SerializeField] GameObject player, enemy;
    [SerializeField] PlayerInventory inventory;
    [SerializeField] Throwable throwable;

    private void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
        //enemy = GameObject.FindGameObjectWithTag("Enemy");
        acutalPower = (int)Random.RandomRange(minPower, maxPower);
    }
        
    //private void OnTriggerEnter(Collider other)
    //{
    //    player = other.gameObject;
    //    inventory = player.GetComponent<PlayerInventory>();
    //    if (other.tag == "Player")
    //    {
    //        //Collected();
    //        inventory.AddItem(gameObject);            
    //        gameObject.SetActive(false);            
    //    }        
    //}

    private void OnCollisionEnter(Collision collision)
    {
        player = collision.gameObject;
        inventory = player.GetComponent<PlayerInventory>();
        throwable = player.GetComponent<Throwable>();
        if (player.tag == "Player")
        {
            //Collected();
            inventory.AddItem(gameObject);
            gameObject.SetActive(false);
        }
        else
        {
            Destroy(this.gameObject);
            throwable.bullet.RemoveAt(0);
        }
    }

    //void Collected()
    //{        
    //    potatoPower += acutalPower;
    //    Debug.Log($"potatoPower is {potatoPower}");
    //}
}
