using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Coin : Collectible
{
    [SerializeField] int value, moneyBag;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Collected();
            Destroy(gameObject);
        }
    }

    void Collected()
    {
        moneyBag += value;
        Debug.Log($"moneyBag ammount is {moneyBag}");
    }
}
