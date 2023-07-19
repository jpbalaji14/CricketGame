using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLauncher : MonoBehaviour
{
    //public Transform ballTarget;
    //public Transform from;
    //public float flightDuration;
    public GameObject ballPrefab;

    public float dampingAmount = 0.5f; // Adjust this value to control the damping (0.0f - 1.0f)
    public float bounceFactor = 0.5f;
    public float seamForce;         // The force with which the ball moves sideways (seam movement)
    public float maxSeamAngle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LaunchBall(Vector3 fromPos, Vector3 targetPos, float flightDuration)   // v0 = pT - gt2 - p0 / t   pT= ballTargetPos  |  g= gravity | t= flightDuration | p0 =fromPosition
    {
        Vector3 pT = targetPos;
        Vector3 gt2 = Physics.gravity * flightDuration * flightDuration / 2;
        Vector3 p0 = fromPos;
        Vector3 initialVelocity = (pT - gt2 - p0) /flightDuration;

        GameObject ball = Instantiate(ballPrefab, fromPos, Quaternion.identity, transform);
        ball.GetComponent<Rigidbody>().velocity = initialVelocity;
    }
}
