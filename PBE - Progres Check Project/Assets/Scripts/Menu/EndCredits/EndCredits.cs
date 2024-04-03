using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndCredits : MonoBehaviour
{
    void Update()
    {
        if (Input.anyKey)
        {
            StartCoroutine(SettingsFadeOut());
        }
    }

    IEnumerator SettingsFadeOut()
    {
        FindObjectOfType<MenuAudio>().AudioTrigger(MenuAudio.SoundFXCat.ButtonBack, transform.position, 0.5f);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Menu");
    }
}
