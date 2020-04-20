using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

public class SliceBehaviorHandler : MonoBehaviour
{

    public Transform slicingCube;

    
    private Transform front;
    private Transform back;
    private Transform right;
    private Transform left;
    private Transform up;
    private Transform down;

    public List<GameObject> destroyItems;

    public float xMin;
    public float xMax;
    public float yMin;
    public float yMax;
    public float zMin;
    public float zMax;

    public PinchSlider highestSlider;
    public PinchSlider lowestSlider;


    void Start()
    {
        front = slicingCube.Find("front");
        back = slicingCube.Find("back");
        right = slicingCube.Find("right");
        left = slicingCube.Find("left");
        up = slicingCube.Find("up");
        down = slicingCube.Find("down");
        XAxisUnlock();


    }

    public void XAxisUnlock()
    {
        SliceController(front, true);
        SliceController(back, true);
        SliceController(right, false);
        SliceController(left, false);
        SliceController(up, true);
        SliceController(down, true);


        highestSlider.SliderValue = xMax * 2;
        lowestSlider.SliderValue = (xMin + 0.5f) * 2;
    }

    public void YAxisUnlock()
    {
        SliceController(front, true);
        SliceController(back, true);
        SliceController(right, true);
        SliceController(left, true);
        SliceController(up, false);
        SliceController(down, false);

        highestSlider.SliderValue = yMax * 2;
        lowestSlider.SliderValue = (yMin + 0.5f) * 2;
    }

    public void ZAxisUnlock()
    {
        SliceController(front, false);
        SliceController(back, false);
        SliceController(right, true);
        SliceController(left, true);
        SliceController(up, true);
        SliceController(down, true);

        highestSlider.SliderValue = zMax * 2;
        lowestSlider.SliderValue = (zMin + 0.5f) * 2;
    }


    private void Update()
    {
        for (int i = 0; i < destroyItems.Count; i++)
        {
            bool xInRange = (destroyItems[i].transform.localPosition.x <= xMax) && (destroyItems[i].transform.localPosition.x >= xMin);
            bool yInRange = (destroyItems[i].transform.localPosition.y <= yMax) && (destroyItems[i].transform.localPosition.y >= yMin);
            bool zInRange = (destroyItems[i].transform.localPosition.z <= zMax) && (destroyItems[i].transform.localPosition.z >= zMin);


            if (yInRange && xInRange && zInRange)
            {
                destroyItems[i].SetActive(true);
                destroyItems.Remove(destroyItems[i]);
            }
        }
    }
    private void SliceController(Transform surface,bool lockmovement)
    {
        surface.GetComponent<PositionConstrain>().lockmovement = lockmovement;
        Color surfaceColor = surface.GetComponent<MeshRenderer>().material.color;
        Color slicingColor = new Color(surfaceColor.r, surfaceColor.g, surfaceColor.b, 0.7f);
        Color lockdownColor = new Color(surfaceColor.r, surfaceColor.g, surfaceColor.b, 0);

        surface.GetComponent<MeshRenderer>().material.color = lockmovement ? lockdownColor : slicingColor;
    }
}
