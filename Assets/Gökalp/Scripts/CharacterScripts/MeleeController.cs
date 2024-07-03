using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeController : MonoBehaviour
{
    Animator anim;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)) //left click
        {
            SetAttack(1);
        }else if(Input.GetMouseButtonDown(1)) //right click
        {
            SetAttack(2);
        }
    }

    private void SetAttack(int attackType)
    {
        if (anim.GetBool("CanAttack"))
        {
            anim.SetTrigger("Attack");
            anim.SetInteger("AttackType", attackType);
        }
    }
}
