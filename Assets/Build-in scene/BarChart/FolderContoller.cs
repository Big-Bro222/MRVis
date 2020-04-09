using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit;

public class FolderContoller : MonoBehaviour
{
    private Vector3 startPos;
    private Color defaultColor;
    private Color onHoverColor;
    private Color lineOnHoverColor;
    public bool isMoving;
    public bool forward;
    public bool isFocus;

    void Start()
    {
        isMoving = false;
        forward = false;
        isFocus = false;

        defaultColor = transform.GetChild(0).GetComponent<MeshRenderer>().material.color;
        onHoverColor = Color.yellow;
        lineOnHoverColor = Color.green;
        startPos = transform.localPosition; 
    }


    public void OnFocus()
    {
        isFocus = !isFocus;
        Debug.Log(isFocus);
        transform.GetChild(0).GetComponent<MeshRenderer>().material.color = isFocus ? onHoverColor : defaultColor;
        transform.GetChild(1).GetComponent<MeshRenderer>().material.color = isFocus ? lineOnHoverColor : Color.yellow;
    }

    // Update is called once per frame
    public void OnClick()
    {
        isMoving = true;
        forward = !forward;
    }

    void Update()
    {
       

        if (isMoving&&forward)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, startPos + new Vector3(0, 0, -1), 0.03f);
        }
        else if(isMoving&&!forward)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, startPos, 0.03f);
        }
        
        if(transform.localPosition==startPos||transform.localPosition == startPos + new Vector3(0, 0, -1))
        {
            isMoving = false;
        }
    }
}
