using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HandManager : MonoBehaviour,IMixedRealityGestureHandler
{
   [SerializeField]
    private MixedRealityInputAction selectAction; // You'll need to set this in the Inspector to Select

    public TextMeshPro protienText;
    public TextMeshPro sugarText;
    public TextMeshPro fatText;
    public TextMeshPro textMeshPro;
    private void OnEnable()
    {
        CoreServices.InputSystem?.RegisterHandler<IMixedRealityGestureHandler>(this);
        protienText.enabled = true;
        sugarText.enabled = true;
        fatText.enabled=true;
    }

    private void OnDisable()
    {
    CoreServices.InputSystem?.UnregisterHandler<IMixedRealityGestureHandler>(this);
    }

    public void OnGestureCompleted(InputEventData eventData)
    {

    Debug.Log("Completed");
    textMeshPro.SetText("Completed");
    protienText.enabled = protienText.enabled ? !protienText.enabled : protienText.enabled;
}

    public void OnGestureStarted(InputEventData eventData)
    {
            Debug.Log("Start");
    textMeshPro.SetText("Start");
    protienText.enabled = protienText.enabled ? protienText.enabled : !protienText.enabled;

}
    public void OnGestureUpdated(InputEventData eventData) {
        Debug.Log("Updated");
        textMeshPro.SetText("Updated");

    }
    public void OnGestureCanceled(InputEventData eventData) {
        Debug.Log("Canceled");
        textMeshPro.SetText("Canceled");
        protienText.enabled = protienText.enabled ? !protienText.enabled : protienText.enabled;

    }

}
