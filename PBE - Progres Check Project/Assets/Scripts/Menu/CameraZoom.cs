using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] private Transform endPoint;
    [SerializeField] private float speed;
    private bool isStart;


    private void Start()
    {
        isStart = false;
        StartCoroutine(WaitTime());
    }
    private void Update()
    {
        Zoom();
    }

    void Zoom()
    {
        if (isStart)
        {
            float percentage = speed * Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, endPoint.position, percentage);
        }
    }

    IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(4f);
        isStart = true;
    }
}
