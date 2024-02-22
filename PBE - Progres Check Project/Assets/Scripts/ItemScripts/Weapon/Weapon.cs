using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collectible 
{
    //Variables and references are declerade as SerializedField for testing purpose
    [SerializeField] float minPower, maxPower; //Range values to apply a random damage value for the weapon
    [SerializeField] int potatoPower, acutalPower; //Temporary variable to check functionality of the script
    [SerializeField] GameObject player;
    [SerializeField] PlayerInventory inventory;
    [SerializeField] Throwable throwable;

    private void Start()
    {        
        acutalPower = (int)Random.RandomRange(minPower, maxPower);
    }

    private void Update()
    {
        Debug.Log(GetComponent<Rigidbody>().velocity.magnitude);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Used player for logic connection, on collision takes the player components PlayerInventory and Throwable
        player = collision.gameObject;
        inventory = player.GetComponent<PlayerInventory>();
        throwable = player.GetComponent<Throwable>();
        if (player.tag == "Player")
        {
            Collected();
            //Adding the collected item (this case Weapon) to the inventory of the player and set the object to false,
            //this because the item still exists in the inventory
            inventory.AddItem(gameObject);
            inventory.weaponAmount++;
            gameObject.SetActive(false);
        }
        else
        { //Any other collision will delete the item and clear the Throwable bullet list (check Throwable Script for more info)
            if (collision.gameObject.CompareTag("Weapon") || collision.gameObject.CompareTag("Coin"))
            {

            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }

    //This method is to add the weapons damage
    //TODO: Need to pass the damage to the Player once we implement DamageSystem
    void Collected()
    {   
        potatoPower += acutalPower;
        type = CollectibleType.Weapon;
        SetType();
        Debug.Log($"potatoPower is {potatoPower}");
    }
}
