using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<GameObject> weaponList;

    public void AddItem(GameObject weapon)
    {
        weaponList.Add(weapon);
    }
}
