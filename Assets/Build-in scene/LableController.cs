using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Microsoft.MixedReality.Toolkit.UI;

public class LableController : MonoBehaviour
{
    public bool showLabels;

    public GameObject Mapviz;
    private Canvas[] canvases;
    private int scale;
    private Vector3[] sliderstartpos;
    private int unit;
    private PinchSlider pinchSlider;

    private void Start()
    {
        canvases = Mapviz.GetComponentsInChildren<Canvas>();
        unit = 72;
        pinchSlider = FindObjectOfType<PinchSlider>();
        if (!showLabels)
        {
            foreach (Canvas canvas in canvases)
            {
                canvas.gameObject.SetActive(false);
            }
        }
    }

    private void Update()
    {
        //Debug.Log(CoreServices.InputSystem.RaiseGestureStarted.);
    }

    public void SliderValueUpdate()
    {
        float sliderscale = pinchSlider.SliderValue;
        foreach (Canvas canvas in canvases)
        {
            Vector3 canvastransform = canvas.GetComponent<RectTransform>().localPosition;
            canvas.GetComponent<RectTransform>().localPosition = new Vector3(canvastransform.x, canvastransform.y, -unit * sliderscale*0.1F);
        }
    }

    //public void ClickerValueUpdate()
    //{
    //    scale++;

    //    if (scale >= 6)
    //    {
    //        scale = 0;
    //    }

    //    foreach (Canvas canvas in canvases)
    //    {
    //        Vector3 canvastransform = canvas.GetComponent<RectTransform>().localPosition;
    //        canvas.GetComponent<RectTransform>().localPosition = new Vector3(canvastransform.x, unit * scale * 0.2F, canvastransform.z);
    //    }
    //}

}

