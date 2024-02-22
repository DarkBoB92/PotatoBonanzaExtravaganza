using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<GameObject> weaponList;
    public int weaponAmount;
      
    public void AddItem(GameObject item)
    {
        weaponList.Add(item);
    }

}
