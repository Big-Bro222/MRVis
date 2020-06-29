using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit;
using UnityEngine.EventSystems;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class FocusObj : MonoBehaviour
{
    public GameObject Focus;
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

        if (prefocusobj == null)
        {
            prefocusobjName = "null";
        }
        else { prefocusobjName = prefocusobj.name; }
        if (pv.IsMine)
        {
            Debug.Log("Set" + focusobjName + " , " + prefocusobjName);
            RaiseSetFocus(focusobjName, prefocusobjName);
        }
        Focus = focusobj;
        if (Focus != null&& Focus.transform.GetChild(1))
        {
            Focus.transform.GetChild(1).GetComponent<NodeInteractionController>().OnHover(true);
        }

        if (prefocusobj != null&& prefocusobj.transform.GetChild(1))
        {
            prefocusobj.transform.GetChild(1).GetComponent<NodeInteractionController>().OnHover(false);
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
