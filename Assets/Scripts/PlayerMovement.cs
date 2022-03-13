using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    public float enduranceTime = 10f;


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

    Rigidbody rb;

    void Awake()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Movement();
        Crouch();
        Sprint();
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
        if(Input.GetKey(KeyCode.LeftShift) && isGrounded && isCrouched == false && enduranceTime > 0 )
        {
            speed += 8f;
            enduranceTime -= Time.deltaTime;
            
        }
        else if ( enduranceTime < 10){
            enduranceTime += Time.deltaTime;
        }
    }
}
