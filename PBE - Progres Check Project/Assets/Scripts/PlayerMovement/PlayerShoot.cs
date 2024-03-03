using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public int ammo, grenades;
    [SerializeField] GameObject bulletPrefab, grenadePrefab;
    [SerializeField] float bulletSpeed;
    [SerializeField] Transform bulletSpawnPoint, grenadeSpawnPoint;
    [SerializeField] float fireRate, throwForce;
    bool isGrenade;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && ammo > 0)
        {
            ShootBullet();

            StartCoroutine(ShootBulletHold());
        }

        if (Input.GetButtonDown("Fire2") && grenades > 0)
        {
            ThrowGrenade();
        }
    }

    void ShootBullet()
    {
        GameObject bulletSpawned = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bulletSpawned.GetComponent<Rigidbody>().velocity = bulletSpawned.transform.up * bulletSpeed;
        bulletSpawned.GetComponent<Weapon>().shooted = true;
        bulletSpawned.GetComponent<Weapon>().isGranade = false;
        ammo--;
    }

    void ThrowGrenade()
    {        
        GameObject grenadeSpawned = Instantiate(grenadePrefab, grenadeSpawnPoint.position, grenadeSpawnPoint.rotation);
        grenadeSpawned.GetComponent <Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePositionY;
        grenadeSpawned.GetComponent<Rigidbody>().AddForce(transform.forward * throwForce, ForceMode.Impulse);
        grenadeSpawned.GetComponent<Weapon>().shooted = true;
        grenadeSpawned.GetComponent<Weapon>().isGranade = true;
        grenades--;
    }

    IEnumerator ShootBulletHold()
    {
        while (Input.GetButton("Fire1") && ammo > 0)
        {
            yield return new WaitForSeconds(fireRate);
            ShootBullet();
        }
    }

    public void AddAmmo()
    {        
        ammo++;
    }

    public void AddGrenade() 
    {
        grenades++;
    }
}
