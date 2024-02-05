using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
    // Pre-Requisite Variables --------------------------------------------------------------------

    public Vector3 SpawnPoint;

    // Main Loop ----------------------------------------------------------------------------------



    // Functions ----------------------------------------------------------------------------------

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.transform.position = SpawnPoint;
            other.attachedRigidbody.velocity = Vector3.zero;
        }
    }
}
