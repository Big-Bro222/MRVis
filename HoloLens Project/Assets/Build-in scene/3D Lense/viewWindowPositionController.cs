using Microsoft.MixedReality.Toolkit;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;

public class viewWindowPositionController : MonoBehaviour
{
    public GameObject ChartLense;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CoreServices.InputSystem.GazeProvider.GazeTarget != null && CoreServices.InputSystem.GazeProvider.GazeTarget==gameObject)
        {


            Vector3 hitPosLocal =transform.InverseTransformPoint(CoreServices.InputSystem.GazeProvider.HitPosition);
            ChartLense.transform.localPosition = new Vector3(hitPosLocal.x, hitPosLocal.y, 0);

        }
    }
}
