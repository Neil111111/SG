using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MeleeAttack : MonoBehaviour
{
    Animator anim;

    private void Start() {

        anim = GetComponent<Animator>();
        
    }

    void Update()
    {
        Attack();
    }

    void Attack()
    {
        if(Input.GetMouseButton(0))
        {
            anim.SetBool("isAttacking",true);
        }
        if(Input.GetMouseButtonUp(0))
        {
            anim.SetBool("isAttacking",false);
        }
    }
}
