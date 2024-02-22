using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] int ammo;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletSpeed;
    [SerializeField] Transform spawnPoint;
    [SerializeField] float fireRate;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2") && ammo > 0)
        {
            ShootBullet();

            StartCoroutine(ShootBulletHold());
        }
    }

    void ShootBullet()
    {
        Debug.Log("Shoot");
        GameObject bulletSpawned = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);
        bulletSpawned.SetActive(true);
        bulletSpawned.GetComponent<Rigidbody>().velocity = bulletSpawned.transform.up * bulletSpeed;
        ammo--;
    }

    IEnumerator ShootBulletHold()
    {
        while (Input.GetButton("Fire2") && ammo > 0)
        {
            yield return new WaitForSeconds(fireRate);
            ShootBullet();
            Debug.Log("Shooting");
        }
    }
}
