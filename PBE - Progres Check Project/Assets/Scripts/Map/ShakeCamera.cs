using System.Collections;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    public Animator camAnim;
    public string isShake = "isShake";



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
}
