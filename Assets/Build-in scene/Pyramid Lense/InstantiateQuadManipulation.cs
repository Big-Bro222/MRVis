using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloLensPyrimad;

public class InstantiateQuadManipulation : MonoBehaviour
{
    private ViewWindowController viewWindowController;
    BoundingBox boundingBox;
    ManipulationHandler manipulationHandler;

    void Start()
    {
        viewWindowController = viewWindowController = gameObject.transform.parent.parent.GetComponentInChildren<ViewWindowController>();
        boundingBox = GetComponent<BoundingBox>();
        manipulationHandler = GetComponent<ManipulationHandler>();
        boundingBox.enabled = true;
        manipulationHandler.enabled = true;
    }

    public void AdjustHandler(bool adjust)
    {
        boundingBox.enabled = adjust;
        manipulationHandler.enabled = adjust;
        if (adjust)
        {
            viewWindowController.EnableWindowTransform(transform.parent.name);
        }
        else
        {
            viewWindowController.DisableWindowTransform(transform.parent.name);
        }
    }

}
