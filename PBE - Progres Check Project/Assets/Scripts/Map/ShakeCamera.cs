using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ShakeCamera : MonoBehaviour
{
    public Animator camAnim;
    public string isShake = "isShake";
    public Toggle screenShakeToggle;

    private void Update()
    {
        Toggled();
    }

    public void CamShake()
    {
        StartCoroutine(ShakeOnTime());
    }

    IEnumerator ShakeOnTime()
    {
        camAnim.SetTrigger("isShake");
        camAnim.SetBool(isShake, true);
        yield return new WaitForSeconds(0.06f);
        camAnim.SetBool(isShake, false);
    }

    public void Toggled()
    {
        if (screenShakeToggle.isOn)
        {
            CamShake();
            Debug.Log("Boolean is true");
        }
        else
        {
            camAnim.SetBool(isShake, false);
        }
    }
}
