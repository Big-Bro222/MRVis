using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

public class PositionConstrain : MonoBehaviour
{

    public bool highest;
    public string axis;
    public bool lockmovement;
    public SliceBehaviorHandler sliceBehaviorHandler;


    private Vector3 startPos;
    //public PinchSlider pinchSlider;

    void Awake()
    {
        startPos = transform.localPosition ;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Destroiable"))
        {
            sliceBehaviorHandler.destroyItems.Add(other.gameObject);
            other.gameObject.SetActive(false);
        }
    }


    public void OnSliderUpdated(SliderEventData eventData)
    {
        if(lockmovement)
        {
            return;
        }
        float valuefalse = -eventData.NewValue / 2;
        float valuetrue = (1-eventData.NewValue) / 2;

        float value = highest ? valuetrue : valuefalse;

        

        if (axis.Equals("x"))
        {
            transform.localPosition = new Vector3(startPos.x-value, startPos.y, startPos.z);
            if (highest)
            {
                sliceBehaviorHandler.xMax = transform.localPosition.x;
            }
            else
            {
                sliceBehaviorHandler.xMin = transform.localPosition.x;
            }
        }
        else if (axis.Equals("y"))
        {
            transform.localPosition = new Vector3(startPos.x, startPos.y-value, startPos.z);
            if (highest)
            {
                sliceBehaviorHandler.yMax = transform.localPosition.y;
            }
            else
            {
                sliceBehaviorHandler.yMin  = transform.localPosition.y;
            }
        }
        else if (axis.Equals("z"))
        {
            transform.localPosition = new Vector3(startPos.x, startPos.y, startPos.z+value);
            if (highest)
            {
                sliceBehaviorHandler.zMax = -transform.localPosition.z;
            }
            else
            {
                sliceBehaviorHandler.zMin = -transform.localPosition.z;
            }
        }
    }


    void Update()
    {
        
    }
}
