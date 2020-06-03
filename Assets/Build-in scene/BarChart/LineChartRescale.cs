using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

public class LineChartRescale : MonoBehaviour
{
    private PinchSlider pinchSlider;
    private List<Transform> lineCharts;
    void Start()
    {
        pinchSlider = GetComponentInChildren<PinchSlider>();
        lineCharts = new List<Transform>();

        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).Find("lineChart"))
            {
                lineCharts.Add(transform.GetChild(i).Find("lineChart"));
                //transform.GetChild(i).Find("lineChart").Translate(new Vector3(0, 0, -0.2f));
            }
        }



    }

    // Update is called once per frame
    void Update()
    {
        foreach (Transform lineChart in lineCharts)
        {
            float sliderValue = -pinchSlider.SliderValue;
            lineChart.localScale = new Vector3(1, 1, sliderValue);
        }
    }
}
