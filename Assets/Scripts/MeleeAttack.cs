using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MeleeAttack : MonoBehaviour
{
    
    Animator anim;
    float timer;
    public float damage = 10f;
    public float range = 5f;
    public Camera fpsCam;
    [SerializeField]public float Cooldown = 0.7f;

    private void Start() {

        anim = GetComponent<Animator>();
        anim.SetBool("isAttacking",false);
        anim.SetBool("isIdle",true);
        
    }

    void Update()
    {
        timer += Time.deltaTime;

        if(Input.GetMouseButtonDown(0))
        {
            Attack();
            timer = 0f;
        }
        
    }

    void Attack()
    {    
        RaycastHit hit;
        Ray ray = fpsCam.ScreenPointToRay(new vector2(Screen.width /2,Screen.height /2));
        if(Physics.Raycast(fpsCam.transform.position,fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            if(hit.collider.GetComponent<ResourceHealth>() != null)
            {
                if(collider.tag = "resource")
                {
                    ResourceHealth.TakeDamage();
                }
            }
        }
    }
}
