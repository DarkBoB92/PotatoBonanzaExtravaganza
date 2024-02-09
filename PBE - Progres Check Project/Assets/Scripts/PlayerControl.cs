using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    // Pre-Requisite Variables --------------------------------------------------------------------

    [SerializeField] private Rigidbody rb;
    [SerializeField] private ParticleSystem ps;
    [SerializeField] private GameObject psObject;
    [SerializeField] private float speed = 5;
    [SerializeField] private float turnSpeed = 360;
    private Vector3 playerInput;
    private bool isSprint = false;



    // Main Loops ----------------------------------------------------------------------------------

    private void Start()
    {
        psObject.SetActive(false);
    }

    void Update()
    {
        GatherInput();
        Look();
    }



    // Fixed Physics Loop -------------------------------------------------------------------------

    void FixedUpdate()
    {
        Move();
    }



    // Functions For Movement ---------------------------------------------------------------------

    void GatherInput()
    {

        playerInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));   // Gain inputs for X & Z axis, stored in a Vector3 - Y axis not required

        if (!isSprint && Input.GetKeyDown(KeyCode.LeftShift))   // Press Shift = Sprint
        {
            isSprint = true;
            ps.emissionRate = 60;
        }
        else if (isSprint && Input.GetKeyUp(KeyCode.LeftShift))   // Unpress Shift = Stop Sprint
        {
            isSprint = false;
            ps.emissionRate = 15;
        }
    }

    void Look()
    {
        if (playerInput != Vector3.zero)   // If the player presses any direction key (WASD), then run:
        {
            psObject.SetActive(true);

            var matrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));    // Creating a matrix which holds the value 45° on the X axis (parameters are Z, X, Y in that order)
    

            var skewedInput = matrix.MultiplyPoint3x4(playerInput);   // Adjusting player input by the rotation stored in the matrix


            var relative = (transform.position + skewedInput) - transform.position;   // Store the targetted direction (as a result of the players input + 45° rotation) in a Vector3
            var rotation = Quaternion.LookRotation(relative, Vector3.up);   // Store the information which would tell the GameObject which way to rotate and setting it as up/forward

            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, turnSpeed * Time.deltaTime);   // Apply the rotation information to the GameObject SMOOTHLY, with framerate independence
        }
        else if (playerInput == Vector3.zero)
        {
            psObject.SetActive(false);
        }
    }
    
    void Move()
    {
        if (isSprint)
        {
            rb.MovePosition(transform.position + (transform.forward * playerInput.magnitude) * (speed + 3) * Time.deltaTime);   // Move the GameObject forward by the set speed + 5 with framerate independence
        }
        else if (!isSprint)
        {
            rb.MovePosition(transform.position + (transform.forward * playerInput.magnitude) * speed * Time.deltaTime);   // Move the GameObject forward by the set speed with framerate independence
        }
    }
}