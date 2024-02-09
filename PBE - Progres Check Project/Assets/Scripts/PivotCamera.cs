using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PivotCamera : MonoBehaviour
{

    // Pre-Requisite Variables --------------------------------------------------------------------

    [SerializeField] private float targetAngle = 45f;
    [SerializeField] private float currentAngle = 0f;
    [SerializeField] private float mouseSensitivity = 2f;
    [SerializeField] private float rotationSpeed = 5f;



    // Main Loop ----------------------------------------------------------------------------------

    void Update()
    {
        DragCamera();
    }



    // Functions ----------------------------------------------------------------------------------

    void DragCamera()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        if (Input.GetMouseButton(0))   // If left button held, targetAngle is changed to mouseX value * dampening factor
        {
            targetAngle += mouseX * mouseSensitivity;
        }
        else   // If not held, round to the nearest 45° angle
        {
            targetAngle = Mathf.Round(targetAngle / 45);
            targetAngle *= 45;
        }

        if (targetAngle <  0)   // Ensures angle does not go below 0
        {
            targetAngle += 359;
        }
        
        if (targetAngle >= 360)   // Ensures angle does not go above 360
        {
            targetAngle = 0;
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            targetAngle = 45;
        }

        currentAngle = Mathf.Lerp(transform.eulerAngles.y, targetAngle, rotationSpeed * Time.deltaTime);   // Calculate interpolation between current angle and target angle by a set speed with framerate independence
        transform.rotation = Quaternion.Euler(30, currentAngle, 0);   // Applies rotation to GameObject using previous calculations
    }
}
