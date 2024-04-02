using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class NewPlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float sprintSpeed = 3.0f;
    [SerializeField] private float sensitivity = 0.1f;
    [SerializeField] private Vector3 inputVectorRH, inputVectorLH, inputVectorGamepad, moveVector, posit;
    [SerializeField] private Quaternion rotation = Quaternion.identity;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Camera secondaryCamera;
    [SerializeField] private ParticleSystem ps;
    [SerializeField] private GameObject psObject;
    [SerializeField] private Slider staminaSlider1;
    [SerializeField] private Slider staminaSlider2;
    public bool gamepad = false;
    public bool rightHand = true;
    private Vector2 mouseOnScreenPos, stickOnScreenPos, mousePos, mousecurrentPos;
    private bool isSprint, staminaZero;
    Rigidbody rb;
    Transform tf;
    GameUIManager gameUIManager;
    PlayerInput playerInput;
    InputDeviceChange inputDeviceChange;
    private CapsuleCollider2D col;
    private GameObject Player;
    ControlOptions controlPanel;

    private void Start()
    {   
        rb = GetComponent<Rigidbody>();
        tf = GetComponent<Transform>();
        playerInput = GetComponent<PlayerInput>();
        Player = GameObject.FindWithTag("Player");
        gameUIManager = GameObject.FindWithTag("UIManager").GetComponent<GameUIManager>();
        controlPanel = gameUIManager.GetComponentInChildren<ControlOptions>(true);
        psObject.SetActive(false);
        LoadInputCongig();
        CheckPlayerInput();
    }

    private void Update()
    {        
        if (Input.GetKeyDown(KeyCode.G)) 
        {
            if (!gamepad)
            {
                gamepad = true;
            }
            else
            {
                gamepad = false;
            }
            CheckPlayerInput();
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (!rightHand)
            {
                rightHand = true;
            }
            else
            {
                rightHand = false;
            }            
        }
        LoadInputCongig();
        if (gameUIManager.currentState == GameUIManager.GameState.Playing)
        {
            if (gamepad)
            {
                AimWithGamepad();
                UpdateMousePosition();
                CalculateMovementGamepad();
            }
            else
            {
                AimWithMouse();
                if (rightHand)
                {
                    CalculateMovementRH();
                }
                else
                {                    
                    CalculateMovementLH();
                }
            }            
        }
        if (gamepad && gameUIManager.currentState == GameUIManager.GameState.Paused)
        {
            UpdateMousePosition();
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = moveVector;
    }

    void LoadInputCongig()
    {
        if(controlPanel != null)
        {
            if(controlPanel.mouseKeyboardRH.isOn)
            {
                rightHand = true;
                gamepad = false;
            }
            else if(controlPanel.mouseKeyboardLH.isOn)
            {
                rightHand = false;
                gamepad = false;
            }
            else if(controlPanel.joypad.isOn)
            {
                gamepad = true;
            }
            CheckPlayerInput();
        }
    }
    
    public void CheckPlayerInput()
    {
        if(playerInput != null)
        {
            if(gamepad)
            {
                playerInput.SwitchCurrentControlScheme("Gamepad", Gamepad.current);
            }
            else
            {
                playerInput.SwitchCurrentControlScheme(Keyboard.current, Mouse.current);
            }
        }
    }

    private void CalculateMovementRH()
    {
        if (inputVectorRH.x != 0 || inputVectorRH.z != 0)
        {
            psObject.SetActive(true);
        }
        else
        {
            psObject.SetActive(false);
        }


        float skewedX = inputVectorRH.x + inputVectorRH.z;   // Functions same as a rotational linear transformation that is
        float skewedZ = inputVectorRH.z - inputVectorRH.x;   // Typically done using matrix multiplication. This is more efficient.
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

        if (staminaSlider1.value == 0)
        {
            StartCoroutine(StaminaRechargeDelay());
        }
        else if (staminaSlider1.value == 1)
        {
            StopCoroutine(StaminaRechargeDelay());
        }
    }
 

    private void CalculateMovementLH()
    {
        if (inputVectorLH.x != 0 || inputVectorLH.z != 0)
        {
            psObject.SetActive(true);
        }
        else
        {
            psObject.SetActive(false);
        }


        float skewedX = inputVectorLH.x + inputVectorLH.z;   // Functions same as a rotational linear transformation that is
        float skewedZ = inputVectorLH.z - inputVectorLH.x;   // Typically done using matrix multiplication. This is more efficient.

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

        if (staminaSlider1.value == 0)
        {
            StartCoroutine(StaminaRechargeDelay());
        }
        else if (staminaSlider1.value == 1)
        {
            StopCoroutine(StaminaRechargeDelay());
        }
    }

    private void CalculateMovementGamepad()
    {
        if (inputVectorGamepad.x != 0 || inputVectorGamepad.z != 0)
        {
            psObject.SetActive(true);
        }
        else
        {
            psObject.SetActive(false);
        }


        float skewedX = inputVectorGamepad.x + inputVectorGamepad.z;   // Functions same as a rotational linear transformation that is
        float skewedZ = inputVectorGamepad.z - inputVectorGamepad.x;   // Typically done using matrix multiplication. This is more efficient.

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

        if (staminaSlider1.value == 0)
        {
            StartCoroutine(StaminaRechargeDelay());
        }
        else if (staminaSlider1.value == 1)
        {
            StopCoroutine(StaminaRechargeDelay());
        }
    }

    void OnMoveRH(InputValue input)
    {
        Vector2 xzValue = input.Get<Vector2>();
        inputVectorRH = new Vector3(xzValue.x, 0, xzValue.y);
    }

    void OnMoveLH(InputValue input)
    {
        Vector2 xyValue = input.Get<Vector2>();
        inputVectorLH = new Vector3(xyValue.x, 0, xyValue.y);        
    }

    void OnMoveGamepad(InputValue input)
    {
        Vector2 xyValue = input.Get<Vector2>();
        inputVectorGamepad = new Vector3(xyValue.x, 0, xyValue.y);
    }

    void OnSprint(InputValue input)
    {
        if (input.isPressed)   // Press Shift = Sprint
        {
            if (!isSprint)
            {
                isSprint = true;
                ps.emissionRate = 60;
            }
        }
        else   // Unpress Shift = Stop Sprint
        {
            if (isSprint)
            {
                isSprint = false;
                ps.emissionRate = 15;
            }
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

            float skewedX;
            float skewedZ;

            if (rightHand)
            {
                skewedX = inputVectorRH.x + inputVectorRH.z;   // Functions same as a rotational linear transformation that is
                skewedZ = inputVectorRH.z - inputVectorRH.x;   // Typically done using matrix multiplication. This is more efficient.
            }
            else
            {
                skewedX = inputVectorLH.x + inputVectorLH.z;   // Functions same as a rotational linear transformation that is
                skewedZ = inputVectorLH.z - inputVectorLH.x;   // Typically done using matrix multiplication. This is more efficient.
            }


            moveVector.x = skewedX * speed;
            moveVector.z = skewedZ * speed;
        }
        staminaZero = false;
    }

    private void AimWithMouse()
    {
        var (success, position) = GetMousePosition();

        if (success)
        {
            Vector3 direction = position - transform.position;
            direction.y = 0;
            transform.forward = direction;
        }
    }
    private void AimWithGamepad()
    {
        var (success, position) = GetMousePosition();

        if (success)
        {
            Vector3 direction = position - transform.position;
            direction.y = 0;
            transform.forward = direction;
        }
    }

    void OnLookMouse(InputValue input)
    {
        if (!gamepad)
        {
            mouseOnScreenPos = input.Get<Vector2>();
        }
    }

    void OnLookGamepad(InputValue input)
    {
        if (gamepad)
        {
            stickOnScreenPos = input.Get<Vector2>();
        }
    }

    void UpdateMousePosition()
    {        
        Vector2 mouseMovement = stickOnScreenPos * sensitivity;
        mousePos = Mouse.current.position.ReadValue() + mouseMovement;
        mousePos.x = Mathf.Clamp(mousePos.x, 0, Screen.width);
        mousePos.y = Mathf.Clamp(mousePos.y, 0, Screen.height);
        Mouse.current.WarpCursorPosition(mousePos);
    }

    private (bool success, Vector3 position) GetMousePosition()
    {
        Ray ray;

        if (!gamepad)
        {
            ray = secondaryCamera.ScreenPointToRay(mouseOnScreenPos);
        }
        else
        {
            ray = secondaryCamera.ScreenPointToRay(mousePos);
        }

        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, groundMask))
        {
            return (success: true, position: hitInfo.point);
        }
        else
        {
            return (success: false, position: Vector3.zero);
        }
    }

    void OnPause(InputValue input)
    {
        if(gameUIManager  != null)
        {
            gameUIManager.CheckInputs(input.isPressed);
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
