using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] private Transform endPoint;
    [SerializeField] private float speed;
    private bool isStart = true;


    private void Start()
    {
        FindObjectOfType<MenuAudio>().AudioTrigger(MenuAudio.SoundFXCat.FridgeHum, transform.position, 0.35f);
        FindObjectOfType<MenuAudio>().AudioTrigger(MenuAudio.SoundFXCat.Jazz, transform.position, 0.3f);
        FindObjectOfType<MenuAudio>().AudioTrigger(MenuAudio.SoundFXCat.KitchenSFX, transform.position, 0.05f);
        if (Manager.instance != null && Manager.instance.firstTime)
        {
            isStart = false;
            StartCoroutine(WaitTime());
        }
        else
        {
            PlaySound();
        }
    }
    private void Update()
    {
        Zoom();
    }

    void Zoom()
    {
        if (isStart)
        {
            float percentage = speed * Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, endPoint.position, percentage);            
        }
    }

    IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(4f);
        PlaySound();
        isStart = true;
    }

    void PlaySound()
    {
        FindObjectOfType<MenuAudio>().AudioTrigger(MenuAudio.SoundFXCat.CameraWoosh, transform.position, 0.5f);
    }
}
