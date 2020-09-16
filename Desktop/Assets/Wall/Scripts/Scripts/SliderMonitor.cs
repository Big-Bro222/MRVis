using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderMonitor : MonoBehaviour
{
    public Slider slider;
    private Vector3 localScale;
    void Start()
    {
        localScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SliderChanger()
    {
        transform.localScale = localScale * slider.value;
    }
}
