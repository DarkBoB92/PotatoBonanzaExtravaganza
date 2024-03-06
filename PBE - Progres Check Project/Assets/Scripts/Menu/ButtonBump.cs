using System.Collections;
using UnityEngine;

public class ButtonBump : MonoBehaviour
{
    private bool isHover;
    private Coroutine bumpCoroutine;
    private Vector3 originalPosition;

    private void Start()
    {
        originalPosition = transform.position;
    }

    private void OnMouseEnter()
    {
        if (!isHover)
        {
            FindObjectOfType<MenuAudio>().AudioTrigger(MenuAudio.SoundFXCat.MouseHover, transform.position, 0.2f);
            bumpCoroutine = StartCoroutine(BumpUp());
        }
    }

    private void OnMouseExit()
    {
        if (isHover)
        {
            StopCoroutine(bumpCoroutine);
            StartCoroutine(BumpDown());
        }
    }

    IEnumerator BumpUp()
    {
        float bumpDistance = 0.2f;
        float duration = 0.1f;
        float elapsedTime = 0f;
        Vector3 startPos = transform.position;
        Vector3 targetPos = originalPosition + Vector3.back * bumpDistance;

        isHover = true;

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startPos, targetPos, elapsedTime / duration);   // Smoothly move between origin to target
            elapsedTime += Time.deltaTime;
            yield return null;   // Don't wait any longer
        }

        transform.position = targetPos;   // Set position to target position
    }

    IEnumerator BumpDown()
    {
        float duration = 0.1f;
        float elapsedTime = 0f;
        Vector3 startPos = transform.position;

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startPos, originalPosition, elapsedTime / duration);   // Smoothly move between current position and original position
            elapsedTime += Time.deltaTime;
            yield return null;   // Don't wait any longer
        }

        transform.position = originalPosition;   // Set position to original position
        isHover = false;
    }
}
