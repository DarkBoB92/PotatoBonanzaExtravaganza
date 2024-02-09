using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    enum CollectibleType { Nothing, Coin, Weapon};
    [SerializeField]CollectibleType type;

    private void Awake()
    {
        SetType();
    }   

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
