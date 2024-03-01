using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public int ammo, grenades;
    [SerializeField] GameObject bulletPrefab, grenadePrefab;
    [SerializeField] float bulletSpeed;
    [SerializeField] Transform spawnPoint;
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
        GameObject bulletSpawned = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);
        bulletSpawned.GetComponent<Rigidbody>().velocity = bulletSpawned.transform.up * bulletSpeed;
        bulletSpawned.GetComponent<Weapon>().shooted = true;
        bulletSpawned.GetComponent<Weapon>().isGranade = false;
        ammo--;
    }

    void ThrowGrenade()
    {
        //TODO: create new spawn position for grenades
        GameObject grenadeSpawned = Instantiate(grenadePrefab, spawnPoint.position, spawnPoint.rotation);
        grenadePrefab.GetComponent<Rigidbody>().AddForce(transform.forward * throwForce, ForceMode.Acceleration);
        grenadePrefab.GetComponent<Weapon>().shooted = true;
        grenadePrefab.GetComponent<Weapon>().isGranade = true;
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
}
