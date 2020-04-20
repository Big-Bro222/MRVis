using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;

public class HandManager : MonoBehaviour, IMixedRealityGestureHandler<Vector3>
{
    private IMixedRealityInputSystem inputSystem = null;
    private IMixedRealityInputSystem InputSystem
    {
        get
        {
            if (inputSystem == null)
            {
                MixedRealityServiceRegistry.TryGetService<IMixedRealityInputSystem>(out inputSystem);
            }
            return inputSystem;
        }
    }
    public void OnGestureCanceled(InputEventData eventData)
    {
        var action = eventData.MixedRealityInputAction.Description;

        if (action == "Hold Action")
        {
            Debug.Log("holding");
        }
        else if (action == "Manipulate Action")
        {
            Debug.Log("manipulation");
        }
        else if (action == "Navigation Action")
        {
            Debug.Log("navigation");
        }
    }

    public void OnGestureCompleted(InputEventData<Vector3> eventData)
    {
        var action = eventData.MixedRealityInputAction.Description;
        if (action == "Manipulate Action")
        {
            Debug.Log("manipulation");
        }
        else if (action == "Navigation Action")
        {
            Debug.Log("navigation");
        }
    }

    public void OnGestureCompleted(InputEventData eventData)
    {
        var action = eventData.MixedRealityInputAction.Description;

        if (action == "Hold Action")
        {
            Debug.Log("holding");
        }
    }

    public void OnGestureStarted(InputEventData eventData)
    {
        var action = eventData.MixedRealityInputAction.Description;

        if (action == "Hold Action")
        {
            Debug.Log("holding");
        }
        else if (action == "Manipulate Action")
        {
            Debug.Log("manipulation");
        }
        else if (action == "Navigation Action")
        {
            Debug.Log("navigation");
        }
    }

    public void OnGestureUpdated(InputEventData<Vector3> eventData)
    {
        var action = eventData.MixedRealityInputAction.Description;

        if (action == "Manipulate Action")
        {
            Debug.Log("manipulation");
        }
        else if (action == "Navigation Action")
        {
            Debug.Log("navigation");
        }
    }

    public void OnGestureUpdated(InputEventData eventData)
    {
        var action = eventData.MixedRealityInputAction.Description;

        if (action == "Hold Action")
        {
            Debug.Log("holding");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }


    void Update()
    {
        
    }
}
