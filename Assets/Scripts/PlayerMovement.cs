using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;

    //Stamina
    public float maxStamina = 100f;
    public float playerStamina;
    public float staminaRegen = 0.5f;
    public float staminaDrain = 1f;

    //Jump
    public float gravity = -9.8f;
    public float jumpHeight = 3f;
    Vector3 velocity;

    private Vector3 crouchScale = new Vector3 (2.5f,1.25f,2.5f);
    private Vector3 playerScale;

    //Checks
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    //Objects
    public Camera cam;
    public Transform player;
    public Transform orientation;
    public Transform hand;

    //Bools
    [SerializeField]private bool isGrounded;
    [SerializeField]private bool isCrouched;
    [SerializeField]private bool isSprinting;
    [SerializeField]private bool hasRegenerated;
    [SerializeField]private bool isJumping;

    //Look
    public float mouseSensitivity = 50f;
    public float mouseSensitivityMultiplier = 1f;
    private float xRotation;
    //Physics
    Rigidbody rb;

    //Animations
    Animator anim;
    void Awake()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        Animator anim = GetComponent<Animator>();
    }
    void Start()
    {
        playerStamina = maxStamina;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Movement();
        MyInputs();
        Stamina();
        Sprint();
        Look();
        
    }

    void MyInputs()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            isSprinting = true;
        }
        else{
            isSprinting = false;
        }
        
        //crouching
        if(Input.GetKeyDown(KeyCode.LeftControl) && isGrounded)
            
            StartCrouch();
        if(Input.GetKeyUp(KeyCode.LeftControl))
            StopCrouch();
    }

    public void Movement()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = (transform.right * x + transform.forward * z);

        controller.Move(move * speed * Time.deltaTime);
        

        if(Input.GetButtonDown("Jump") && isGrounded && !isCrouched && playerStamina > 9.9f )
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            playerStamina -= 20f;
        }
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    void StartCrouch()
    {
        transform.localScale = crouchScale;
        transform.position = new Vector3(transform.position.x,transform.position.y - 1.25f,transform.position.z);
        isCrouched = true;
    }

    void StopCrouch()
    {
        transform.localScale = new Vector3 (2.5f,2.5f,2.5f);
        transform.position = new Vector3(transform.position.x,transform.position.y + 1.25f,transform.position.z);
        isCrouched = false;
        
    }

    void Sprint()
    {
        if(isSprinting == true && playerStamina > 0 && !isCrouched)
        {
            speed = 24f;
            playerStamina -= staminaDrain * Time.deltaTime;
        }
        else
        {
            speed = 12f;
        }
        
    }

    void Stamina()
    {
        if(playerStamina > maxStamina) playerStamina = maxStamina;

        
        if(isSprinting == false)
        {
            if(playerStamina <= 99)
            {
                playerStamina += staminaRegen * Time.deltaTime;

                if(playerStamina >= maxStamina)
                {
                    hasRegenerated = true;
                }
                else{
                    hasRegenerated = false;
                }

                
            }
        }
        
    }
    private float desiredX;
    private void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.fixedDeltaTime * mouseSensitivityMultiplier;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.fixedDeltaTime * mouseSensitivityMultiplier;

        Vector3 rot = cam.transform.localRotation.eulerAngles;
        desiredX = rot.y + mouseX;

        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, - 90f, 90f);
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        orientation.transform.localRotation = Quaternion.Euler(0,desiredX, 0).normalized;
        
        player.Rotate(Vector3.up * mouseX);
    }
}
