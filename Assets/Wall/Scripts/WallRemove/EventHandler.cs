using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using ExitGames.Client.Photon;
using System;

namespace WallRemote
{
    public class EventHandler : MonoBehaviourPun
    {
        public ScrollBehaviorHandler scrollBehaviorHandler;
        public GameObject ExtrudeWindow;
        public CameraRaycastManager cameraRaycastManager;
        private void OnEnable()
        {
            PhotonNetwork.NetworkingClient.EventReceived += NetworkingClient_EventReceived;
        }
        private void OnDisable()
        {
            PhotonNetwork.NetworkingClient.EventReceived -= NetworkingClient_EventReceived;
        }

        private void NetworkingClient_EventReceived(EventData obj)
        {
            if (obj.Code == Global.EXTURDE_WINDOW_EVENT)
            {
                Extrude(obj);
            }
            else if (obj.Code == Global.DESTROY_WINDOW_EVENT)
            {
                DestoryWindow(obj);
            }
            else if (obj.Code == Global.ENABLE_WINDOW_TRANSFORM)
            {
                object[] datas = (object[])obj.CustomData;
                string windowName = (string)datas[0];
                scrollBehaviorHandler.TextEventReciever(windowName + " transform enabled");
            }
            else if (obj.Code == Global.DISABLE_WINDOW_TRANSFORM)
            {
                object[] datas = (object[])obj.CustomData;
                string windowName = (string)datas[0];
                scrollBehaviorHandler.TextEventReciever(windowName + " transform disabled");
            }
            else if (obj.Code == Global.SHOW_HIERARCHY)
            {
                object[] datas = (object[])obj.CustomData;
                string windowName = (string)datas[0];
                scrollBehaviorHandler.TextEventReciever(windowName + " show hierarchy");
            }
            else if (obj.Code == Global.HIDE_HIERARCHY)
            {
                object[] datas = (object[])obj.CustomData;
                string windowName = (string)datas[0];
                scrollBehaviorHandler.TextEventReciever(windowName + " hide hierarchy");
            }
        }

        private void DestoryWindow(EventData obj)
        {
            
            object[] datas = (object[])obj.CustomData;
            int ViewID = (int)datas[0];
            string windowName = (string)datas[1];

            Debug.Log(windowName);
            GameObject DestroyObject = transform.parent.Find(windowName).gameObject;
            Destroy(DestroyObject.GetComponentInChildren<ScaleUpdater>().viewWindowMarker);
            Destroy(DestroyObject);
            GetComponent<PhotonSynChroManager>().RemovesyncronizeObj(windowName);
            //Destroy(DestroyObject.transform.parent.gameObject);
            scrollBehaviorHandler.TextEventReciever(windowName+" is Destroyed");
        }

        private void Extrude(EventData obj)
        {
            object[] datas = (object[])obj.CustomData;
            int ViewID = (int)datas[0];
            string windowName = (string)datas[1];

            GameObject newextrudeWindow = Instantiate(ExtrudeWindow, cameraRaycastManager.currentGazeGameObject.transform);
            newextrudeWindow.transform.localPosition = new Vector3(cameraRaycastManager.currnetRelativeHitPoint.x, cameraRaycastManager.currnetRelativeHitPoint.y, -0.2f);
            newextrudeWindow.name = windowName;
            newextrudeWindow.transform.parent = transform.parent;
            newextrudeWindow.transform.localScale = new Vector3(1, 1, 1);

            newextrudeWindow.transform.Find("ViewWindow/Marker").SetParent(cameraRaycastManager.currentGazeGameObject.transform);

            GetComponent<PhotonSynChroManager>().AddsyncronizeObj(windowName, newextrudeWindow.transform.Find("Quad").gameObject);

            scrollBehaviorHandler.TextEventReciever("Extrude new window "+windowName);

        }
    }
}

