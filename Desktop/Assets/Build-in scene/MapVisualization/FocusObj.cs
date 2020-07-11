using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using TMPro;

public class FocusObj : MonoBehaviour
{
    public GameObject Focus;
    public Transform FixedAnnotation;
    //private AudioSource hoverSFX;
    private LayerMask[] laymasks;
    private PointerEventData pointeventdata;
    public PhotonView pv;

    void Start()
    {
        //hoverSFX = GetComponent<AudioSource>();
    }
    public void SetFocus(GameObject focusobj,GameObject prefocusobj)
    {
        string focusobjName = "";
        string prefocusobjName = "";
        if (focusobj == null)
        {
            focusobjName = "null";
        }
        else { focusobjName = focusobj.name; }

        //if (focusobjName.Equals("MapShrink"))
        //{
        //    return;
        //}

        if (prefocusobj == null)
        {
            prefocusobjName = "null";
        }
        else { prefocusobjName = prefocusobj.name; }

        if (focusobj.GetComponent<Node>())
        {
            if (FixedAnnotation)
            {
                FixedAnnotation.Find("Cube/Description").GetComponent<TextMeshPro>().text = focusobjName;
                FixedAnnotation.Find("Cube/price").GetComponent<TextMeshPro>().text = "€ " + focusobj.GetComponent<Node>().id;
            }
        }
        else
        {
            Debug.Log("false" + focusobj.name);
        }


        if (pv.IsMine)
        {
            if (focusobjName != "MapShrink" && prefocusobjName != "MapShrink")
            {
                RaiseSetFocus(focusobjName, prefocusobjName);
            }
        }
        Focus = focusobj;
        if (Focus != null&& Focus.transform.GetChild(1))
        {
            if (Focus.transform.GetComponent<NodeInteractionController>())
            {
                Focus.transform.GetComponent<NodeInteractionController>().OnHover(true);
            }
        }

        if (prefocusobj != null&& prefocusobj.transform.GetChild(1))
        {
            if (prefocusobj.transform.GetComponent<NodeInteractionController>())
            {
                prefocusobj.transform.GetComponent<NodeInteractionController>().OnHover(false);
            }
        }
    }

    private void RaiseSetFocus(string focusobjName, string prefocusobjName)
    {
        object[] datas = new object[] { focusobjName, prefocusobjName, "Map" };
        PhotonNetwork.RaiseEvent(Global.SET_FOCUS, datas, RaiseEventOptions.Default, SendOptions.SendReliable);
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
