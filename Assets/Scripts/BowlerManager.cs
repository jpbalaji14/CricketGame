using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlerManager : MonoBehaviour
{
    public BowlerTarget bowlerTargetScript;
    public BowlerPowerSlider powerSliderScript;
    public PlayerBowler bowlerScript;
    public GameObject aimingPanelGameObject;
    public GameObject bowlingPanelGameObject;
    public static Action OnAimingStarted; 
    public static Action OnBowlingStarted;
    public AnimationCurve bowlingSpeedCurve;
    public Vector2 minMaxBowlingSpeed;
    // Start is called before the first frame update
    void Start()
    {
        StartAiming();
        BowlerPowerSlider.onPowerSliderStopped += powerSliderStoppedCallback;
    }
    private void OnDestroy()
    {
        BowlerPowerSlider.onPowerSliderStopped -= powerSliderStoppedCallback;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void StartAiming()
    {
        bowlerTargetScript.EnableMovement();
        OnAimingStarted?.Invoke();
        aimingPanelGameObject.SetActive(true);
        bowlingPanelGameObject.SetActive(false);
    }

    public void StartBowling()
    {
        bowlerTargetScript.DisableMovement(); // target slider stop
        bowlingPanelGameObject.SetActive(true);
        aimingPanelGameObject.SetActive(false);
        OnBowlingStarted ?.Invoke();  //cam shift
        powerSliderScript.StartMovingSlider(); //power slider start move
    }
    void powerSliderStoppedCallback(float sliderValue)
    {
        float lerp = bowlingSpeedCurve.Evaluate(sliderValue);  //on anim curve, slider value is X axis, and it find and gives the Y axis value;

        float bowlingSpeed = Mathf.Lerp(minMaxBowlingSpeed.x, minMaxBowlingSpeed.y, lerp);  // Between the min max value, based on the lerp value calculated, it returns the speed.
        bowlerScript.StartRunning(bowlingSpeed);
    }
}
