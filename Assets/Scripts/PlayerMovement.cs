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
    //checks

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    //Objects
    public Camera cam;
    public Transform player;
    //Bools
    [SerializeField]private bool isGrounded;
    [SerializeField]private bool isCrouched;
    [SerializeField]private bool isSprinting;
    [SerializeField]private bool hasRegenerated;
    [SerializeField]private bool isJumping;
    //Physics
    Rigidbody rb;

    void Awake()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
    }
    void Start()
    {
        playerStamina = maxStamina;
    }

    void Update()
    {
        Movement();
        MyInputs();
        Stamina();
        Sprint();
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
        if(Input.GetKeyDown(KeyCode.LeftControl))
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

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);
        

        if(Input.GetButtonDown("Jump") && isGrounded && playerStamina > 9.9f)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            playerStamina -= 20f;
            isJumping = true;
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
}
