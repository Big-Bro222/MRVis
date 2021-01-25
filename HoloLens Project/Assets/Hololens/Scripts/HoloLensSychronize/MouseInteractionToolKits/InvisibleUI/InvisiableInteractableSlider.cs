using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InvisiableInteractableSlider: MonoBehaviour
{
    public string UIid;
    public UnityEvent OnvalueChanged;
    public float SliderValue=1;
    void Start()
    {
        MouseEventCoreService.Instance.OnSliderChanged += OnValueChanged;

    }

    private void OnValueChanged(float sliderValue, string uiId)
    {
        if (uiId.Equals(UIid))
        {
            Debug.Log(UIid+ " is working"+sliderValue);
            SliderValue = sliderValue;
            OnvalueChanged.Invoke();
        }
    }
}
