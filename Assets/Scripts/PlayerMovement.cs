using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float maxSpeed;
    public float acceleration;
    public Camera cam;
    public Transform player;

    private float xInp;
    private float yInp;

    // Update is called once per frame
    void Update()
    {
        myInputs();
        CameraTransform();
    }

    public void myInputs()
    {
        xInp = Input.GetAxis("Horizontal");
        yInp = Input.GetAxis("Vertical");

    }
    public void Movement()
    {
        
    }
    public void CameraTransform()
    {
        cam.transform.position = player.transform.position;
    }
    
}
