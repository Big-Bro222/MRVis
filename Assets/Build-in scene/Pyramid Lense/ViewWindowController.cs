using Microsoft.MixedReality.Toolkit;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;

public class ViewWindowController : MonoBehaviour
{
    public GameObject viewWindow;
    public GameObject quad;
    public GameObject viewWindowIndicator;



    public bool isLock;


    private float[] viewWindowScale;
    void Start()
    {
        isLock = false;
    }

    public void Lock(bool Lock)
    {
        isLock = Lock;
    }

    // Update is called once per frame
    void Update()
    {
        if (isLock)
        {
            return;
        }

        if (CoreServices.InputSystem.GazeProvider.GazeTarget != null&& CoreServices.InputSystem.GazeProvider.GazeTarget.CompareTag("ViewWindowQuad"))
        {



            viewWindow = CoreServices.InputSystem.GazeProvider.GazeTarget.transform.Find("ViewWindow").gameObject;

            Vector3 hitPosLocal = viewWindow.transform.parent.InverseTransformPoint(CoreServices.InputSystem.GazeProvider.HitPosition);
            Debug.Log(hitPosLocal);
            viewWindow.transform.localPosition = new Vector3(hitPosLocal.x, hitPosLocal.y, -0.001f);

        }



    }
}
