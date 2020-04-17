using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    }

    public void YAxisUnlock()
    {
        SliceController(front, true);
        SliceController(back, true);
        SliceController(right, true);
        SliceController(left, true);
        SliceController(up, false);
        SliceController(down, false);
    }

    public void ZAxisUnlock()
    {
        SliceController(front, false);
        SliceController(back, false);
        SliceController(right, true);
        SliceController(left, true);
        SliceController(up, true);
        SliceController(down, true);
    }


    private void Update()
    {
        foreach(GameObject destroyitem in destroyItems)
        {
            bool xInRange = (destroyitem.transform.localPosition.x <= xMax) && (destroyitem.transform.localPosition.x >= xMin);
            bool yInRange = (destroyitem.transform.localPosition.y <= yMax) && (destroyitem.transform.localPosition.y >= yMin);
            bool zInRange = (destroyitem.transform.localPosition.z >= zMax) && (destroyitem.transform.localPosition.z <= zMin);


            if (yInRange&&xInRange&&zInRange)
            {
                destroyitem.SetActive(true);
                destroyItems.Remove(destroyitem);
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
