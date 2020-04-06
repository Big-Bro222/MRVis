using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public GameObject Mapviz;
    private Canvas[] canvases;
    private float scale;
    private Vector3[] sliderstartpos;
    private int unit;
    public PinchSlider pinchSlider;

    private void Start()
    {
        canvases = Mapviz.GetComponentsInChildren<Canvas>();
        unit = 144;
        
        
    }

    private void Update()
    {
        scale = pinchSlider.SliderValue * 5;
        SliderValueUpdate(scale);
    }

    private void SliderValueUpdate(float sliderscale)
    {
        foreach (Canvas canvas in canvases)
        {
            Vector3 canvastransform = canvas.GetComponent<RectTransform>().localPosition;
            canvas.GetComponent<RectTransform>().localPosition = new Vector3(canvastransform.x, unit* sliderscale, canvastransform.z);
        }
    }

}
