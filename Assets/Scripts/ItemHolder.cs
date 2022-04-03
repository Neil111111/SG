using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHolder : MonoBehaviour
{
    public GameObject hand;
    private void MyInputs()
    {
        if(Input.GetMouseButtonDown(1))
        {
            hand.SetActive(true);
        }
        else{
            hand.SetActive(false);
        }
    }
}
