using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderHoloLens : MonoBehaviour
{
    //this responsible for the zoom in/out for the HoloLens map visualization which is controlled by monitor application using the monitor display.(obsolete)It's working but we didn't use it.
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
