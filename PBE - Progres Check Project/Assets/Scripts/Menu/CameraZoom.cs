using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] private Transform endPoint;
    [SerializeField] private float speed;

    private void Update()
    {
        Zoom();
    }

    void Zoom()
    {
        transform.position = Vector3.Lerp(transform.position, endPoint.position, speed * Time.deltaTime);
    }
}
