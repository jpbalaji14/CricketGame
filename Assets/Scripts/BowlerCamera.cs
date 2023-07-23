using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlerCamera : MonoBehaviour
{
    public GameObject aimingCamera;
    public GameObject bowlingCamera; 
    public GameObject ballCamera;
    private void Awake()
    {
        BowlerManager.OnAimingStarted += EnableAimingCamera; 
        BowlerManager.OnBowlingStarted += EnableBowlingCamera; 
        AiBatsman.onBallHit += EnableBallCamera;
    }
    private void OnDestroy()
    {
        BowlerManager.OnAimingStarted -= EnableAimingCamera;
        BowlerManager.OnBowlingStarted -= EnableBowlingCamera;
        AiBatsman.onBallHit -= EnableBallCamera;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void EnableAimingCamera()
    {
        aimingCamera.SetActive(true);
        bowlingCamera.SetActive(false);
        ballCamera.SetActive(false);
    }
    private void EnableBowlingCamera()
    {
        bowlingCamera.SetActive(true);
        aimingCamera.SetActive(false);
        ballCamera.SetActive(false);
    } 
    private void EnableBallCamera(Transform ball)
    {
        ballCamera.GetComponent<Cinemachine.CinemachineVirtualCamera>().Follow=ball; 
        ballCamera.GetComponent<Cinemachine.CinemachineVirtualCamera>().LookAt=ball;
        ballCamera.SetActive(true);
        bowlingCamera.SetActive(false);
        aimingCamera.SetActive(false);
    }
}
