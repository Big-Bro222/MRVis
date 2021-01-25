﻿
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NodeInteractionController : MonoBehaviour
{
    public GameObject node;
    //public InvisiableInteractableSlider labelSlider;
    //public InvisiableInteractableSlider HighlightSlider;
    //public GameObject highlightNode;

    private Maptask mapTask;
    private Color onhovercolor;
    public Color defaultcolor;
    private bool onHover;
    private Vector3 originalscale;
    private TextMeshPro namelable;
    private LabelMovementManager labelRotationHandler;

    private Vector3 nodestartLocation;
    private Vector3 labelstartLocation;

    // Start is called before the first frame update
    void Start()
    {
        //highlightNode.SetActive(false);
        mapTask = FindObjectOfType<Maptask>();
        node = transform.GetChild(0).gameObject;
        nodestartLocation = node.transform.localPosition;
        namelable = GetComponentInChildren<TextMeshPro>();

        //labelRotationHandler = namelable.transform.GetComponent<LabelMovementManager>();
        //labelRotationHandler.enabled = false;

        onHover = false;
        onhovercolor = Color.red;
        if(defaultcolor!=new Color(255, 217, 0, 0))
        {
            defaultcolor = node.GetComponent<MeshRenderer>().material.GetColor("_MainColor");
        }

        node.GetComponent<MeshRenderer>().material.SetColor("_MainColor", defaultcolor);

        originalscale = node.transform.localScale*1.2f;
        node.transform.localScale = originalscale;
    }

    private void Update()
    {
        //label position for customize mode

        //if (mapTaskController&&mapTaskController.taskState == MapTaskController.TaskState.Customize)
        //{
        //    namelable.gameObject.GetComponent<RectTransform>().localPosition = labelstartLocation + new Vector3(0, 0, -4 * labelSlider.SliderValue);
        //    node.transform.localPosition = nodestartLocation + new Vector3(0, 0, -4 * HighlightSlider.SliderValue);
        //}
        

    }

    public void OnHover(bool onHover)
    {
        NodeControl(onHover);
        if (mapTask.taskState == Maptask.TaskState.Fixedlabel)
        {
            return;
        }

        LabelControl(onHover);

        
    }

    private void NodeControl(bool onHover)
    {
        if (onHover)
        {
            //if(mapTaskController.taskState == MapTaskController.TaskState.InFront)
            //{
            //    node.transform.localPosition += new Vector3(0, 0, -20.0f);
            //    node.GetComponent<BoxCollider>().center -= new Vector3(0, 900.0f, 0);
            //}

                node.GetComponent<MeshRenderer>().material.shader = Shader.Find("Mixed Reality Toolkit/Standard");
                node.GetComponent<MeshRenderer>().material.SetColor("_Color", onhovercolor);
        }
        else
        {

            //if (mapTaskController.taskState == MapTaskController.TaskState.InFront)
            //{

            //    node.transform.localPosition += new Vector3(0, 0, 20.0f);
            //    node.GetComponent<BoxCollider>().center -= new Vector3(0, -900.0f, 0);
            //}

                node.GetComponent<MeshRenderer>().material.shader = Shader.Find("Custom/CliptestReverse");
                node.GetComponent<MeshRenderer>().material.SetColor("_MainColor", defaultcolor);


        }
        node.transform.localScale = onHover ? originalscale * 2.0f : originalscale;
    }

    private void LabelControl(bool onHover)
    {
        //namelable
        namelable.enabled = onHover;
        if (onHover)
        {
            if (mapTask.taskState == Maptask.TaskState.OnScreen)
            {
                namelable.gameObject.GetComponent<RectTransform>().localPosition += new Vector3(0, 2.0f, 0);
            }
            else if (mapTask.taskState == Maptask.TaskState.InFront)
            {
                namelable.gameObject.GetComponent<RectTransform>().localPosition += new Vector3(0, 2.0f, -20.0f);
            }
        }
        else
        {
            if (mapTask.taskState == Maptask.TaskState.OnScreen)
            {
                namelable.gameObject.GetComponent<RectTransform>().localPosition += new Vector3(0, -2.0f, 0);
            }
            else if (mapTask.taskState == Maptask.TaskState.InFront)
            {
                namelable.gameObject.GetComponent<RectTransform>().localPosition += new Vector3(0, -2.0f, 20.0f);
            }
        }
        namelable.fontSize = onHover ? 40 : 28;

        //labelRotationHandler.enabled = onHover ? true : false;
    }
}
