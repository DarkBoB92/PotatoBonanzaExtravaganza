using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpsEffects : MonoBehaviour
{
    [SerializeField] float yAngle, rotationSpeed;
    // Update is called once per frame
    void Update()
    {
        rotationSpeed = yAngle * Time.deltaTime;
        transform.Rotate(new Vector3 (0f, rotationSpeed, 0f), Space.Self); 
    }
}
