using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static Collectible;

public class PlayerShoot : MonoBehaviour
{
    public int ammo, grenades, damageIncrement, maxIncrement, knifeFireMode, tomatoFireMode;
    [SerializeField] GameObject bulletPrefab, grenadePrefab, knifeAmmo, timerAmmo;
    [SerializeField] float bulletSpeed;
    public Transform[] bulletSpawnPoint, grenadeSpawnPoint;
    public float fireRate, throwForce, delayReduction;
    [SerializeField] TextMeshProUGUI knifeAmmoText, timerAmmoText;  
    GameUIManager gameUIManager;

    // Start is called before the first frame update
    void Start()
    {
        gameUIManager = GameObject.FindWithTag("UIManager").GetComponent<GameUIManager>();
        knifeAmmoText.text = ammo.ToString();
        timerAmmoText.text = grenades.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameUIManager.currentState == GameUIManager.GameState.Playing)
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
        knifeAmmoText.text = ammo.ToString();
        timerAmmoText.text = grenades.ToString();
    }

    void ShootBullet()
    {       
        SelectKnifeFireMode();
    }

    void ThrowGrenade()
    {        
        SelectTomatoFireMode();
    }

    IEnumerator ShootBulletHold()
    {
        while (Input.GetButton("Fire1") && ammo > 0)
        {
            FindObjectOfType<GameplayAudio>().AudioTrigger(GameplayAudio.SoundFXCat.Shoot, transform.position, 0.5f);
            yield return new WaitForSeconds(fireRate);
            ShootBullet();
        }
    }

    void SelectKnifeFireMode()
    {
        GameObject bulletSpawned;
        switch (knifeFireMode)
        {
            case 0:
                bulletSpawned = Instantiate(bulletPrefab, bulletSpawnPoint[0].position, bulletSpawnPoint[0].rotation);
                bulletSpawned.GetComponent<Rigidbody>().velocity = bulletSpawned.transform.up * bulletSpeed;
                bulletSpawned.GetComponent<Weapon>().shooted = true;
                bulletSpawned.GetComponent<Weapon>().isGranade = false;
                bulletSpawned.GetComponent<Weapon>().DamageUp(damageIncrement);
                bulletSpawned.GetComponent<Weapon>().type = CollectibleType.Weapon;
                bulletSpawned.GetComponent<Weapon>().SetType();
                ammo--;
                break;
            case 1:
                bulletSpawned = Instantiate(bulletPrefab, bulletSpawnPoint[1].position, bulletSpawnPoint[1].rotation);
                bulletSpawned.GetComponent<Rigidbody>().velocity = bulletSpawned.transform.up * bulletSpeed;
                bulletSpawned.GetComponent<Weapon>().shooted = true;
                bulletSpawned.GetComponent<Weapon>().isGranade = false;
                bulletSpawned.GetComponent<Weapon>().DamageUp(damageIncrement);
                bulletSpawned.GetComponent<Weapon>().type = CollectibleType.Weapon;
                bulletSpawned.GetComponent<Weapon>().SetType();
                ammo--;
                bulletSpawned = Instantiate(bulletPrefab, bulletSpawnPoint[2].position, bulletSpawnPoint[2].rotation);
                bulletSpawned.GetComponent<Rigidbody>().velocity = bulletSpawned.transform.up * bulletSpeed;
                bulletSpawned.GetComponent<Weapon>().shooted = true;
                bulletSpawned.GetComponent<Weapon>().isGranade = false;
                bulletSpawned.GetComponent<Weapon>().DamageUp(damageIncrement);
                bulletSpawned.GetComponent<Weapon>().type = CollectibleType.Weapon;
                bulletSpawned.GetComponent<Weapon>().SetType();
                ammo--;
                break;
            case 2:
                bulletSpawned = Instantiate(bulletPrefab, bulletSpawnPoint[0].position, bulletSpawnPoint[0].rotation);
                bulletSpawned.GetComponent<Rigidbody>().velocity = bulletSpawned.transform.up * bulletSpeed;
                bulletSpawned.GetComponent<Weapon>().shooted = true;
                bulletSpawned.GetComponent<Weapon>().isGranade = false;
                bulletSpawned.GetComponent<Weapon>().DamageUp(damageIncrement);
                bulletSpawned.GetComponent<Weapon>().type = CollectibleType.Weapon;
                bulletSpawned.GetComponent<Weapon>().SetType();
                ammo--;
                bulletSpawned = Instantiate(bulletPrefab, bulletSpawnPoint[3].position, bulletSpawnPoint[3].rotation);
                bulletSpawned.GetComponent<Rigidbody>().velocity = bulletSpawned.transform.up * bulletSpeed;
                bulletSpawned.GetComponent<Weapon>().shooted = true;
                bulletSpawned.GetComponent<Weapon>().isGranade = false;
                bulletSpawned.GetComponent<Weapon>().DamageUp(damageIncrement);
                bulletSpawned.GetComponent<Weapon>().type = CollectibleType.Weapon;
                bulletSpawned.GetComponent<Weapon>().SetType();
                ammo--;
                bulletSpawned = Instantiate(bulletPrefab, bulletSpawnPoint[4].position, bulletSpawnPoint[4].rotation);
                bulletSpawned.GetComponent<Rigidbody>().velocity = bulletSpawned.transform.up * bulletSpeed;
                bulletSpawned.GetComponent<Weapon>().shooted = true;
                bulletSpawned.GetComponent<Weapon>().isGranade = false;
                bulletSpawned.GetComponent<Weapon>().DamageUp(damageIncrement);
                bulletSpawned.GetComponent<Weapon>().type = CollectibleType.Weapon;
                bulletSpawned.GetComponent<Weapon>().SetType();
                ammo--;
                break;
            case 3:
                bulletSpawned = Instantiate(bulletPrefab, bulletSpawnPoint[1].position, bulletSpawnPoint[1].rotation);
                bulletSpawned.GetComponent<Rigidbody>().velocity = bulletSpawned.transform.up * bulletSpeed;
                bulletSpawned.GetComponent<Weapon>().shooted = true;
                bulletSpawned.GetComponent<Weapon>().isGranade = false;
                bulletSpawned.GetComponent<Weapon>().DamageUp(damageIncrement);
                bulletSpawned.GetComponent<Weapon>().type = CollectibleType.Weapon;
                bulletSpawned.GetComponent<Weapon>().SetType();
                ammo--;
                bulletSpawned = Instantiate(bulletPrefab, bulletSpawnPoint[2].position, bulletSpawnPoint[2].rotation);
                bulletSpawned.GetComponent<Rigidbody>().velocity = bulletSpawned.transform.up * bulletSpeed;
                bulletSpawned.GetComponent<Weapon>().shooted = true;
                bulletSpawned.GetComponent<Weapon>().isGranade = false;
                bulletSpawned.GetComponent<Weapon>().DamageUp(damageIncrement);
                bulletSpawned.GetComponent<Weapon>().type = CollectibleType.Weapon;
                bulletSpawned.GetComponent<Weapon>().SetType();
                ammo--;
                bulletSpawned = Instantiate(bulletPrefab, bulletSpawnPoint[3].position, bulletSpawnPoint[3].rotation);
                bulletSpawned.GetComponent<Rigidbody>().velocity = bulletSpawned.transform.up * bulletSpeed;
                bulletSpawned.GetComponent<Weapon>().shooted = true;
                bulletSpawned.GetComponent<Weapon>().isGranade = false;
                bulletSpawned.GetComponent<Weapon>().DamageUp(damageIncrement);
                bulletSpawned.GetComponent<Weapon>().type = CollectibleType.Weapon;
                bulletSpawned.GetComponent<Weapon>().SetType();
                ammo--;
                bulletSpawned = Instantiate(bulletPrefab, bulletSpawnPoint[4].position, bulletSpawnPoint[4].rotation);
                bulletSpawned.GetComponent<Rigidbody>().velocity = bulletSpawned.transform.up * bulletSpeed;
                bulletSpawned.GetComponent<Weapon>().shooted = true;
                bulletSpawned.GetComponent<Weapon>().isGranade = false;
                bulletSpawned.GetComponent<Weapon>().DamageUp(damageIncrement);
                bulletSpawned.GetComponent<Weapon>().type = CollectibleType.Weapon;
                bulletSpawned.GetComponent<Weapon>().SetType();
                ammo--;
                break;
            case 4:
                bulletSpawned = Instantiate(bulletPrefab, bulletSpawnPoint[0].position, bulletSpawnPoint[0].rotation);
                bulletSpawned.GetComponent<Rigidbody>().velocity = bulletSpawned.transform.up * bulletSpeed;
                bulletSpawned.GetComponent<Weapon>().shooted = true;
                bulletSpawned.GetComponent<Weapon>().isGranade = false;
                bulletSpawned.GetComponent<Weapon>().DamageUp(damageIncrement);
                bulletSpawned.GetComponent<Weapon>().type = CollectibleType.Weapon;
                bulletSpawned.GetComponent<Weapon>().SetType();
                ammo--;
                bulletSpawned = Instantiate(bulletPrefab, bulletSpawnPoint[1].position, bulletSpawnPoint[1].rotation);
                bulletSpawned.GetComponent<Rigidbody>().velocity = bulletSpawned.transform.up * bulletSpeed;
                bulletSpawned.GetComponent<Weapon>().shooted = true;
                bulletSpawned.GetComponent<Weapon>().isGranade = false;
                bulletSpawned.GetComponent<Weapon>().DamageUp(damageIncrement);
                bulletSpawned.GetComponent<Weapon>().type = CollectibleType.Weapon;
                bulletSpawned.GetComponent<Weapon>().SetType();
                ammo--;
                bulletSpawned = Instantiate(bulletPrefab, bulletSpawnPoint[2].position, bulletSpawnPoint[2].rotation);
                bulletSpawned.GetComponent<Rigidbody>().velocity = bulletSpawned.transform.up * bulletSpeed;
                bulletSpawned.GetComponent<Weapon>().shooted = true;
                bulletSpawned.GetComponent<Weapon>().isGranade = false;
                bulletSpawned.GetComponent<Weapon>().DamageUp(damageIncrement);
                bulletSpawned.GetComponent<Weapon>().type = CollectibleType.Weapon;
                bulletSpawned.GetComponent<Weapon>().SetType();
                ammo--;
                bulletSpawned = Instantiate(bulletPrefab, bulletSpawnPoint[3].position, bulletSpawnPoint[3].rotation);
                bulletSpawned.GetComponent<Rigidbody>().velocity = bulletSpawned.transform.up * bulletSpeed;
                bulletSpawned.GetComponent<Weapon>().shooted = true;
                bulletSpawned.GetComponent<Weapon>().isGranade = false;
                bulletSpawned.GetComponent<Weapon>().DamageUp(damageIncrement);
                bulletSpawned.GetComponent<Weapon>().type = CollectibleType.Weapon;
                bulletSpawned.GetComponent<Weapon>().SetType();
                ammo--;
                bulletSpawned = Instantiate(bulletPrefab, bulletSpawnPoint[4].position, bulletSpawnPoint[4].rotation);
                bulletSpawned.GetComponent<Rigidbody>().velocity = bulletSpawned.transform.up * bulletSpeed;
                bulletSpawned.GetComponent<Weapon>().shooted = true;
                bulletSpawned.GetComponent<Weapon>().isGranade = false;
                bulletSpawned.GetComponent<Weapon>().DamageUp(damageIncrement);
                bulletSpawned.GetComponent<Weapon>().type = CollectibleType.Weapon;
                bulletSpawned.GetComponent<Weapon>().SetType();
                ammo--;
                break;
        }
    }

    void SelectTomatoFireMode()
    {
        GameObject grenadeSpawned;
        switch (tomatoFireMode)
        {
            case 0:
                grenadeSpawned = Instantiate(grenadePrefab, grenadeSpawnPoint[0].position, grenadeSpawnPoint[0].rotation);
                grenadeSpawned.GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePositionY;
                grenadeSpawned.GetComponent<Rigidbody>().AddForce(transform.forward * throwForce, ForceMode.Impulse);
                grenadeSpawned.GetComponent<Weapon>().isGranade = true;
                grenadeSpawned.GetComponent<Weapon>().explosionTimer -= delayReduction;
                grenadeSpawned.GetComponent<Weapon>().shooted = true;
                grenadeSpawned.GetComponent<Weapon>().DamageUp(damageIncrement);
                grenades--;
                break;
            case 1:
                grenadeSpawned = Instantiate(grenadePrefab, grenadeSpawnPoint[1].position, grenadeSpawnPoint[1].rotation);
                grenadeSpawned.GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePositionY;
                grenadeSpawned.GetComponent<Rigidbody>().AddForce(transform.forward * throwForce, ForceMode.Impulse);
                grenadeSpawned.GetComponent<Weapon>().isGranade = true;
                grenadeSpawned.GetComponent<Weapon>().explosionTimer -= delayReduction;
                grenadeSpawned.GetComponent<Weapon>().shooted = true;
                grenadeSpawned.GetComponent<Weapon>().DamageUp(damageIncrement);
                grenades--; 
                grenadeSpawned = Instantiate(grenadePrefab, grenadeSpawnPoint[2].position, grenadeSpawnPoint[2].rotation);
                grenadeSpawned.GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePositionY;
                grenadeSpawned.GetComponent<Rigidbody>().AddForce(transform.forward * throwForce, ForceMode.Impulse);
                grenadeSpawned.GetComponent<Weapon>().isGranade = true;
                grenadeSpawned.GetComponent<Weapon>().explosionTimer -= delayReduction;
                grenadeSpawned.GetComponent<Weapon>().shooted = true;
                grenadeSpawned.GetComponent<Weapon>().DamageUp(damageIncrement);
                grenades--;
                break;
            case 2:
                grenadeSpawned = Instantiate(grenadePrefab, grenadeSpawnPoint[0].position, grenadeSpawnPoint[0].rotation);
                grenadeSpawned.GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePositionY;
                grenadeSpawned.GetComponent<Rigidbody>().AddForce(transform.forward * throwForce, ForceMode.Impulse);
                grenadeSpawned.GetComponent<Weapon>().isGranade = true;
                grenadeSpawned.GetComponent<Weapon>().explosionTimer -= delayReduction;
                grenadeSpawned.GetComponent<Weapon>().shooted = true;
                grenadeSpawned.GetComponent<Weapon>().DamageUp(damageIncrement);
                grenades--;
                grenadeSpawned = Instantiate(grenadePrefab, grenadeSpawnPoint[3].position, grenadeSpawnPoint[3].rotation);
                grenadeSpawned.GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePositionY;
                grenadeSpawned.GetComponent<Rigidbody>().AddForce(transform.forward * throwForce, ForceMode.Impulse);
                grenadeSpawned.GetComponent<Weapon>().isGranade = true;
                grenadeSpawned.GetComponent<Weapon>().explosionTimer -= delayReduction;
                grenadeSpawned.GetComponent<Weapon>().shooted = true;
                grenadeSpawned.GetComponent<Weapon>().DamageUp(damageIncrement);
                grenades--; 
                grenadeSpawned = Instantiate(grenadePrefab, grenadeSpawnPoint[4].position, grenadeSpawnPoint[4].rotation);
                grenadeSpawned.GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePositionY;
                grenadeSpawned.GetComponent<Rigidbody>().AddForce(transform.forward * throwForce, ForceMode.Impulse);
                grenadeSpawned.GetComponent<Weapon>().isGranade = true;
                grenadeSpawned.GetComponent<Weapon>().explosionTimer -= delayReduction;
                grenadeSpawned.GetComponent<Weapon>().shooted = true;
                grenadeSpawned.GetComponent<Weapon>().DamageUp(damageIncrement);
                grenades--;
                break;
            case 3:
                grenadeSpawned = Instantiate(grenadePrefab, grenadeSpawnPoint[1].position, grenadeSpawnPoint[1].rotation);
                grenadeSpawned.GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePositionY;
                grenadeSpawned.GetComponent<Rigidbody>().AddForce(transform.forward * throwForce, ForceMode.Impulse);
                grenadeSpawned.GetComponent<Weapon>().isGranade = true;
                grenadeSpawned.GetComponent<Weapon>().explosionTimer -= delayReduction;
                grenadeSpawned.GetComponent<Weapon>().shooted = true;
                grenadeSpawned.GetComponent<Weapon>().DamageUp(damageIncrement);
                grenades--; 
                grenadeSpawned = Instantiate(grenadePrefab, grenadeSpawnPoint[2].position, grenadeSpawnPoint[2].rotation);
                grenadeSpawned.GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePositionY;
                grenadeSpawned.GetComponent<Rigidbody>().AddForce(transform.forward * throwForce, ForceMode.Impulse);
                grenadeSpawned.GetComponent<Weapon>().isGranade = true;
                grenadeSpawned.GetComponent<Weapon>().explosionTimer -= delayReduction;
                grenadeSpawned.GetComponent<Weapon>().shooted = true;
                grenadeSpawned.GetComponent<Weapon>().DamageUp(damageIncrement);
                grenades--;
                grenadeSpawned = Instantiate(grenadePrefab, grenadeSpawnPoint[3].position, grenadeSpawnPoint[3].rotation);
                grenadeSpawned.GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePositionY;
                grenadeSpawned.GetComponent<Rigidbody>().AddForce(transform.forward * throwForce, ForceMode.Impulse);
                grenadeSpawned.GetComponent<Weapon>().isGranade = true;
                grenadeSpawned.GetComponent<Weapon>().explosionTimer -= delayReduction;
                grenadeSpawned.GetComponent<Weapon>().shooted = true;
                grenadeSpawned.GetComponent<Weapon>().DamageUp(damageIncrement);
                grenades--; 
                grenadeSpawned = Instantiate(grenadePrefab, grenadeSpawnPoint[4].position, grenadeSpawnPoint[4].rotation);
                grenadeSpawned.GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePositionY;
                grenadeSpawned.GetComponent<Rigidbody>().AddForce(transform.forward * throwForce, ForceMode.Impulse);
                grenadeSpawned.GetComponent<Weapon>().isGranade = true;
                grenadeSpawned.GetComponent<Weapon>().explosionTimer -= delayReduction;
                grenadeSpawned.GetComponent<Weapon>().shooted = true;
                grenadeSpawned.GetComponent<Weapon>().DamageUp(damageIncrement);
                grenades--;
                break;
            case 4:
                grenadeSpawned = Instantiate(grenadePrefab, grenadeSpawnPoint[0].position, grenadeSpawnPoint[0].rotation);
                grenadeSpawned.GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePositionY;
                grenadeSpawned.GetComponent<Rigidbody>().AddForce(transform.forward * throwForce, ForceMode.Impulse);
                grenadeSpawned.GetComponent<Weapon>().isGranade = true;
                grenadeSpawned.GetComponent<Weapon>().explosionTimer -= delayReduction;
                grenadeSpawned.GetComponent<Weapon>().shooted = true;
                grenadeSpawned.GetComponent<Weapon>().DamageUp(damageIncrement);
                grenades--; 
                grenadeSpawned = Instantiate(grenadePrefab, grenadeSpawnPoint[1].position, grenadeSpawnPoint[1].rotation);
                grenadeSpawned.GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePositionY;
                grenadeSpawned.GetComponent<Rigidbody>().AddForce(transform.forward * throwForce, ForceMode.Impulse);
                grenadeSpawned.GetComponent<Weapon>().isGranade = true;
                grenadeSpawned.GetComponent<Weapon>().explosionTimer -= delayReduction;
                grenadeSpawned.GetComponent<Weapon>().shooted = true;
                grenadeSpawned.GetComponent<Weapon>().DamageUp(damageIncrement);
                grenades--;
                grenadeSpawned = Instantiate(grenadePrefab, grenadeSpawnPoint[2].position, grenadeSpawnPoint[2].rotation);
                grenadeSpawned.GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePositionY;
                grenadeSpawned.GetComponent<Rigidbody>().AddForce(transform.forward * throwForce, ForceMode.Impulse);
                grenadeSpawned.GetComponent<Weapon>().isGranade = true;
                grenadeSpawned.GetComponent<Weapon>().explosionTimer -= delayReduction;
                grenadeSpawned.GetComponent<Weapon>().shooted = true;
                grenadeSpawned.GetComponent<Weapon>().DamageUp(damageIncrement);
                grenades--; 
                grenadeSpawned = Instantiate(grenadePrefab, grenadeSpawnPoint[3].position, grenadeSpawnPoint[3].rotation);
                grenadeSpawned.GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePositionY;
                grenadeSpawned.GetComponent<Rigidbody>().AddForce(transform.forward * throwForce, ForceMode.Impulse);
                grenadeSpawned.GetComponent<Weapon>().isGranade = true;
                grenadeSpawned.GetComponent<Weapon>().explosionTimer -= delayReduction;
                grenadeSpawned.GetComponent<Weapon>().shooted = true;
                grenadeSpawned.GetComponent<Weapon>().DamageUp(damageIncrement);
                grenades--;
                grenadeSpawned = Instantiate(grenadePrefab, grenadeSpawnPoint[4].position, grenadeSpawnPoint[4].rotation);
                grenadeSpawned.GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePositionY;
                grenadeSpawned.GetComponent<Rigidbody>().AddForce(transform.forward * throwForce, ForceMode.Impulse);
                grenadeSpawned.GetComponent<Weapon>().isGranade = true;
                grenadeSpawned.GetComponent<Weapon>().explosionTimer -= delayReduction;
                grenadeSpawned.GetComponent<Weapon>().shooted = true;
                grenadeSpawned.GetComponent<Weapon>().DamageUp(damageIncrement);
                grenades--;
                break;
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
