using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] int ammo;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletSpeed;
    [SerializeField] Transform spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire2") && ammo > 0)
        {
            ShootBullet();
        }
    }

    void ShootBullet()
    {
        Debug.Log("Shoot");
        GameObject bulletSpawned = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);
        bulletSpawned.SetActive(true);
        bulletSpawned.GetComponent<Rigidbody>().velocity = bulletSpawned.transform.forward * bulletSpeed;
        ammo--;
    }
}
