using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Throwable : MonoBehaviour
{
    //Variables and references are declerade as SerializedField for testing purpose
    public GameObject player, item; //item will be the current Item (weapon in this instance) that the player is holding 
    public List<GameObject> bullet; //This list contains every throwed item before colliding, so multiple item can be thrown
    [SerializeField] Transform throwStartPosition; //This object will contain the position of an EmptyGameObject to set a starting point to shoot 
    [SerializeField] PlayerInventory inventory;
    [SerializeField] int bulletSpeed; 

    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); //Fetch the current GameObject of the Hierarchy that has the "PLayer" tag 
        inventory = player.GetComponent<PlayerInventory>();  //Get the player inventory
    }

    void Update()
    {
        //Assigns to item the first object of the player inventory if there is an item, otherwise it keeps it empy.
        if (inventory.weaponList.Count > 0)
        {
            item = inventory.weaponList[0];
        }
        else
        {
            item = null;
        }
                
        if (Input.GetButtonDown("Fire1"))
        {            
            ThrowItem(0);            
        }

        //This loop is to give constant movement to the bullet only if the list has a game object if not, it deletes the element from the list.
        //This assures that there will be no missing object in the list.
        for (int i = 0; i < bullet.Count; i++)
        {
            if (bullet[i] != null)
            {
                bullet[i].transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime, Space.Self);
            }
            else
            {
                bullet.RemoveAt(i);
            }
        }
    }

    //This method launchs the item by removing the item form the player inventory and adding the holded item to the bullet list
    //and after setting the item true it moves it to the throwStartPoint position and rotation and clears the player holded item
    //this again to avoid the presence of missing object that might get caught in the loop and sent to the list.
    public void ThrowItem(int bulletIndex)
    {
        if (item != null)
        {
            inventory.weaponList.RemoveAt(0);
            bullet.Add(item);
            bullet[bulletIndex].SetActive(true);
            bullet[bulletIndex].transform.position = throwStartPosition.position;
            bullet[bulletIndex].transform.rotation = throwStartPosition.rotation;
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
            bullet.Remove(weapon);            
        }

        if (bullet.Contains(weapon))
        {
            bullet.Remove(weapon);
        }

        if (item == weapon)
        {
            item = null;
        }
    }
}
