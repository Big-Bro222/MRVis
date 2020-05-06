using Microsoft.MixedReality.Toolkit;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;

public class ViewWindowController : MonoBehaviour
{
    public GameObject viewWindowIndicator;
    public GameObject extrudeWindow;
    public GameObject quad;
    public GameObject currentGazeTarget;



    public bool isLock;
    public bool clipable;


    private int windowNum;

    public void Clip()
    {
        //if (CoreServices.InputSystem.GazeProvider.GazeTarget && CoreServices.InputSystem.GazeProvider.GazeTarget.transform.GetChild(0)== viewWindow.transform)
          if (CoreServices.InputSystem.GazeProvider.GazeTarget&&clipable)
            {
            windowNum++;
            GameObject newextrudeWindow=Instantiate(extrudeWindow, currentGazeTarget.transform);
            newextrudeWindow.transform.localPosition = new Vector3(viewWindowIndicator.transform.localPosition.x, viewWindowIndicator.transform.localPosition.y, -0.2f);
            newextrudeWindow.name = "extrude window " + windowNum;
            newextrudeWindow.transform.parent = transform.parent;
            newextrudeWindow.transform.localScale = new Vector3(1, 1, 1);

            newextrudeWindow.transform.Find("ViewWindow/Marker").SetParent(currentGazeTarget.transform.Find("Quad"));
            if (currentGazeTarget.transform.parent.GetComponent<ViewHandler>())
            {
                currentGazeTarget.transform.parent.GetComponent<ViewHandler>().ChildWindows.Add( newextrudeWindow);
                foreach(GameObject ParentWindow in currentGazeTarget.transform.parent.GetComponent<ViewHandler>().ParentWindowsLine)
                {
                    newextrudeWindow.GetComponent<ViewHandler>().ParentWindowsLine.Add(ParentWindow);
                }

                newextrudeWindow.GetComponent<ViewHandler>().ParentWindowsLine.Add(currentGazeTarget.transform.parent.gameObject);

            }
        }
    }


    private float[] viewWindowScale;
    void Start()
    {
        isLock = false;
        windowNum = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if (CoreServices.InputSystem.GazeProvider.GazeTarget != null&& CoreServices.InputSystem.GazeProvider.GazeTarget.CompareTag("ViewWindowQuad"))
        {

            currentGazeTarget = CoreServices.InputSystem.GazeProvider.GazeTarget;
            viewWindowIndicator = currentGazeTarget.transform.Find("ViewWindowIndicator").gameObject;
            //if (viewWindowMarker.GetComponent<FreezePosition>().islock)
            //{
            //    return;
            //}

            Vector3 hitPosLocal = currentGazeTarget.transform.InverseTransformPoint(CoreServices.InputSystem.GazeProvider.HitPosition);
            viewWindowIndicator.transform.localPosition = new Vector3(hitPosLocal.x, hitPosLocal.y, -0.001f);

        }



    }
}
