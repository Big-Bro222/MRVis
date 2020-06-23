using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderHoloLens : MonoBehaviour
{
    public InvisiableInteractableSlider Slider;
    private Vector3 localScale;
    void Start()
    {
        localScale = transform.localScale;
    }

    public void rescale()
    {
        transform.localScale = Slider.SliderValue * localScale;
    }
}
