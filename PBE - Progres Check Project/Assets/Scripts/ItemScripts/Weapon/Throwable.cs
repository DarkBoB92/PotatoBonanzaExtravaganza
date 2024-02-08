using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour
{
    [SerializeField] GameObject player, item;
    [SerializeField] PlayerInventory inventory;



    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        inventory = player.GetComponent<PlayerInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inventory.weaponList.Count > 0)
        {
            item = inventory.weaponList[0];
        }

        if (Input.GetButton("Fire1"))
        {
            ThrowItem();
        }
    }

    public void ThrowItem()
    {        
        inventory.weaponList.RemoveAt(0);
        item.SetActive(true);
    }
}
