using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collectible
{
    [SerializeField] float minPower, maxPower; 
    [SerializeField] int potatoPower;

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
        potatoPower += (int)Random.RandomRange(minPower, maxPower);
        Debug.Log($"potatoPower is {potatoPower}");
    }
}
