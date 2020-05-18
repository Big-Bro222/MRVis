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
            if (obj.Code == Global.INSTANTIATE_EVENT)
            {
                object[] datas = (object[])obj.CustomData;

                string objectname = (string)datas[0];
                string eventname = (string)datas[1];
                string visualization = (string)datas[2];
                InstantiateGameObject(objectname, visualization);
                scrollViewLogUpdate(objectname, eventname);
            }
            else if (obj.Code == Global.DESTROY_WINDOW_EVENT)
            {
                object[] datas = (object[])obj.CustomData;

                string objectname = (string)datas[0];
                string eventname = (string)datas[1];
                string visualization = (string)datas[2];
                DestroyWindow(objectname, visualization);
                scrollViewLogUpdate(objectname, eventname);
            }
            else if (obj.Code == Global.SCROLLVIEW_LOG_EVENT)
            {
                object[] datas = (object[])obj.CustomData;

                string objectname = (string)datas[0];
                string eventname = (string)datas[1];
                string visualization = (string)datas[2];
                scrollViewLogUpdate(objectname, eventname);
            }

        }

        private void scrollViewLogUpdate(string name, string eventname)
        {
            scrollBehaviorHandler.TextEventReciever(name + " acting event: " + eventname);
        }



        private void DestroyWindow(string GameObjectName, string visualizationName)
        {
            GameObject DestroyObject = transform.parent.Find(GameObjectName).gameObject;
            Destroy(DestroyObject.GetComponentInChildren<ScaleUpdater>().viewWindowMarker);
            Destroy(DestroyObject);
            GetComponent<PhotonSynChroManager>().RemovesyncronizeObj(GameObjectName);
        }

        private void InstantiateGameObject(string windowName, string visualizationName)
        {
            GameObject newextrudeWindow = Instantiate(ExtrudeWindow, cameraRaycastManager.currentGazeGameObject.transform);
            newextrudeWindow.transform.localPosition = new Vector3(cameraRaycastManager.currnetRelativeHitPoint.x, cameraRaycastManager.currnetRelativeHitPoint.y, -0.2f);
            newextrudeWindow.name = windowName;
            newextrudeWindow.transform.parent = transform.parent;
            newextrudeWindow.transform.localScale = new Vector3(1, 1, 1);
            newextrudeWindow.transform.Find("ViewWindow/Marker").SetParent(cameraRaycastManager.currentGazeGameObject.transform);
            GetComponent<PhotonSynChroManager>().AddsyncronizeObj(windowName, newextrudeWindow.transform.Find("Quad").gameObject);
        }


    }
}

