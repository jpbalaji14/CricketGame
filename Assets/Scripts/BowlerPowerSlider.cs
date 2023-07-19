using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BowlerPowerSlider : MonoBehaviour
{
    public Slider powerSlider;
    public float moveSpeed;
    public bool canMove;
    public static Action<float> onPowerSliderStopped;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove) 
        {
            MoveSlider();
        }
    }

    public void StartMovingSlider() 
    {
        canMove = true;
    }
    public void StopMovingSlider()
    {
        if (canMove)
        {
            canMove = false;
        }
        onPowerSliderStopped?.Invoke(powerSlider.value);
    }

    public void MoveSlider()
    {
        powerSlider.value = (Mathf.Sin(Time.time * moveSpeed) + 1) / 2;
    }
}
