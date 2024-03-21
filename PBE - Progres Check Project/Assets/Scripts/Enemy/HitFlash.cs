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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(FlashRoutine());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Weapon")
        {
            StartCoroutine(FlashRoutine());
        }
    }
    private void OnTriggerEnter(Collider other)
    {
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
