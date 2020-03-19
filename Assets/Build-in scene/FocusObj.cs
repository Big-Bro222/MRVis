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
    public void setFocus(GameObject focusobj,GameObject prefocusobj)
    {
        Focus = focusobj;
        if (Focus != null)
        {
            Focus.transform.GetChild(0).GetComponent<AirTapTest>().OnHover(true);
        }

        if (prefocusobj != null)
        {
            prefocusobj.transform.GetChild(0).GetComponent<AirTapTest>().OnHover(false);
        }
        

        //if (focusobj && focusobj.name == "Wall")
        //{
        //    Focus = null;
        //}
        //else
        //{
        //    Focus = focusobj;
        //}
        ////hoverSFX.Play();


        //if (prefocusobj != null ||Focus != null)
        //{
        //    if (prefocusobj == null)
        //    {
        //        Focus.GetComponentInChildren<AirTapTest>().OnHover(true);
        //    }
        //    else if (Focus == null)
        //    {
        //        prefocusobj.GetComponentInChildren<AirTapTest>().OnHover(false);
        //    }
        //    else
        //    {
        //        prefocusobj.GetComponentInChildren<AirTapTest>().OnHover(false);
        //        Focus.GetComponentInChildren<AirTapTest>().OnHover(true);
        //    }
        //}

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
