using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    // Pre-Requisite Variables --------------------------------------------------------------------

    [SerializeField] private Rigidbody rb;
    [SerializeField] private ParticleSystem ps;
    [SerializeField] private GameObject psObject;
    [SerializeField] private float speed = 5;
    [SerializeField] private float sprintSpeed = 8;
    private Vector3 playerInput;
    private bool canMove;
    private bool isSprint;



    // Main Loops ----------------------------------------------------------------------------------

    private void Start()
    {
        psObject.SetActive(false);
    }

    void Update()
    {
        GatherInput();
        ParticleManagement();
    }



    // Fixed Physics Loop -------------------------------------------------------------------------

    void FixedUpdate()
    {
        Move();
    }



    // Functions For Movement ---------------------------------------------------------------------

    void GatherInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            canMove = true;
            if (canMove)
            {
                playerInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));   // Gain inputs for X & Z axis, stored in a Vector3 - Y axis not required
            }
        }
        else 
        {
            canMove = false;
            playerInput = Vector3.zero;
        }

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
    
    void Move()
    {
        if (isSprint)
        {
            rb.MovePosition(transform.position + (transform.forward * playerInput.magnitude) * (sprintSpeed * Time.deltaTime));   // Move the GameObject forward by the set speed + 5 with framerate independence
        }
        else if (!isSprint)
        {
            rb.MovePosition(transform.position + (transform.forward * playerInput.magnitude) * speed * Time.deltaTime);   // Move the GameObject forward by the set speed with framerate independence
        }
    }

    void ParticleManagement()
    {
        if (playerInput != Vector3.zero)    // If the player presses any direction key (WASD), then run:
        {
            psObject.SetActive(true);
        }
        else if (playerInput == Vector3.zero)
        {
            psObject.SetActive(false);
        }
    }
}