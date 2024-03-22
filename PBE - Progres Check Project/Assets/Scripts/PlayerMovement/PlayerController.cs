using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float sprintSpeed = 3.0f;
    [SerializeField] private Vector3 inputVector, moveVector, posit;
    [SerializeField] private Quaternion rotation = Quaternion.identity;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Camera secondaryCamera;
    [SerializeField] private ParticleSystem ps;
    [SerializeField] private GameObject psObject;
    [SerializeField] private Slider staminaSlider1;
    [SerializeField] private Slider staminaSlider2;
    private Vector3 mousePos;
    private bool isSprint, staminaZero;
    Rigidbody rb;
    Transform tf;
    GameUIManager gameUIManager;

    private CapsuleCollider2D col;
    private GameObject Player;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        tf = GetComponent<Transform>();
        Player = GameObject.FindWithTag("Player");
        gameUIManager = GameObject.FindWithTag("UIManager").GetComponent<GameUIManager>();
        psObject.SetActive(false);
    }

    private void Update()
    {
        if (gameUIManager.currentState == GameUIManager.GameState.Playing)
        {
            Aim();
            GetInput();
            CalculateMovement();
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = moveVector;
    }

    private void GetInput()
    {

        inputVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

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

    private void CalculateMovement()
    {
        if (inputVector.x != 0 && inputVector.z != 0)   // Corrects increased movement in diagonal directional movements
        {
            inputVector.x = inputVector.x * 0.75f;
            inputVector.z = inputVector.z * 0.75f;
        }
        else if (inputVector.x != 0 || inputVector.z != 0)
        {
            psObject.SetActive(true);
        }
        else
        {
            psObject.SetActive(false);
        }

 
        float skewedX = inputVector.x + inputVector.z;   // Functions same as a rotational linear transformation that is
        float skewedZ = inputVector.z - inputVector.x;   // Typically done using matrix multiplication. This is more efficient.

        if (isSprint)
        {
            moveVector.x = skewedX * (speed + sprintSpeed);
            moveVector.z = skewedZ * (speed + sprintSpeed);

            staminaSlider1.value -= 0.5f * Time.deltaTime;
            staminaSlider2.value -= 0.5f * Time.deltaTime;
        }
        else if (!isSprint)
        {
            moveVector.x = skewedX * speed;
            moveVector.z = skewedZ * speed;

            if (!staminaZero)
            {
                staminaSlider1.value += 0.25f * Time.deltaTime;
                staminaSlider2.value += 0.25f * Time.deltaTime;
            }
        }

        if (staminaSlider1.value == 0 )
        {
            StartCoroutine(StaminaRechargeDelay());
        }
        else if (staminaSlider1.value == 1)
        {
            StopCoroutine(StaminaRechargeDelay());
        }
    }

    IEnumerator StaminaRechargeDelay()
    {
        staminaZero = true;
        isSprint = false;
        ps.emissionRate = 15;
        staminaSlider1.value = 0f;
        staminaSlider2.value = 0f;
        yield return new WaitForSeconds(3f);
        while (staminaSlider1.value != 1 && staminaZero)
        {
            yield return new WaitForSeconds(0.2f);
            staminaSlider1.value += 0.25f * Time.deltaTime;
            staminaSlider2.value += 0.25f * Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                float skewedX = inputVector.x + inputVector.z;   // Functions same as a rotational linear transformation that is
                float skewedZ = inputVector.z - inputVector.x;   // Typically done using matrix multiplication. This is more efficient.

                moveVector.x = skewedX * speed;
                moveVector.z = skewedZ * speed;
            }
        }
        staminaZero = false;
    }

    private void Aim()
    {
        var (success, position) = GetMousePosition();

        if (success)
        {
            Vector3 direction = position - transform.position;
            direction.y = 0;
            transform.forward = direction;
        }
    }

    private (bool success, Vector3 position) GetMousePosition()
    {
        Ray ray = secondaryCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, groundMask))
        {
            return (success: true, position: hitInfo.point);
        }
        else
        {
            return (success: false, position: Vector3.zero);
        }
    }

    private void OnDrawGizmos()
    {
        var (success, position) = GetMousePosition();
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(position, 0.5f);
        var ray = secondaryCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, groundMask))
        {
            Gizmos.DrawLine(secondaryCamera.transform.position, hitInfo.point);
        }

    }
}
