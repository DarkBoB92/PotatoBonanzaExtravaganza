using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Intro : MonoBehaviour
{
    [SerializeField] Image background;
    [SerializeField] Image logo;
    float backgroundAlpha;



    void Start()
    {
        Cursor.visible = false;
        StartCoroutine(backgroundAnim());
        StartCoroutine(logoAnim());
    }


    IEnumerator backgroundAnim()
    {
        Color tempColor = background.color;
        yield return new WaitForSeconds(4);
        while (tempColor.a != 0)
        {
            tempColor.a -= 0.01f;
            background.color = tempColor;
            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator logoAnim()
    {
        yield return new WaitForSeconds(1);
        Color tempColor = logo.color;
        FindObjectOfType<MenuAudio>().AudioTrigger(MenuAudio.SoundFXCat.LogoWoosh, transform.position, 0.5f);
        tempColor.a = 0f;
        while (tempColor.a <= 1)
        {
            tempColor.a += 0.02f;
            logo.color = tempColor;
            yield return new WaitForSeconds(0.01f);
        }

        yield return new WaitForSeconds(2.3f);

        while (tempColor.a >= 0)
        {
            tempColor.a -= 0.04f;
            logo.color = tempColor;
            yield return new WaitForSeconds(0.01f);
        }
        Cursor.visible = true;
    }
}
