using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlerAnimator : MonoBehaviour
{
    public PlayerBowler playerBowlerScript;

    public void BallThrow()
    {
        playerBowlerScript.ThrowBall();
    }
}
