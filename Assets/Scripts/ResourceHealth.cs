using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceHealth : MonoBehaviour
{  
    public float _resourceHealth;

    public void TakeDamage(float damage)
    {
        _resourceHealth -= damage;
    }
}
