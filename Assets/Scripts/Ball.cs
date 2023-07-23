using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody ballRigidbody;
    BallLauncher ballLauncher;
     // Adjust this value to control the bounce reduction (0.0f - 1.0f)

    private void Start()
    {
        ballRigidbody=this.GetComponent<Rigidbody>();
        ballLauncher=this.GetComponentInParent<BallLauncher>();
    }
    void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.CompareTag("Ground"))
        //{
        //    Debug.Log("Touched Ground");
        //    // Calculate the force to apply for bouncing
        //    Vector3 normalForce = collision.contacts[0].normal;
        //    Vector3 bounceForce = normalForce * ballRigidbody.velocity.magnitude * ballLauncher.bounceFactor;
        //    bounceForce *= (1 - ballLauncher.dampingAmount);
        //    //// Apply the bounce force to the ball
        //    ballRigidbody.AddForce(bounceForce, ForceMode.Impulse);


        //    float seamAngle = Input.GetAxis("Mouse X") * ballLauncher.maxSeamAngle;
        //    Quaternion seamRotation = Quaternion.Euler(0f, seamAngle, 0f);
        //    Vector3 seamDirection = seamRotation * transform.right;
        //    ballRigidbody.AddForce(seamDirection * ballLauncher.seamForce, ForceMode.Impulse);
        //}
    }
}
