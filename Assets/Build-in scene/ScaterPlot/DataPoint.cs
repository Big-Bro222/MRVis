using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit;
using TMPro;
using System;

public class DataPoint : MonoBehaviour
{

    public float calories;
    public float protein ;
    public float fat ;
    public float sugars;

    public GameObject label;

    private Vector3 startScale;
    private int UpdateNum;
    private int delayNum;
   
    void Start()
    {
        startScale = transform.localScale;

        UpdateNum = Convert.ToInt32(0.08f / Time.deltaTime);
        delayNum = 0;

        label = transform.parent.parent.Find("Label").gameObject;

        if (calories < 100)
        {
            GetComponent<MeshRenderer>().material.color = Color.green;
        }else if(calories<140){
            GetComponent<MeshRenderer>().material.color = Color.yellow;
        }
        else
        {
            GetComponent<MeshRenderer>().material.color = Color.red;
        }
    }


    //public void OnSliderUpdatedGreen(SliderEventData eventData)
    //{
    //    TargetRenderer = GetComponentInChildren<Renderer>();
    //    if ((TargetRenderer != null) && (TargetRenderer.material != null))
    //    {
    //        TargetRenderer.material.color = new Color(TargetRenderer.sharedMaterial.color.r, eventData.NewValue, TargetRenderer.sharedMaterial.color.b);
    //    }
    //}

    void Update()
    {
        label.transform.rotation = Camera.main.transform.rotation;
        if (CoreServices.InputSystem.GazeProvider.GazeTarget == gameObject)
        {
            if (delayNum < UpdateNum)
            {
                delayNum++;
            }
            else
            {
                transform.localScale = startScale * 2;
                Vector3 LabelOffset = (Camera.main.transform.position - gameObject.transform.position).normalized * 0.1f;
                label.transform.position = transform.position + new Vector3(LabelOffset.x, LabelOffset.y + 0.2f + LabelOffset.z);
                label.GetComponentInChildren<TextMeshPro>().SetText(gameObject.name);
                label.GetComponent<LineRendererUpdate>().target = transform;
            }
        }
        else
        {
            delayNum = 0;
            transform.localScale = startScale;
        }
    }
}
