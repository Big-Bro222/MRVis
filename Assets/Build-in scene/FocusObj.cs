using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit;
using UnityEngine.EventSystems;

public class FocusObj : MonoBehaviour
{
    public GameObject Focus;
    //private AudioSource hoverSFX;
    private LayerMask[] laymasks;
    private PointerEventData pointeventdata;


    void Start()
    {
        //hoverSFX = GetComponent<AudioSource>();
    }
    public void SetFocus(GameObject focusobj,GameObject prefocusobj)
    {
        Focus = focusobj;
        if (Focus != null)
        {
            Debug.Log("the focus is " + Focus.name);
            Focus.transform.GetChild(0).GetComponent<AirTapTest>().OnHover(true);
        }

        if (prefocusobj != null)
        {
            prefocusobj.transform.GetChild(0).GetComponent<AirTapTest>().OnHover(false);
        }
    }

    public GameObject GetFocus()
    {
        return Focus;
    }
    // Update is called once per frame
    void Update()
    {


    }
}
