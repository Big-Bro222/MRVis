using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Microsoft.MixedReality.Toolkit.UI;

public class LableController : MonoBehaviour
{
    //public bool showLabels;

    public GameObject nodeParent;
    private TextMeshPro[] labels;
    private int scale;
    private Vector3[] sliderstartpos;
    private int unit;

    private void Start()
    {
        labels = nodeParent.GetComponentsInChildren<TextMeshPro>();
        unit = 72;
        //if (!showLabels)
        //{
        //    foreach (Canvas canvas in canvases)
        //    {
        //        canvas.gameObject.SetActive(false);
        //    }
        //}
    }

}

