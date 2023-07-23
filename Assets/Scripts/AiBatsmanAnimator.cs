using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiBatsmanAnimator : MonoBehaviour
{
    public AiBatsman aiBatsman;

    public void StartDetectingHits()
    {
        aiBatsman.StartDetectingHits();
    }
}
