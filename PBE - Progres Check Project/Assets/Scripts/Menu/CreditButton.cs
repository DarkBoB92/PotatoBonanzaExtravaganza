using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditButton : MonoBehaviour
{    private void OnMouseUpAsButton()
    {
        StartCoroutine(DelayAndSFX());
    }

    IEnumerator DelayAndSFX()
    {
        FindObjectOfType<MenuAudio>().AudioTrigger(MenuAudio.SoundFXCat.ButtonSelect, transform.position, 0.5f);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("EndCredits");
    }
}
