using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Throwable : MonoBehaviour
{
    //Variables and references are declerade as SerializedField for testing purpose
    public GameObject player, item, bullet; //item will be the current Item (weapon in this instance) that the player is holding 
    public List<GameObject> bulletMovement; //This list contains every throwed item before colliding, so multiple item can be thro
    [SerializeField] Transform throwStartPosition; //This object will contain the position of an EmptyGameObject to set a starting point to shoot 
    [SerializeField] PlayerInventory inventory;
    [SerializeField] int bulletSpeed;
    GameObject bulletToSpawn;



    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); //Fetch the current GameObject of the Hierarchy that has the "PLayer" tag 
        inventory = player.GetComponent<PlayerInventory>();  //Get the player inventory
    }

    void Update()
    {
        UpdateCurrentItem();

        //GetFireInput();

        //SetItemDirection();
    }

    //Assigns to item the first object of the player inventory if there is an item, otherwise it keeps it empty.
    void UpdateCurrentItem()
    {        
        if (inventory.weaponList.Count > 0)
        {
            item = inventory.weaponList[0];
        }
        else
        {
            item = null;
        }
    }

    //void GetFireInput()
    //{
    //    if (Input.GetButtonDown("Fire2"))
    //    {
    //        ThrowItem();
    //        //if (bullet != null)
    //        //{
    //        //    bulletMovement.Add(bullet);
    //        //    bullet = null;
    //        //}
    //        inventory.weaponAmount--;
    //    }
    //}

    //This method launchs the item by removing the item form the player inventory and adding the holded item to the bullet list
    //and after setting the item true it moves it to the throwStartPoint position and rotation and clears the player holded item
    //this again to avoid the presence of missing object that might get caught in the loop and sent to the list.
    public void ThrowItem()
    {
        if (item != null)
        {
            inventory.weaponList.RemoveAt(0);
            bullet = item;
            bullet.SetActive(true);
            bulletToSpawn = Instantiate(bullet, throwStartPosition.position, throwStartPosition.rotation);
            bulletToSpawn.GetComponent<Rigidbody>().velocity = bulletToSpawn.transform.up * bulletSpeed;
            //bullet.transform.position = throwStartPosition.position;
            //bullet.transform.rotation = throwStartPosition.rotation;                       
            item = null;
        }
        else
        {
            Debug.Log("OwO"); //TODO: maybe some pop up message to tell the player that it doesn't have any item
        }
    }

    //This method makes sure to remove the item from the lists and resetting the holded item and it's used in the KillEnemy Script.
    public void RemoveItemFromList(GameObject weapon)
    {
        if (inventory.weaponList.Contains(weapon))
        {
            inventory.weaponList.Remove(weapon);           
        }

        if (bullet == weapon)
        {
            bullet = null;
        }

        if (item == weapon)
        {
            item = null;
        }
    }

    //This loop is to give constant movement to the bullet only if the list has a game object if not, it deletes the element from the list.
    //This assures that there will be no missing object in the list.
    //void SetItemDirection()
    //{        
    //    for (int i = 0; i < bulletMovement.Count; i++)
    //    {
    //        if (bulletMovement[i] != null)
    //        {                
    //            bulletToSpawn.GetComponent<Rigidbody>().velocity = bulletToSpawn.transform.up * bulletSpeed;
    //        }
    //        else
    //        {
    //            bulletMovement.RemoveAt(i);
    //        }
    //    }
    //}
}
