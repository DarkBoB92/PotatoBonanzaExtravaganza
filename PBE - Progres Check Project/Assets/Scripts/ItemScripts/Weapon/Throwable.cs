using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour
{
    [SerializeField] GameObject player, enemy, item;
    [SerializeField] PlayerInventory inventory;
    [SerializeField] Rigidbody itemRb;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        inventory = player.GetComponent<PlayerInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inventory.weaponList.Count > 0)
        {
            item = inventory.weaponList[0];
        }

        if (Input.GetButtonDown("Fire1"))
        {
            ThrowItem();
        }
    }

    public void ThrowItem()
    {        
        inventory.weaponList.RemoveAt(0);
        item.SetActive(true);
        item.transform.position = new Vector3(player.transform.position.x + 2f, player.transform.position.y + 2f, player.transform.position.z + 2);
        itemRb.AddForce(enemy.transform.position);
    }
}
