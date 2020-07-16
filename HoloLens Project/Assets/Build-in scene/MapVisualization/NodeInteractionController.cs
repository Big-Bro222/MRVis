using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit;
using TMPro;

public class NodeInteractionController : MonoBehaviour
{

    public GameObject node;
    public InvisiableInteractableSlider labelSlider;
    public InvisiableInteractableSlider HighlightSlider;
    //public GameObject highlightNode;

    private MapTaskController mapTaskController;
    private Color onhovercolor;
    public Color defaultcolor;
    private bool onHover;
    private Vector3 originalscale;
    private TextMeshPro namelable;
    private LabelMovementManager labelRotationHandler;

    private Vector3 nodestartLocation;
    private Vector3 labelstartLocation;
    private Vector3 CameraToNameLabel;
    private Vector3 CameraToNode;
    // Start is called before the first frame update
    void Start()
    {
        //highlightNode.SetActive(false);
        mapTaskController = FindObjectOfType<MapTaskController>();
        node = transform.GetChild(0).gameObject;
        nodestartLocation = node.transform.localPosition;
        namelable = GetComponentInChildren<TextMeshPro>();

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

        if (mapTaskController&&mapTaskController.taskState == MapTaskController.TaskState.Customize)
        {
            namelable.gameObject.GetComponent<RectTransform>().localPosition = labelstartLocation + transform.InverseTransformVector(-CoreServices.InputSystem.GazeProvider.GazeDirection * labelSlider.SliderValue);
            //node.transform.localPosition = nodestartLocation + new Vector3(0, 0, -4 * HighlightSlider.SliderValue);
            //node.transform.localPosition = nodestartLocation + transform.InverseTransformVector(-CoreServices.InputSystem.GazeProvider.GazeDirection * HighlightSlider.SliderValue);

        }


    }

    public void OnHover(bool onHover)
    {
        NodeControl(onHover);
        if (mapTaskController&&mapTaskController.taskState == MapTaskController.TaskState.Fixedlabel)
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
            CameraToNode = transform.InverseTransformVector(-CoreServices.InputSystem.GazeProvider.GazeDirection * 0.1f);
            if (mapTaskController.taskState == MapTaskController.TaskState.InFront)
            {
                node.transform.localPosition += CameraToNode;
            }
            else if(mapTaskController.taskState == MapTaskController.TaskState.Customize)
            {
                node.transform.localPosition += CameraToNode* HighlightSlider.SliderValue;
            }
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
            if (mapTaskController.taskState == MapTaskController.TaskState.InFront)
            {
                node.transform.localPosition -= CameraToNode;
            }else if(mapTaskController.taskState == MapTaskController.TaskState.Customize)
            {
                node.transform.localPosition -= CameraToNode * HighlightSlider.SliderValue;
            }

        }
        node.transform.localScale = onHover ? originalscale * 2.0f : originalscale;
    }

    private void LabelControl(bool onHover)
    {
        if (mapTaskController == null)
        {
            return;
        }
        //namelable
        namelable.enabled = onHover;
        if (onHover)
        {
            CameraToNameLabel = transform.InverseTransformVector(-CoreServices.InputSystem.GazeProvider.GazeDirection * 0.2f);
            if (mapTaskController.taskState == MapTaskController.TaskState.OnScreen)
            {
                CameraToNameLabel = transform.InverseTransformVector(-CoreServices.InputSystem.GazeProvider.GazeDirection * 0.2f);
                namelable.gameObject.GetComponent<RectTransform>().localPosition += new Vector3(0, 2.0f, 0);
            }
            else if (mapTaskController.taskState == MapTaskController.TaskState.InFront)
            {
                CameraToNameLabel = transform.InverseTransformVector(-CoreServices.InputSystem.GazeProvider.GazeDirection * 0.2f);
                namelable.gameObject.GetComponent<RectTransform>().localPosition += CameraToNameLabel;
            }
        }
        else
        {
            if (mapTaskController.taskState == MapTaskController.TaskState.OnScreen)
            {
                namelable.gameObject.GetComponent<RectTransform>().localPosition += new Vector3(0, -2.0f, 0);
            }
            else if (mapTaskController.taskState == MapTaskController.TaskState.InFront)
            {
                namelable.gameObject.GetComponent<RectTransform>().localPosition -= CameraToNameLabel;
            }
        }
        namelable.fontSize = onHover ? 40 : 28;

        //labelRotationHandler.enabled = onHover ? true : false;
    }
}
