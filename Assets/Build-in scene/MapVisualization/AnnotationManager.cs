using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnnotationManager : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject focusObj;
    private FocusObj focus;
    private GameObject title;
    private GameObject description;
    private GameObject backPanel;
    private string descriptiontText;

    public bool isAnchored;
    public float panelFollowOffset;
    private Transform annotationOriginalParent;


    void Start()
    {
        focus = FindObjectOfType<FocusObj>();
        title = gameObject.transform.Find("TextContent/Title").gameObject;
        description = gameObject.transform.Find("TextContent/Description").gameObject;
        backPanel = gameObject.transform.Find("Backpanel").gameObject;
        descriptiontText = description.GetComponent<Text>().text;
        annotationOriginalParent = transform.parent;
        isAnchored = true;
    }

    public void SetAnchor(bool anchor)
    {
        isAnchored = anchor;
        if (!isAnchored)
        {
            transform.parent = annotationOriginalParent;
        }
    }

    // Update is called once per frame
    void Update()
    {
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
            TextUpdate(focusObj.name);
        }
        if (isAnchored)
        {
            TransformUpdate(focusObj);
        }
    }

    private void TransformUpdate(GameObject focus)
    {
        if (!focus)
        {
            return;
        }
        transform.parent = focus.transform;
        Vector3 startPos = transform.localPosition;
        transform.localPosition=Vector3.Lerp(startPos, new Vector3(0, 0- panelFollowOffset, -1.5f), 0.02f);
        transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
    }

    private void TextUpdate(string name)
    {
        title.GetComponent<Text>().text = name;
        description.GetComponent<Text>().text = name+descriptiontText;
    }
}
