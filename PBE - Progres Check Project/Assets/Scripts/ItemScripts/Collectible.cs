using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public enum CollectibleType { Nothing, Coin, Weapon }; //Enum list with possible collectible Items
    public CollectibleType type; // It is declared as a SerializeField just for testing purposes 

    private void Awake()
    {
        SetInitialType();
    }

    //This Method checks the tag of the object where this script is attached, and assigns his type
    public void SetInitialType()
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

    public void SetType()
    {
        switch (type)
        {
            case CollectibleType.Coin:
                transform.gameObject.tag = "Coin";
                break;
            case CollectibleType.Weapon:
                transform.gameObject.tag = "Weapon";
                break;
        }
    }
}
