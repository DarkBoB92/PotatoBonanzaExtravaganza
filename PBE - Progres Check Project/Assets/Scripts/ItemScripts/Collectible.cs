using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    enum CollectibleType { Nothing, Coin, Weapon}; //Enum list with possible collectible Items
    [SerializeField]CollectibleType type; // It is declared as a SerializeField just for testing purposes 

    private void Awake()
    {
        SetType();
    }   

    //This Method checks the tag of the object where this script is attached, and assigns his type
    void SetType()
    {
        switch (gameObject.tag)
        {
            case "Coin":
                type = CollectibleType.Coin;
                break;
            case "Weapon":
                type = CollectibleType.Weapon;
                break;
        }
    }
}
