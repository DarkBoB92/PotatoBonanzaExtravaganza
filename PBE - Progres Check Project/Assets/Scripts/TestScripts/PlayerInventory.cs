using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<GameObject> weaponList;
    public int weaponAmount;
    [SerializeField] int ammo;
    //[SerializeField] private GameObject inventoryWeapon;
    //[SerializeField] private Transform throwStartPoint;


    private void Awake()
    {
        //for(int i = 0; i < ammo; i++)
        //{
        //    inventoryWeapon.GetComponent<Weapon>().type = Collectible.CollectibleType.Weapon;
        //    inventoryWeapon.GetComponent<Weapon>().SetType();
        //    weaponList.Add(inventoryWeapon); // Not working currently, ASK ANDREI.
        //}
    }
    public void AddItem(GameObject weapon)
    {
        weaponList.Add(weapon);
    }
}
