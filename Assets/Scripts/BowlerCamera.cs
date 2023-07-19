using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlerCamera : MonoBehaviour
{
    public GameObject aimingCamera;
    public GameObject bowlingCamera;
    private void Awake()
    {
        BowlerManager.OnAimingStarted += EnableAimingCamera; 
        BowlerManager.OnBowlingStarted += EnableBowlingCamera;
    }
    private void OnDestroy()
    {
        BowlerManager.OnAimingStarted -= EnableAimingCamera;
        BowlerManager.OnBowlingStarted -= EnableBowlingCamera;
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
    }
    private void EnableBowlingCamera()
    {
        bowlingCamera.SetActive(true);
        aimingCamera.SetActive(false);
    }
}
