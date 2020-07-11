using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Microsoft.MixedReality.Toolkit;
using TMPro;
using System;

public class DataPointInteraction : MonoBehaviour
{

    public GameObject Panel;

    private Vector3 startScale;
    private int UpdateNum;
    private int delayNum;
    private DataPoint dataPoint;

    void Start()
    {
        startScale = transform.localScale;
        dataPoint = GetComponent<DataPoint>();
        UpdateNum = Convert.ToInt32(0.08f / Time.deltaTime);
        delayNum = 0;

        Panel = transform.parent.parent.Find("InfoPanel").gameObject;

        if (dataPoint.calories < 100)
        {
            GetComponent<MeshRenderer>().material.color = Color.green;
        }
        else if (dataPoint.calories < 140)
        {
            GetComponent<MeshRenderer>().material.color = Color.yellow;
        }
        else
        {
            GetComponent<MeshRenderer>().material.color = Color.red;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (CoreServices.InputSystem.GazeProvider.GazeTarget == gameObject)
        //{
        //    if (delayNum < UpdateNum)
        //    {
        //        delayNum++;
        //    }
        //    else
        //    {
        //        transform.localScale = startScale * 2;
        //        Vector3 LabelOffset = (Camera.main.transform.position - gameObject.transform.position).normalized * 0.1f;
        //        Panel.transform.position = transform.position + new Vector3(LabelOffset.x, LabelOffset.y + 0.2f + LabelOffset.z);
        //        Panel.GetComponentInChildren<TextMeshPro>().SetText(gameObject.name);
        //        Panel.GetComponent<LineRendererUpdate>().target = transform;
        //    }
        //}
        //else
        //{
        //    delayNum = 0;
        //    transform.localScale = startScale;
        //}
    }
}
