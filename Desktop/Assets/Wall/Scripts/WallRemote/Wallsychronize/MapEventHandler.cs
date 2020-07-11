using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using ExitGames.Client.Photon;
using System;

public class MapEventHandler : MonoBehaviour
{
    public Transform nodeparent;
    public FocusObj focusObj;
    private GameObject focusGameobj;
    private GameObject prefocusGameobj;
    public Transform AnnotationPanel;
    public Transform MetroLineCollectionBoth;
    public Maptask mapTask;
    public TaskUpdate taskUpdate;

    // Start is called before the first frame update
    private void OnEnable()
    {
        for (int i = 0; i < MetroLineCollectionBoth.childCount; i++)
        {
            MetroLineCollectionBoth.GetChild(i).gameObject.SetActive(false);
        }
        PhotonNetwork.NetworkingClient.EventReceived += NetworkingClient_EventReceived;
    }
    private void OnDisable()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= NetworkingClient_EventReceived;
    }

    private void NetworkingClient_EventReceived(EventData obj)
    {
        if (obj.Code == Global.SET_FOCUS)
        {
            //set focus gameobject
            object[] datas = (object[])obj.CustomData;

            string focusobjName = (string)datas[0];
            string prefocusobjName = (string)datas[1];
            string visualization = (string)datas[2];
            SetFocus(focusobjName, prefocusobjName);
        }
        else if (obj.Code == Global.INSTANTIATE_EVENT)
        {

            //set select metro
            object[] datas = (object[])obj.CustomData;

            bool Onselect = (bool)datas[0];
            string MetrolineName = (string)datas[1];
            string visualization = (string)datas[2];

            SetMetroline(Onselect, MetrolineName);
        }
        else if (obj.Code == Global.SCALE)
        {
            object[] datas = (object[])obj.CustomData;
            float PanX = (float)datas[0];
            float PanY = (float)datas[1];
            if (AnnotationPanel == null)
            {
                MapPan(PanX, PanY);
            }
        }
        else if (obj.Code == Global.NEXT_TASK)
        {
            object[] datas = (object[])obj.CustomData;
            string TaskName = (string)datas[0];
            mapTask.NextTask(TaskName);
            taskUpdate.Nexttask(TaskName);
        }
    }

    private void MapPan(float PanX, float PanY)
    {
        nodeparent.parent.localPosition += new Vector3(PanX,PanY,0);
        Debug.Log("Pan!!!");
    }

    private void SetMetroline(bool Onselect,string MetrolineMame)
    {
        for(int i=0;i< MetroLineCollectionBoth.childCount; i++)
        {
            MetroLineCollectionBoth.GetChild(i).gameObject.SetActive(false);
        }

        if (Onselect)
        {
            MetroLineCollectionBoth.Find(MetrolineMame).gameObject.SetActive(true);
        }
    }

    private void SetFocus(string focusobjName,string prefocusobjName)
    {
        if (focusobjName.Equals("null"))
        {
            focusGameobj = null;
        }
        else
        {
            focusGameobj = nodeparent.Find(focusobjName).gameObject;
        }

        if (prefocusobjName.Equals("null"))
        {
            prefocusGameobj = null;
        }
        else
        {
            prefocusGameobj = nodeparent.Find(prefocusobjName).gameObject;
        }

        AnnotationPanel.SetParent(focusGameobj.transform);
        focusObj.SetFocus(focusGameobj, prefocusGameobj);
    }
}
