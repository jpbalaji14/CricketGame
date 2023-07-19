using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class AiBatsman : MonoBehaviour
{
    public BowlerTarget bowlerTarget;
    public Animator batsmanAnimator;
    public float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
       Vector3 targetPos = transform.position;
        targetPos.x = bowlerTarget.transform.position.x;

        float difference = targetPos.x - transform.position.x;
        if (difference == 0)
        {
            batsmanAnimator.Play("Batsman_Idle");
        }
        else if(difference > 0)
        {
            batsmanAnimator.Play("Batsman_SideWalk_L");
        }
        else
        {
            batsmanAnimator.Play("Batsman_SideWalk_R");
        }
        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed*Time.deltaTime);
    }
}
