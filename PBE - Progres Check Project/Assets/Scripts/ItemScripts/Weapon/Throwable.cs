using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Throwable : MonoBehaviour
{
    public GameObject player, item;
    public List<GameObject> bullet;
    [SerializeField] Transform throwStartPosition;
    [SerializeField] PlayerInventory inventory;
    [SerializeField] int bulletSpeed;


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
        else
        {
            item = null;
        }

        if (Input.GetButtonDown("Fire1"))
        {            
            ThrowItem(0);            
        }

        for (int i = 0; i < bullet.Count; i++)
        {
            if (bullet[i] != null)
            {
                bullet[i].transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime, Space.Self);
            }
            else
            {
                bullet.RemoveAt(i);
            }
        }
    }

    public void ThrowItem(int bulletIndex)
    {
        if (item != null)
        {
            inventory.weaponList.RemoveAt(0);
            bullet.Add(item);
            bullet[bulletIndex].SetActive(true);
            bullet[bulletIndex].transform.position = throwStartPosition.position;
            bullet[bulletIndex].transform.rotation = throwStartPosition.rotation;
            item = null;
        }
        else
        {
            Debug.Log("OwO");
        }
    }

    public void RemoveItemFromList(GameObject weapon)
    {
        if (inventory.weaponList.Contains(weapon))
        {
            inventory.weaponList.Remove(weapon);
            bullet.Remove(weapon);            
        }

        if (bullet.Contains(weapon))
        {
            bullet.Remove(weapon);
        }

        if (item == weapon)
        {
            item = null;
        }
    }
}
