using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit;

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LableController : MonoBehaviour
{
    public bool showLabels;

    public GameObject Mapviz;
    private Canvas[] canvases;
    private int scale;
    private Vector3[] sliderstartpos;
    private int unit;

    private void Start()
    {
        canvases = Mapviz.GetComponentsInChildren<Canvas>();
        unit = 288;
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
    }

    public void SliderValueUpdate()
    {
        scale++;

        if (scale >= 6)
        {
            scale = 0;
        }

        foreach (Canvas canvas in canvases)
        {
            Vector3 canvastransform = canvas.GetComponent<RectTransform>().localPosition;
            canvas.GetComponent<RectTransform>().localPosition = new Vector3(canvastransform.x, unit * scale*0.2F, canvastransform.z);
        }
    }

}

