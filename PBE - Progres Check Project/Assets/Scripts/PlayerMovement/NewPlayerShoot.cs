using System;
using Random = UnityEngine.Random;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static Collectible;
using UnityEngine.InputSystem;

public class NewPlayerShoot : MonoBehaviour
{
    public int ammo, grenades, damageIncrement, maxIncrement, knifeFireMode, tomatoFireMode;
    [SerializeField] GameObject bulletPrefab, grenadePrefab, knifeAmmo, timerAmmo;
    [SerializeField] float bulletSpeed;
    public Transform[] bulletSpawnPoint, grenadeSpawnPoint;
    public float fireRate, throwForce, delayReduction;
    [SerializeField] TextMeshProUGUI knifeAmmoText, timerAmmoText;
    GameUIManager gameUIManager;
    bool isShooting;
    NewPlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<NewPlayerController>();
        gameUIManager = GameObject.FindWithTag("UIManager").GetComponent<GameUIManager>();
        knifeAmmoText.text = ammo.ToString();
        timerAmmoText.text = grenades.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        knifeAmmoText.text = ammo.ToString();
        timerAmmoText.text = grenades.ToString();
    }

    void OnFire1RH(InputValue input)
    {
        if (player != null && player.rightHand && gameUIManager.currentState == GameUIManager.GameState.Playing && input.isPressed && ammo > 0 && !isShooting)
        {
            isShooting = true;

            ShootBullet();

            StartCoroutine(ShootBulletHold());
        }
        else
        {
            isShooting = false;
        }
    }

    void OnFire1LH(InputValue input)
    {
        if (player != null && !player.rightHand && gameUIManager.currentState == GameUIManager.GameState.Playing && input.isPressed && ammo > 0 && !isShooting)
        {
            isShooting = true;

            ShootBullet();
            
            StartCoroutine(ShootBulletHold());
        }
        else
        {
            isShooting = false;
        }
    }

    void OnFire1Gamepad(InputValue input)
    {
        if (player != null && player.gamepad && gameUIManager.currentState == GameUIManager.GameState.Playing && input.isPressed && ammo > 0 && !isShooting)
        {
            isShooting = true;

            ShootBullet();

            StartCoroutine(ShootBulletHold());
        }
        else
        {
            isShooting = false;
        }
    }

    void OnFire2RH(InputValue input)
    {
        if (player != null && player.rightHand && gameUIManager.currentState == GameUIManager.GameState.Playing && input.isPressed && grenades > 0)
        {
            ThrowGrenade();
        }
    }

    void OnFire2LH(InputValue input)
    {
        if (player != null && !player.rightHand && gameUIManager.currentState == GameUIManager.GameState.Playing && input.isPressed && grenades > 0)
        {
            ThrowGrenade();
        }
    }

    void OnFire2Gamepad(InputValue input)
    {
        if (player != null && player.gamepad && gameUIManager.currentState == GameUIManager.GameState.Playing && input.isPressed && grenades > 0)
        {
            ThrowGrenade();
        }
    }

    void ShootBullet()
    {
        SelectKnifeFireMode();
        ammo--;
    }

    void ThrowGrenade()
    {
        SelectTomatoFireMode();
        grenades--;
    }

    IEnumerator ShootBulletHold()
    {
        while (ammo > 0 && isShooting)
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
                break;
            case 1:
                bulletSpawned = Instantiate(bulletPrefab, bulletSpawnPoint[1].position, bulletSpawnPoint[1].rotation);
                bulletSpawned.GetComponent<Rigidbody>().velocity = bulletSpawned.transform.up * bulletSpeed;
                bulletSpawned.GetComponent<Weapon>().shooted = true;
                bulletSpawned.GetComponent<Weapon>().isGranade = false;
                bulletSpawned.GetComponent<Weapon>().DamageUp(damageIncrement);
                bulletSpawned.GetComponent<Weapon>().type = CollectibleType.Weapon;
                bulletSpawned.GetComponent<Weapon>().SetType();
                bulletSpawned = Instantiate(bulletPrefab, bulletSpawnPoint[2].position, bulletSpawnPoint[2].rotation);
                bulletSpawned.GetComponent<Rigidbody>().velocity = bulletSpawned.transform.up * bulletSpeed;
                bulletSpawned.GetComponent<Weapon>().shooted = true;
                bulletSpawned.GetComponent<Weapon>().isGranade = false;
                bulletSpawned.GetComponent<Weapon>().DamageUp(damageIncrement);
                bulletSpawned.GetComponent<Weapon>().type = CollectibleType.Weapon;
                bulletSpawned.GetComponent<Weapon>().SetType();
                break;
            case 2:
                bulletSpawned = Instantiate(bulletPrefab, bulletSpawnPoint[0].position, bulletSpawnPoint[0].rotation);
                bulletSpawned.GetComponent<Rigidbody>().velocity = bulletSpawned.transform.up * bulletSpeed;
                bulletSpawned.GetComponent<Weapon>().shooted = true;
                bulletSpawned.GetComponent<Weapon>().isGranade = false;
                bulletSpawned.GetComponent<Weapon>().DamageUp(damageIncrement);
                bulletSpawned.GetComponent<Weapon>().type = CollectibleType.Weapon;
                bulletSpawned.GetComponent<Weapon>().SetType();
                bulletSpawned = Instantiate(bulletPrefab, bulletSpawnPoint[3].position, bulletSpawnPoint[3].rotation);
                bulletSpawned.GetComponent<Rigidbody>().velocity = bulletSpawned.transform.up * bulletSpeed;
                bulletSpawned.GetComponent<Weapon>().shooted = true;
                bulletSpawned.GetComponent<Weapon>().isGranade = false;
                bulletSpawned.GetComponent<Weapon>().DamageUp(damageIncrement);
                bulletSpawned.GetComponent<Weapon>().type = CollectibleType.Weapon;
                bulletSpawned.GetComponent<Weapon>().SetType();
                bulletSpawned = Instantiate(bulletPrefab, bulletSpawnPoint[4].position, bulletSpawnPoint[4].rotation);
                bulletSpawned.GetComponent<Rigidbody>().velocity = bulletSpawned.transform.up * bulletSpeed;
                bulletSpawned.GetComponent<Weapon>().shooted = true;
                bulletSpawned.GetComponent<Weapon>().isGranade = false;
                bulletSpawned.GetComponent<Weapon>().DamageUp(damageIncrement);
                bulletSpawned.GetComponent<Weapon>().type = CollectibleType.Weapon;
                bulletSpawned.GetComponent<Weapon>().SetType();
                break;
            case 3:
                bulletSpawned = Instantiate(bulletPrefab, bulletSpawnPoint[1].position, bulletSpawnPoint[1].rotation);
                bulletSpawned.GetComponent<Rigidbody>().velocity = bulletSpawned.transform.up * bulletSpeed;
                bulletSpawned.GetComponent<Weapon>().shooted = true;
                bulletSpawned.GetComponent<Weapon>().isGranade = false;
                bulletSpawned.GetComponent<Weapon>().DamageUp(damageIncrement);
                bulletSpawned.GetComponent<Weapon>().type = CollectibleType.Weapon;
                bulletSpawned.GetComponent<Weapon>().SetType();
                bulletSpawned = Instantiate(bulletPrefab, bulletSpawnPoint[2].position, bulletSpawnPoint[2].rotation);
                bulletSpawned.GetComponent<Rigidbody>().velocity = bulletSpawned.transform.up * bulletSpeed;
                bulletSpawned.GetComponent<Weapon>().shooted = true;
                bulletSpawned.GetComponent<Weapon>().isGranade = false;
                bulletSpawned.GetComponent<Weapon>().DamageUp(damageIncrement);
                bulletSpawned.GetComponent<Weapon>().type = CollectibleType.Weapon;
                bulletSpawned.GetComponent<Weapon>().SetType();
                bulletSpawned = Instantiate(bulletPrefab, bulletSpawnPoint[3].position, bulletSpawnPoint[3].rotation);
                bulletSpawned.GetComponent<Rigidbody>().velocity = bulletSpawned.transform.up * bulletSpeed;
                bulletSpawned.GetComponent<Weapon>().shooted = true;
                bulletSpawned.GetComponent<Weapon>().isGranade = false;
                bulletSpawned.GetComponent<Weapon>().DamageUp(damageIncrement);
                bulletSpawned.GetComponent<Weapon>().type = CollectibleType.Weapon;
                bulletSpawned.GetComponent<Weapon>().SetType();
                bulletSpawned = Instantiate(bulletPrefab, bulletSpawnPoint[4].position, bulletSpawnPoint[4].rotation);
                bulletSpawned.GetComponent<Rigidbody>().velocity = bulletSpawned.transform.up * bulletSpeed;
                bulletSpawned.GetComponent<Weapon>().shooted = true;
                bulletSpawned.GetComponent<Weapon>().isGranade = false;
                bulletSpawned.GetComponent<Weapon>().DamageUp(damageIncrement);
                bulletSpawned.GetComponent<Weapon>().type = CollectibleType.Weapon;
                bulletSpawned.GetComponent<Weapon>().SetType();
                break;
            case 4:
                bulletSpawned = Instantiate(bulletPrefab, bulletSpawnPoint[0].position, bulletSpawnPoint[0].rotation);
                bulletSpawned.GetComponent<Rigidbody>().velocity = bulletSpawned.transform.up * bulletSpeed;
                bulletSpawned.GetComponent<Weapon>().shooted = true;
                bulletSpawned.GetComponent<Weapon>().isGranade = false;
                bulletSpawned.GetComponent<Weapon>().DamageUp(damageIncrement);
                bulletSpawned.GetComponent<Weapon>().type = CollectibleType.Weapon;
                bulletSpawned.GetComponent<Weapon>().SetType();
                bulletSpawned = Instantiate(bulletPrefab, bulletSpawnPoint[1].position, bulletSpawnPoint[1].rotation);
                bulletSpawned.GetComponent<Rigidbody>().velocity = bulletSpawned.transform.up * bulletSpeed;
                bulletSpawned.GetComponent<Weapon>().shooted = true;
                bulletSpawned.GetComponent<Weapon>().isGranade = false;
                bulletSpawned.GetComponent<Weapon>().DamageUp(damageIncrement);
                bulletSpawned.GetComponent<Weapon>().type = CollectibleType.Weapon;
                bulletSpawned.GetComponent<Weapon>().SetType();
                bulletSpawned = Instantiate(bulletPrefab, bulletSpawnPoint[2].position, bulletSpawnPoint[2].rotation);
                bulletSpawned.GetComponent<Rigidbody>().velocity = bulletSpawned.transform.up * bulletSpeed;
                bulletSpawned.GetComponent<Weapon>().shooted = true;
                bulletSpawned.GetComponent<Weapon>().isGranade = false;
                bulletSpawned.GetComponent<Weapon>().DamageUp(damageIncrement);
                bulletSpawned.GetComponent<Weapon>().type = CollectibleType.Weapon;
                bulletSpawned.GetComponent<Weapon>().SetType();
                bulletSpawned = Instantiate(bulletPrefab, bulletSpawnPoint[3].position, bulletSpawnPoint[3].rotation);
                bulletSpawned.GetComponent<Rigidbody>().velocity = bulletSpawned.transform.up * bulletSpeed;
                bulletSpawned.GetComponent<Weapon>().shooted = true;
                bulletSpawned.GetComponent<Weapon>().isGranade = false;
                bulletSpawned.GetComponent<Weapon>().DamageUp(damageIncrement);
                bulletSpawned.GetComponent<Weapon>().type = CollectibleType.Weapon;
                bulletSpawned.GetComponent<Weapon>().SetType();
                bulletSpawned = Instantiate(bulletPrefab, bulletSpawnPoint[4].position, bulletSpawnPoint[4].rotation);
                bulletSpawned.GetComponent<Rigidbody>().velocity = bulletSpawned.transform.up * bulletSpeed;
                bulletSpawned.GetComponent<Weapon>().shooted = true;
                bulletSpawned.GetComponent<Weapon>().isGranade = false;
                bulletSpawned.GetComponent<Weapon>().DamageUp(damageIncrement);
                bulletSpawned.GetComponent<Weapon>().type = CollectibleType.Weapon;
                bulletSpawned.GetComponent<Weapon>().SetType();
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
                break;
            case 1:
                grenadeSpawned = Instantiate(grenadePrefab, grenadeSpawnPoint[1].position, grenadeSpawnPoint[1].rotation);
                grenadeSpawned.GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePositionY;
                grenadeSpawned.GetComponent<Rigidbody>().AddForce(transform.forward * throwForce, ForceMode.Impulse);
                grenadeSpawned.GetComponent<Weapon>().isGranade = true;
                grenadeSpawned.GetComponent<Weapon>().explosionTimer -= delayReduction;
                grenadeSpawned.GetComponent<Weapon>().shooted = true;
                grenadeSpawned.GetComponent<Weapon>().DamageUp(damageIncrement);
                grenadeSpawned = Instantiate(grenadePrefab, grenadeSpawnPoint[2].position, grenadeSpawnPoint[2].rotation);
                grenadeSpawned.GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePositionY;
                grenadeSpawned.GetComponent<Rigidbody>().AddForce(transform.forward * throwForce, ForceMode.Impulse);
                grenadeSpawned.GetComponent<Weapon>().isGranade = true;
                grenadeSpawned.GetComponent<Weapon>().explosionTimer -= delayReduction;
                grenadeSpawned.GetComponent<Weapon>().shooted = true;
                grenadeSpawned.GetComponent<Weapon>().DamageUp(damageIncrement);
                break;
            case 2:
                grenadeSpawned = Instantiate(grenadePrefab, grenadeSpawnPoint[0].position, grenadeSpawnPoint[0].rotation);
                grenadeSpawned.GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePositionY;
                grenadeSpawned.GetComponent<Rigidbody>().AddForce(transform.forward * throwForce, ForceMode.Impulse);
                grenadeSpawned.GetComponent<Weapon>().isGranade = true;
                grenadeSpawned.GetComponent<Weapon>().explosionTimer -= delayReduction;
                grenadeSpawned.GetComponent<Weapon>().shooted = true;
                grenadeSpawned.GetComponent<Weapon>().DamageUp(damageIncrement);
                grenadeSpawned = Instantiate(grenadePrefab, grenadeSpawnPoint[3].position, grenadeSpawnPoint[3].rotation);
                grenadeSpawned.GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePositionY;
                grenadeSpawned.GetComponent<Rigidbody>().AddForce(transform.forward * throwForce, ForceMode.Impulse);
                grenadeSpawned.GetComponent<Weapon>().isGranade = true;
                grenadeSpawned.GetComponent<Weapon>().explosionTimer -= delayReduction;
                grenadeSpawned.GetComponent<Weapon>().shooted = true;
                grenadeSpawned.GetComponent<Weapon>().DamageUp(damageIncrement);
                grenadeSpawned = Instantiate(grenadePrefab, grenadeSpawnPoint[4].position, grenadeSpawnPoint[4].rotation);
                grenadeSpawned.GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePositionY;
                grenadeSpawned.GetComponent<Rigidbody>().AddForce(transform.forward * throwForce, ForceMode.Impulse);
                grenadeSpawned.GetComponent<Weapon>().isGranade = true;
                grenadeSpawned.GetComponent<Weapon>().explosionTimer -= delayReduction;
                grenadeSpawned.GetComponent<Weapon>().shooted = true;
                grenadeSpawned.GetComponent<Weapon>().DamageUp(damageIncrement);
                break;
            case 3:
                grenadeSpawned = Instantiate(grenadePrefab, grenadeSpawnPoint[1].position, grenadeSpawnPoint[1].rotation);
                grenadeSpawned.GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePositionY;
                grenadeSpawned.GetComponent<Rigidbody>().AddForce(transform.forward * throwForce, ForceMode.Impulse);
                grenadeSpawned.GetComponent<Weapon>().isGranade = true;
                grenadeSpawned.GetComponent<Weapon>().explosionTimer -= delayReduction;
                grenadeSpawned.GetComponent<Weapon>().shooted = true;
                grenadeSpawned.GetComponent<Weapon>().DamageUp(damageIncrement);
                grenadeSpawned = Instantiate(grenadePrefab, grenadeSpawnPoint[2].position, grenadeSpawnPoint[2].rotation);
                grenadeSpawned.GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePositionY;
                grenadeSpawned.GetComponent<Rigidbody>().AddForce(transform.forward * throwForce, ForceMode.Impulse);
                grenadeSpawned.GetComponent<Weapon>().isGranade = true;
                grenadeSpawned.GetComponent<Weapon>().explosionTimer -= delayReduction;
                grenadeSpawned.GetComponent<Weapon>().shooted = true;
                grenadeSpawned.GetComponent<Weapon>().DamageUp(damageIncrement);
                grenadeSpawned = Instantiate(grenadePrefab, grenadeSpawnPoint[3].position, grenadeSpawnPoint[3].rotation);
                grenadeSpawned.GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePositionY;
                grenadeSpawned.GetComponent<Rigidbody>().AddForce(transform.forward * throwForce, ForceMode.Impulse);
                grenadeSpawned.GetComponent<Weapon>().isGranade = true;
                grenadeSpawned.GetComponent<Weapon>().explosionTimer -= delayReduction;
                grenadeSpawned.GetComponent<Weapon>().shooted = true;
                grenadeSpawned.GetComponent<Weapon>().DamageUp(damageIncrement);
                grenadeSpawned = Instantiate(grenadePrefab, grenadeSpawnPoint[4].position, grenadeSpawnPoint[4].rotation);
                grenadeSpawned.GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePositionY;
                grenadeSpawned.GetComponent<Rigidbody>().AddForce(transform.forward * throwForce, ForceMode.Impulse);
                grenadeSpawned.GetComponent<Weapon>().isGranade = true;
                grenadeSpawned.GetComponent<Weapon>().explosionTimer -= delayReduction;
                grenadeSpawned.GetComponent<Weapon>().shooted = true;
                grenadeSpawned.GetComponent<Weapon>().DamageUp(damageIncrement);
                break;
            case 4:
                grenadeSpawned = Instantiate(grenadePrefab, grenadeSpawnPoint[0].position, grenadeSpawnPoint[0].rotation);
                grenadeSpawned.GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePositionY;
                grenadeSpawned.GetComponent<Rigidbody>().AddForce(transform.forward * throwForce, ForceMode.Impulse);
                grenadeSpawned.GetComponent<Weapon>().isGranade = true;
                grenadeSpawned.GetComponent<Weapon>().explosionTimer -= delayReduction;
                grenadeSpawned.GetComponent<Weapon>().shooted = true;
                grenadeSpawned.GetComponent<Weapon>().DamageUp(damageIncrement);
                grenadeSpawned = Instantiate(grenadePrefab, grenadeSpawnPoint[1].position, grenadeSpawnPoint[1].rotation);
                grenadeSpawned.GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePositionY;
                grenadeSpawned.GetComponent<Rigidbody>().AddForce(transform.forward * throwForce, ForceMode.Impulse);
                grenadeSpawned.GetComponent<Weapon>().isGranade = true;
                grenadeSpawned.GetComponent<Weapon>().explosionTimer -= delayReduction;
                grenadeSpawned.GetComponent<Weapon>().shooted = true;
                grenadeSpawned.GetComponent<Weapon>().DamageUp(damageIncrement);
                grenadeSpawned = Instantiate(grenadePrefab, grenadeSpawnPoint[2].position, grenadeSpawnPoint[2].rotation);
                grenadeSpawned.GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePositionY;
                grenadeSpawned.GetComponent<Rigidbody>().AddForce(transform.forward * throwForce, ForceMode.Impulse);
                grenadeSpawned.GetComponent<Weapon>().isGranade = true;
                grenadeSpawned.GetComponent<Weapon>().explosionTimer -= delayReduction;
                grenadeSpawned.GetComponent<Weapon>().shooted = true;
                grenadeSpawned.GetComponent<Weapon>().DamageUp(damageIncrement);
                grenadeSpawned = Instantiate(grenadePrefab, grenadeSpawnPoint[3].position, grenadeSpawnPoint[3].rotation);
                grenadeSpawned.GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePositionY;
                grenadeSpawned.GetComponent<Rigidbody>().AddForce(transform.forward * throwForce, ForceMode.Impulse);
                grenadeSpawned.GetComponent<Weapon>().isGranade = true;
                grenadeSpawned.GetComponent<Weapon>().explosionTimer -= delayReduction;
                grenadeSpawned.GetComponent<Weapon>().shooted = true;
                grenadeSpawned.GetComponent<Weapon>().DamageUp(damageIncrement);
                grenadeSpawned = Instantiate(grenadePrefab, grenadeSpawnPoint[4].position, grenadeSpawnPoint[4].rotation);
                grenadeSpawned.GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePositionY;
                grenadeSpawned.GetComponent<Rigidbody>().AddForce(transform.forward * throwForce, ForceMode.Impulse);
                grenadeSpawned.GetComponent<Weapon>().isGranade = true;
                grenadeSpawned.GetComponent<Weapon>().explosionTimer -= delayReduction;
                grenadeSpawned.GetComponent<Weapon>().shooted = true;
                grenadeSpawned.GetComponent<Weapon>().DamageUp(damageIncrement);
                break;
        }
    }

    public void AddAmmo()
    {
        int RandomAmmo = UnityEngine.Random.Range(10, 40);
        ammo += RandomAmmo;
    }

    public void AddGrenade()
    {
        grenades++;
    }
}
