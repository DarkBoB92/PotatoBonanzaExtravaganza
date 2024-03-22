using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public int ammo, grenades;
    [SerializeField] GameObject bulletPrefab, grenadePrefab, knifeAmmo, timerAmmo;
    [SerializeField] float bulletSpeed;
    [SerializeField] Transform bulletSpawnPoint, grenadeSpawnPoint;
    [SerializeField] float fireRate, throwForce;
    [SerializeField] TextMeshProUGUI knifeAmmoText, timerAmmoText;
    bool isGrenade;
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
            FindObjectOfType<GameplayAudio>().AudioTrigger(GameplayAudio.SoundFXCat.Shoot, transform.position, 0.5f);
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
