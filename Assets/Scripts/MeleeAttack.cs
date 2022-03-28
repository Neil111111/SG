using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MeleeAttack : MonoBehaviour
{
    Animator anim;
    float timer;
    [SerializeField]public float Cooldown = 0.7f;

    private void Start() {

        anim = GetComponent<Animator>();
        anim.SetBool("isAttacking",false);
        anim.SetBool("isIdle",true);
        
    }

    void Update()
    {
        timer += Time.deltaTime;

        Attack();
        
    }

    void Attack()
    {   
        if(Input.GetMouseButtonDown(0) && timer > Cooldown)
        {
            anim.SetBool("isAttacking",true);
            anim.SetBool("isIdle",false);
            timer = 0;
        }
        if(Input.GetMouseButtonUp(0))
        {
            anim.SetBool("isAttacking",false);
            anim.SetBool("isIdle",true);
        }
    }
}
