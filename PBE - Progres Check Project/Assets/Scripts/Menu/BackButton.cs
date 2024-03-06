using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
    [SerializeField] Image settingsBackground;
    [SerializeField] GameObject buttons;

    private void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            StartCoroutine(SettingsFadeOut());
        }
    }

    IEnumerator SettingsFadeOut()
    {
        FindObjectOfType<MenuAudio>().AudioTrigger(MenuAudio.SoundFXCat.ButtonBack, transform.position, 0.5f);
        buttons.SetActive(true);
        Color tempColor = settingsBackground.color;
        while (tempColor.a >= 0)
        {
            tempColor.a -= 0.04f;
            settingsBackground.color = tempColor;
            yield return new WaitForSeconds(0.01f);
        }
    }
}
