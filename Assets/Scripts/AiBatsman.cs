using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class AiBatsman : MonoBehaviour
{
    enum State
    {
        Moving, Hitting
    }
    public BowlerTarget bowlerTarget;
    public Animator batsmanAnimator;
    public float moveSpeed;
    State batsmanState;
    bool canDetectHits;
    public BoxCollider batCollider;
    public LayerMask ballLayerMask;
    public float hitTimer;
    public float maxHitDuration;
    public Vector2 minMaxHitVelocity;
    // Start is called before the first frame update
    void Start()
    {
        batsmanState=State.Moving;
        PlayerBowler.onBallThrown += BallThrownCallback;
    }
    private void OnDestroy()
    {
        PlayerBowler.onBallThrown -= BallThrownCallback;
    }
    // Update is called once per frame
    void Update()
    {
        ManageState();
    }
    private void ManageState()
    {
        switch (batsmanState)
        {
            case State.Moving:
                Move();
                break;
            case State.Hitting:
                if(canDetectHits) 
                {
                    CheckForHits();
                }
                break;
        }
    }
    void BallThrownCallback(float ballFlightDuration)
    {
        batsmanState = State.Hitting;

        StartCoroutine(WaitandHitBall());
        IEnumerator WaitandHitBall()
        {
            float bestDelay = ballFlightDuration - 0.4f;
            float delay= UnityEngine.Random.Range(bestDelay - 0.1f, bestDelay + 0.1f);
            yield return new WaitForSeconds(delay);
            batsmanAnimator.Play("Batsman_Shot");
        }
    }
    private void Move()
    {
       Vector3 targetPos = transform.position;
        targetPos.x = GetTargetX();
        targetPos.x = Mathf.Clamp(targetPos.x, - 0.6f, 0.6f);

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

     float GetTargetX()
    {
        Vector3 bowlerShootPosition = new Vector3(-0.35f,1, -14.5f);
        Vector3 shootDirection = bowlerTarget.transform.position - bowlerShootPosition;

        float shootAngle = Vector3.Angle(Vector3.right, shootDirection);

        //ab = bc /sin(alpha)

        float bc = transform.position.z - bowlerShootPosition.z;
        float ab= bc / Mathf.Sin(shootAngle * Mathf.Deg2Rad);

        Vector3 targetAiPosition = bowlerShootPosition + shootDirection.normalized * ab;
        return targetAiPosition.x +0.5f;
    }


    public void StartDetectingHits()
    {
        canDetectHits=true;
        hitTimer = 0;
    }
    void CheckForHits()
    {
        Vector3 center = batCollider.transform.TransformPoint(batCollider.center);
        Vector3 halfExtents = 1.5f * batCollider.size / 2;
        Quaternion rotation = batCollider.transform.rotation;

        Collider[] detectedBalls= Physics.OverlapBox(center, halfExtents,rotation,ballLayerMask);

        for(int i=0; i< detectedBalls.Length; i++)
        {
            BallDetectedCallBack(detectedBalls[i]);
            return;
        }
        hitTimer += Time.deltaTime;
    }

    void BallDetectedCallBack(Collider ballCollider)
    {
        canDetectHits = false;

        ShootBall(ballCollider.transform);
    }

    void ShootBall(Transform ball)
    {
        // compare hittimer with max duation
        // if hittime = 0  max hit velocity 
        // if hittime >  max hit duration   min hit velocity 

        float lerp = Mathf.Clamp01( hitTimer / maxHitDuration);
        float hitVeloctiy = Mathf.Lerp(minMaxHitVelocity.y, minMaxHitVelocity.x,lerp);

        ball.GetComponent<Rigidbody>().velocity = (Vector3.back + Vector3.up + Vector3.right * UnityEngine.Random.Range(-1f,1f)) * hitVeloctiy;
    }
}
