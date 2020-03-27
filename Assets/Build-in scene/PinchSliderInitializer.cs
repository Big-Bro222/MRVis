using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinchSliderInitializer : MonoBehaviour
{
    BoundingBox boundingBox;
    ManipulationHandler manipulationHandler;
    PinchSlider pinchSlider;
    SliderSounds sliderSounds;
    // Start is called before the first frame update
    void Start()
    {
        boundingBox = GetComponent<BoundingBox>();
        manipulationHandler = GetComponent<ManipulationHandler>();
        pinchSlider = GetComponent<PinchSlider>();
        sliderSounds = GetComponent<SliderSounds>();

        AdjustSlider(false);

    }

    public void AdjustSlider(bool isAdjustable)
    {
        boundingBox.enabled = isAdjustable;
        manipulationHandler.enabled = isAdjustable;
        pinchSlider.enabled = !isAdjustable;
        sliderSounds.enabled = !isAdjustable;
    }

}
