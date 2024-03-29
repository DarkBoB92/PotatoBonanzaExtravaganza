using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HitFlash : MonoBehaviour
{
    [SerializeField] private Material flashMaterial;
    [SerializeField] private float duration = 0.15f;
    [SerializeField] private MeshRenderer[] mr;
    [SerializeField] private Material[] originalMaterial;
    [SerializeField] private ShakeCamera shake;
    Weapon weapon;



    private void Start()
    {
        shake = GameObject.FindGameObjectWithTag("ScreenShake").GetComponent<ShakeCamera>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Weapon")
        {
            weapon = GetComponent<Weapon>();
            if (weapon != null && weapon.shooted)
            {
                if (shake != null && shake.screenShakeToggle.isOn)
                {
                    shake.CamShake();
                }
                FindObjectOfType<GameplayAudio>().AudioTrigger(GameplayAudio.SoundFXCat.Hit, transform.position, 0.5f);
                StartCoroutine(FlashRoutine());
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (shake != null && shake.screenShakeToggle.isOn)
        {
            shake.CamShake();
        }
        FindObjectOfType<GameplayAudio>().AudioTrigger(GameplayAudio.SoundFXCat.Hit, transform.position, 0.5f);
        StartCoroutine(FlashRoutine());
    }

    IEnumerator FlashRoutine()
    {
        for (int i = 0; i < mr.Length; i++)
        {
            mr[i].sharedMaterial = flashMaterial;
        }

        yield return new WaitForSeconds(duration);

        for (int i = 0;i < originalMaterial.Length; i++)
        {
            mr[i].sharedMaterial = originalMaterial[i];
        }
    }
}
