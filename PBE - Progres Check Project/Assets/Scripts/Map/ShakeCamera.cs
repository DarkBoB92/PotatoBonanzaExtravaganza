using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ShakeCamera : MonoBehaviour
{
    public Animator camAnim;
    public string isShake = "isShake";
    public Toggle screenShakeToggle;

    

    public void CamShake()
    {
        if (screenShakeToggle.isOn)
        {
            StartCoroutine(ShakeOnTime());
        }
    }

    IEnumerator ShakeOnTime()
    {
        camAnim.SetTrigger("isShake");
        camAnim.SetBool(isShake, true);
        yield return new WaitForSeconds(0.06f);
        camAnim.SetBool(isShake, false);
    }

    public void ToogleShakeCamera()
    {
        if (screenShakeToggle.isOn)
        {
            screenShakeToggle.isOn = false;
        }
        else
        {
            screenShakeToggle.isOn = true;
        }
    }
}
