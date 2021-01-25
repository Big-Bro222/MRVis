using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using UnityEngine.UI;
using TMPro;
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


    //public void SliderValueUpdate()
    //{
        
    //    float sliderscale = nodeParent.SliderValue;
    //    foreach (TextMeshPro label in labels)
    //    {
    //        Vector3 labeltransform = label.GetComponent<RectTransform>().localPosition;
    //        label.GetComponent<RectTransform>().localPosition = new Vector3(labeltransform.x, labeltransform.y, -unit * sliderscale*0.1F);
    //    }
    //}

}

