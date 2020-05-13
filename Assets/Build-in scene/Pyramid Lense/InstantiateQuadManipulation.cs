using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateQuadManipulation : MonoBehaviour
{

    BoundingBox boundingBox;
    ManipulationHandler manipulationHandler;

    void Start()
    {
        boundingBox = GetComponent<BoundingBox>();
        manipulationHandler = GetComponent<ManipulationHandler>();
        boundingBox.enabled = true;
        manipulationHandler.enabled = true;
    }

    public void AdjustHandler(bool adjust)
    {
        boundingBox.enabled = adjust;
        manipulationHandler.enabled = adjust;
    }

}
