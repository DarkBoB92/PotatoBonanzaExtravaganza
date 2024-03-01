using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBump : MonoBehaviour
{
    private bool isHover;

    private void OnMouseEnter()
    {
        StartCoroutine(Bump());
    }

    IEnumerator Bump()
    {
        float bumpDistance = 0.2f;
        float duration = 0.1f;
        float elapsedTime = 0f;
        Vector3 startPos = transform.position;
        Vector3 targetPos = startPos + Vector3.back * bumpDistance;

        isHover = true;

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startPos, targetPos, elapsedTime / duration);   // Smoothly move between origin to target
            elapsedTime += Time.deltaTime;
            yield return null;   // Don't wait any longer
        }

        transform.position = targetPos;   // swap object position and target to ready for fall
        elapsedTime = 0f;   // Reset elapsed variable

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(targetPos, startPos, elapsedTime / duration);   // Smoothly move between origin to target
            elapsedTime += Time.deltaTime;
            yield return null;   // Don't wait any longer
        }

        transform.position = startPos;   // reset position to original start position in any chance this wasnt already done
        isHover = false;
    }
}
