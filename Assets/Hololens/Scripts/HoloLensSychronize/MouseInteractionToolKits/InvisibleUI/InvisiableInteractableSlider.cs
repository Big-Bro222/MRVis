using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InvisiableInteractableSlider: MonoBehaviour
{
    public int UIid;
    public UnityEvent OnvalueChanged;
    public float SliderValue=1;
    void Start()
    {
        MouseEventCoreService.Instance.OnSliderChanged += OnValueChanged;

    }

    private void OnValueChanged(float sliderValue, int uiId)
    {
        if (uiId.Equals(UIid))
        {
            SliderValue = sliderValue;
            OnvalueChanged.Invoke();
        }
    }
}
