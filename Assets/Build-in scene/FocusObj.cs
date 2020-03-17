using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit;
using UnityEngine.EventSystems;

public class FocusObj : MonoBehaviour
{
    public GameObject Focus;
    private AudioSource hoverSFX;
    private LayerMask[] laymasks;
    private PointerEventData pointeventdata;


    void Start()
    {
        hoverSFX = GetComponent<AudioSource>();
    }
    public void setFocus(GameObject focusobj,GameObject prefocusobj)
    {
            hoverSFX.Play();
            Focus = focusobj;
        if (focusobj.name == "PinchSlider")
        {
            return;
        }
             
        if(prefocusobj != null ||Focus != null)
        {
            if (prefocusobj == null)
            {
                Focus.GetComponent<AirTapTest>().OnHover(true);
            }
            else if (Focus == null)
            {
                prefocusobj.GetComponent<AirTapTest>().OnHover(false);
            }
            else
            {
                prefocusobj.GetComponent<AirTapTest>().OnHover(false);
                Focus.GetComponent<AirTapTest>().OnHover(true);
            }
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
