using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AnnotationManager : MonoBehaviour
{
    // Start is called before the first frame update
    private MapTaskController mapTaskController;
    private GameObject focusObj;
    private FocusObj focus;
    private GameObject title;
    private GameObject description;
    private GameObject price;
    private GameObject backPanel;
    private string descriptiontText;
    private string priceText;

    private bool isObservermode;

    [SerializeField]
    private InvisiableInteractableSlider annotationSlider;
    private float AnnotationDepth;

    //public bool isAnchored;
    public float panelFollowOffset;
    private Transform annotationOriginalParent;

    //public float Annotationdistance;

    void Start()
    {
        mapTaskController = FindObjectOfType<MapTaskController>();
        focus = FindObjectOfType<FocusObj>();
        title = gameObject.transform.Find("TextContent/Title").gameObject;
        description = gameObject.transform.Find("TextContent/Description").gameObject;
        backPanel = gameObject.transform.Find("Backpanel").gameObject;
        price = gameObject.transform.Find("TextContent/Price").gameObject;
        isObservermode = false;
        if (description.GetComponent<Text>())
        {
            descriptiontText = description.GetComponent<Text>().text;
        }
        else
        {
            descriptiontText = description.GetComponent<TextMeshPro>().text;
            isObservermode = true;
        }

        if (price.GetComponent<Text>())
        {
            priceText = price.GetComponent<Text>().text;
        }
        else
        {
            priceText = price.GetComponent<TextMeshPro>().text;
        }

        annotationOriginalParent = transform.parent;
        //isAnchored = true;
    }

    public void SetAnchor(bool anchor)
    {
        transform.parent = annotationOriginalParent;
    }

    // Update is called once per frame
    void Update()
    {
        if (isObservermode)
        {
            return;
        }
        if (focusObj != focus.GetFocus())
        {
            if (focus == null)
            {
                return;
            }
            focusObj = focus.GetFocus();
            if (focusObj == null)
            {
                return;
            }
            if (focusObj.GetComponent<Node>())
            {
                TextUpdate(focusObj.name, focusObj.GetComponent<Node>().id);
            }
        }
        TransformUpdate(focusObj);

    }

    private void TransformUpdate(GameObject focus)
    {
        if (!focus)
        {
            return;
        }
        transform.parent = focus.transform;
        Vector3 startPos = transform.localPosition;
        if (mapTaskController.taskState == MapTaskController.TaskState.OnScreen)
        {
            transform.localPosition = Vector3.Lerp(startPos, new Vector3(0, 0 - panelFollowOffset, -1.5f), 0.02f);
        }else if(mapTaskController.taskState == MapTaskController.TaskState.InFront)
        {
            transform.localPosition = Vector3.Lerp(startPos, new Vector3(0, 0 - panelFollowOffset, -10.0f), 0.02f);
        }else if (mapTaskController.taskState == MapTaskController.TaskState.Customize)
        {
            transform.localPosition = Vector3.Lerp(startPos, new Vector3(0, 0 - panelFollowOffset, AnnotationDepth), 0.02f);
        }
        transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
    }

    public void UpdateAnnotationDepth()
    {
        AnnotationDepth = -annotationSlider.SliderValue*10;
    }

    private void TextUpdate(string name,string hotelPrice)
    {
        //Update the text on Annotation
        if (isObservermode)
        {
            title.GetComponent<TextMeshPro>().text = "";
            description.GetComponent<TextMeshPro>().text = "";
            price.GetComponent<TextMeshPro>().text = priceText + " " + hotelPrice;
        }
        else
        {
            title.GetComponent<Text>().text = "";
            description.GetComponent<Text>().text = "";
            price.GetComponent<Text>().text = priceText + " " + hotelPrice;
        }


    }
}
