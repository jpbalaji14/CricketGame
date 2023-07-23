using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBowler : MonoBehaviour
{
    public enum States { Idle, Aiming, Running, Bowling}
    public Animator bowlerAnimator;
    public float runSpeed;
    public float runDuration; //Duration that the player should run before bowl
    public float runTimer;
    private States bowlerState;

    public GameObject fakeBall;
    public BallLauncher ballLauncherScript;
    public Transform ballTarget;
    private float bowlingSpeed;
    public float flightDurationMultiplier;
    public static Action<float> onBallThrown;
    // Start is called before the first frame update
    void Start()
    {
      bowlerState =States.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        ManageBowlerStates();
       
    }

    private void ManageBowlerStates()
    {
        switch(bowlerState) 
        {
            case States.Idle:
                break; 
            case States.Aiming:
                break; 
            case States.Running:
                Run();
                break;
            case States.Bowling:
                break; 
        }
    }
    [ContextMenu("TestBowlAction")]
    public void StartRunning(float bowlSpeed)
    {
        bowlingSpeed = bowlSpeed;
        bowlerState = States.Running;
        bowlerAnimator.SetInteger("State", 1);
    }
    void Run()
    {
        transform.position += Vector3.forward * runSpeed * Time.deltaTime;
        runTimer += Time.deltaTime;
        if(runTimer > runDuration) 
        {
            StartBowling();
        }
    }

    void StartBowling()
    {
        bowlerState = States.Bowling;
        bowlerAnimator.SetInteger("State", 2);
    }

    public void ThrowBall()
    {
        fakeBall.SetActive(false);
        Vector3 fromPos = fakeBall.transform.position;
        Vector3 targetPos = ballTarget.position;

        //Calculate flight duration depending on bowl speed
        //velocity = distance / time

        float distance= Vector3.Distance(fromPos, targetPos);
        float velocity = bowlingSpeed / 3.6f;  // 3.6 is used to convert kmph to metre

        float flightDuration = flightDurationMultiplier * distance / velocity;
        Debug.Log("Duration : " + flightDuration+ ", BowlSpeed : " + bowlingSpeed);
        ballLauncherScript.LaunchBall(fromPos,targetPos,flightDuration);
        onBallThrown?.Invoke(flightDuration);
    }


   
}
