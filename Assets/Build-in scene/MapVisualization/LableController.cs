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
    private TextMeshPro[] labels;
    private int scale;
    private Vector3[] sliderstartpos;
    private int unit;
    private PinchSlider pinchSlider;

    private void Start()
    {
        labels = Mapviz.GetComponentsInChildren<TextMeshPro>();
        unit = 72;
        pinchSlider = FindObjectOfType<PinchSlider>();
        //if (!showLabels)
        //{
        //    foreach (Canvas canvas in canvases)
        //    {
        //        canvas.gameObject.SetActive(false);
        //    }
        //}
    }


    public void SliderValueUpdate()
    {
        
        float sliderscale = pinchSlider.SliderValue;
        foreach (TextMeshPro label in labels)
        {
            Vector3 labeltransform = label.GetComponent<RectTransform>().localPosition;
            label.GetComponent<RectTransform>().localPosition = new Vector3(labeltransform.x, labeltransform.y, -unit * sliderscale*0.1F);
        }
    }

}

