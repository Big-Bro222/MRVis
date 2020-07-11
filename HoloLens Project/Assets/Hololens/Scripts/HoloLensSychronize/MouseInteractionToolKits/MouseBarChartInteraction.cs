using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

[RequireComponent(typeof(MouseInteractable))]
public class MouseBarChartInteraction : MonoBehaviour
{
    private Vector3 OriginalScale;
    private bool isInteractable;
    private PinchSlider pinchSlider;
    void Start()
    {
        isInteractable = false;

        OriginalScale = transform.localScale;
        pinchSlider = GetComponentInChildren<PinchSlider>();


        MouseEventCoreService.Instance.OnDoubleClicked += OnDoubleClicked;
        MouseEventCoreService.Instance.onScroll += onScroll;

    }

    private void OnDoubleClicked()
    {
        if (MouseEventCoreService.Instance.GazeTarget != transform.gameObject)
        {
            return;
        }
        isInteractable = !isInteractable;
        transform.localScale = isInteractable ? OriginalScale * 1.2f : OriginalScale;
    }

    private void onScroll(bool Delta)
    {
        
        if (MouseEventCoreService.Instance.GazeTarget != transform.gameObject)
        {
            return;
        }
        Debug.Log("Onscroll" + Delta);
        if (pinchSlider.SliderValue >= 0 && pinchSlider.SliderValue <= 1)
        {
            if (Delta)
            {
                pinchSlider.SliderValue += 0.1f;
            }
            else
            {
                pinchSlider.SliderValue -= 0.1f;
            }
        }
        // wired behavior of silder if not adding this script, the slidervalue will always be one
        else{
            if (pinchSlider.SliderValue > 0.5f)
            {
                pinchSlider.SliderValue = 0.999f;
            }
            else
            {
                pinchSlider.SliderValue = 0.001f;
            }
           
        }
        
    }

    private void Update()
    {
        if (MouseEventCoreService.Instance.GazeTarget != transform.gameObject)
        {
            return;
        }
        float MouseY = MouseEventCoreService.Instance.MouseY;
        if (isInteractable)
        {
            Debug.Log(MouseY+"dragging");
            if (pinchSlider.SliderValue >= 0 && pinchSlider.SliderValue <= 1)
            {

                pinchSlider.SliderValue += MouseY;
            }
            else
            {
                if (pinchSlider.SliderValue > 0.5f)
                {
                    pinchSlider.SliderValue = 0.999f;
                }
                else
                {
                    pinchSlider.SliderValue = 0.001f;
                }

            }
        }

    }


}
