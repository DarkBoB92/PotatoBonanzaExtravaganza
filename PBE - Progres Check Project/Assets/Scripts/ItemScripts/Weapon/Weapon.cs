using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collectible
{
    [SerializeField] float minPower, maxPower; 
    [SerializeField] int potatoPower, acutalPower;
    [SerializeField] GameObject player;
    [SerializeField] PlayerInventory inventory;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        acutalPower = (int)Random.RandomRange(minPower, maxPower);
    }

    private void Update()
    {
        if (!gameObject.active)
        {
            transform.position = player.transform.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        player = other.gameObject;
        inventory = player.GetComponent<PlayerInventory>();
        if (other.tag == "Player")
        {
            Collected();
            inventory.AddItem(gameObject);            
            gameObject.SetActive(false);
        }
    }

    void Collected()
    {        
        potatoPower += acutalPower;
        Debug.Log($"potatoPower is {potatoPower}");
    }
}
