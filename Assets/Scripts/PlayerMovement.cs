using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float maxSpeed;
    public float acceleration;

    private float xInp;
    private float yInp;

    // Update is called once per frame
    void Update()
    {
        
    }

    void myInputs()
    {
        xInp = Input.GetAxis("Horizontal");
        yInp = Input.GetAxis("Vertical");
        
    }
    
}
