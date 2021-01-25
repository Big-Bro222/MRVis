using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Utilities;
using Microsoft.MixedReality.Toolkit.Input;


public class HandDetectionHandler : MonoBehaviour,IMixedRealityGestureHandler
{
    [SerializeField]
    private MixedRealityInputAction selectAction; // You'll need to set this in the Inspector to Select



    private void OnEnable()
    { 
        CoreServices.InputSystem?.RegisterHandler<IMixedRealityGestureHandler>(this);
    }

    private void OnDisable()
    {
        CoreServices.InputSystem?.UnregisterHandler<IMixedRealityGestureHandler>(this);
    }

    public void OnGestureCompleted(InputEventData eventData)
    {

        Debug.Log("Completed");       
    }

    public void OnGestureStarted(InputEventData eventData)
    {
        
        if (eventData.Handedness == Handedness.Right)
        {

            Debug.Log("right");
        }
        else {
            Debug.Log("left");

        }
    }
    public void OnGestureUpdated(InputEventData eventData)
    {
        Debug.Log("Updated");

    }
    public void OnGestureCanceled(InputEventData eventData)
    {
        Debug.Log("Canceled");

    }

}
