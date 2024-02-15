using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    [SerializeField] private LayerMask groundMask;
    private Camera mainCamera;



    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        Aim();
    }

    private void Aim()
    {
        var (success, position) = GetMousePosition();
        if (success)
        {
            var direction = position - transform.position;   // Calculate the direction
            direction.y = 0;   // Ignore the height difference.

            transform.forward = direction;   // Make the transform look in the direction.
        }
    }

    private (bool success, Vector3 position) GetMousePosition()
    {
        var ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, groundMask))
        {
            return (success: true, position: hitInfo.point);   // The Raycast hit something, return with the position.
        }
        else
        {
            return (success: false, position: Vector3.zero);   // The Raycast did not hit anything.
        }
    }

}