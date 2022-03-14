using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    public float maxStamina = 100f;
    public float playerStamina;
    public float staminaRegen = 0.5f;
    public float staminaDrain = 1f;


    public float gravity = -9.8f;
    public float jumpHeight = 3f;
    Vector3 velocity;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public Camera cam;
    public Transform player;

    public bool isGrounded;
    public bool isCrouched;
    public bool isSprinting;
    public bool hasRegenerated;

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
        Crouch();
        Sprint();
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
        

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    void Crouch()
    {
        if(Input.GetButton("Crouch") && isGrounded == true)
        {
            player.transform.localScale = new Vector3 (2.5f,1.25f,2.5f);
            gravity = -120f;
            isCrouched = true;
        }else if(Input.GetButtonUp("Crouch")){
            player.transform.localScale = new Vector3 (2.5f,2.5f,2.5f);
            gravity = -50f;
            isCrouched = false;
        }
    }
    void Sprint()
    {
        if(hasRegenerated)
        {
            if(isSprinting == true && isGrounded == true && playerStamina > 0)
            {
                speed = 24f;
                playerStamina -= staminaDrain * Time.deltaTime;
            }
            else
            {
                speed = 12f;
            }
        }
    }
    void Stamina()
    {
        if(isSprinting == false)
        {
            if(playerStamina <= maxStamina - 0.01f)
            {
                playerStamina += staminaRegen * Time.deltaTime;

                if(playerStamina >= maxStamina)
                {
                    hasRegenerated = true;
                }
            }
        }
    }
}
