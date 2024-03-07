using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsButton : MonoBehaviour
{
    [SerializeField] Image settingsBackground;
    [SerializeField] GameObject buttons;
    [SerializeField] GameObject volumeSlider;



    private void Start()
    {
        volumeSlider.SetActive(false);
    }

    private void OnMouseUpAsButton()
    {
        FindObjectOfType<MenuAudio>().AudioTrigger(MenuAudio.SoundFXCat.ButtonSelect, transform.position, 0.5f);
        StartCoroutine(settingsFadeIn());
    }

    IEnumerator settingsFadeIn()
    {
        Color tempColor = settingsBackground.color;
        while (tempColor.a <= 1)
        {
            tempColor.a += 0.04f;
            settingsBackground.color = tempColor;
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.5f);
        buttons.SetActive(false);
        FindObjectOfType<MenuAudio>().AudioTrigger(MenuAudio.SoundFXCat.ButtonSelect, transform.position, 0.5f);
        volumeSlider.SetActive(true);

    }
}
