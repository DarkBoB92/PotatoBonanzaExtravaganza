using System.Collections;
using UnityEngine;

public class NonHostileController : MonoBehaviour
{
    // Pre-Requisite Variables --------------------------------------------------------------------

    [SerializeField] private GameObject player;
    private Vector3 appliedDirection;
    private Vector3 calculatedDirection;
    private float speed;
    private bool isHopping = false;



    // Main Loops ---------------------------------------------------------------------------------

    private void FixedUpdate()
    {
        FollowDirection();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isHopping && other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Hop());
        }
    }



    // Functions ----------------------------------------------------------------------------------

    void FollowDirection()
    {
        speed = 2 * Time.deltaTime;
        calculatedDirection = player.transform.position - transform.position;   // Calculate direction between GameObject vector positions
        appliedDirection = Vector3.RotateTowards(transform.forward, calculatedDirection, speed, 0f);   // Applying the direction into a usable Vector3

        transform.rotation = Quaternion.LookRotation(appliedDirection);   // Apply new calculated Vector3
    }

    IEnumerator Hop()
    {
        float hopHeight = 0.5f;
        float duration = 0.2f;
        float elapsedTime = 0f;
        Vector3 startPos = transform.position;
        Vector3 targetPos = startPos + Vector3.up * hopHeight;

        isHopping = true;

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
        isHopping = false;
    }
}
