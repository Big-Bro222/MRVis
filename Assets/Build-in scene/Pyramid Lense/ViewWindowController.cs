using Microsoft.MixedReality.Toolkit;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;
using Photon.Pun;
using System;
using Photon.Realtime;
using ExitGames.Client.Photon;

namespace HoloLensPyrimad
{
    public class ViewWindowController : MonoBehaviourPun
    {

        public GameObject viewWindowIndicator;
        public GameObject extrudeWindow;
        public GameObject quad;
        public GameObject currentGazeTarget;
        public PhotonView pv;



        public bool isLock;
        public bool clipable;


        private int windowNum;
        private string visualizationName = "Pyrimad";

        public void Clip()
        {
            if (CoreServices.InputSystem.GazeProvider.GazeTarget && clipable)
            {


                windowNum++;
                GameObject newextrudeWindow = Instantiate(extrudeWindow, currentGazeTarget.transform);
                newextrudeWindow.transform.localPosition = new Vector3(viewWindowIndicator.transform.localPosition.x, viewWindowIndicator.transform.localPosition.y, -0.2f);
                newextrudeWindow.name = "extrude window " + windowNum;
                newextrudeWindow.transform.parent = transform.parent;
                newextrudeWindow.transform.localScale = new Vector3(1, 1, 1);

                if (pv.IsMine)
                {
                    RaiseInstantiateWallEvent(newextrudeWindow.name);
                    GameObject quad = newextrudeWindow.transform.Find("Quad").gameObject;
                    transform.GetComponent<PhotonSynChroManager>().AddsyncronizeObj(newextrudeWindow.name,quad);                    
                }


                newextrudeWindow.transform.Find("ViewWindow/Marker").SetParent(currentGazeTarget.transform);



                if (currentGazeTarget.transform.parent.GetComponent<ViewHandler>())
                {
                    currentGazeTarget.transform.parent.GetComponent<ViewHandler>().ChildWindows.Add(newextrudeWindow);
                    foreach (GameObject ParentWindow in currentGazeTarget.transform.parent.GetComponent<ViewHandler>().ParentWindowsLine)
                    {
                        newextrudeWindow.GetComponent<ViewHandler>().ParentWindowsLine.Add(ParentWindow);
                    }

                    newextrudeWindow.GetComponent<ViewHandler>().ParentWindowsLine.Add(currentGazeTarget.transform.parent.gameObject);

                }
            }
        }

        ////call instantiate function in Wall Scene
        private void RaiseInstantiateWallEvent(string name)
        {
            int ViewID = pv.ViewID;
            object[] datas = new object[] { name,"Instantiate GameObject", visualizationName };
            PhotonNetwork.RaiseEvent(Global.INSTANTIATE_EVENT, datas, RaiseEventOptions.Default, SendOptions.SendReliable);
        }

        
        public void RaiseDestroyWallEvent(string name)
        {
            int ViewID = pv.ViewID;
            object[] datas = new object[] { name,"Destroy GameObject",visualizationName };
            PhotonNetwork.RaiseEvent(Global.DESTROY_WINDOW_EVENT, datas, RaiseEventOptions.Default, SendOptions.SendUnreliable);
        }

        public void ShowHierarchy(string name)
        {
            object[] datas = new object[] { name, "ShowHierarchy", visualizationName };
            PhotonNetwork.RaiseEvent(Global.SCROLLVIEW_LOG_EVENT, datas, RaiseEventOptions.Default, SendOptions.SendUnreliable);
        }

        public void HideHierarchy(string name)
        {
            object[] datas = new object[] { name, "HideHierarchy" , visualizationName };
            PhotonNetwork.RaiseEvent(Global.SCROLLVIEW_LOG_EVENT, datas, RaiseEventOptions.Default, SendOptions.SendUnreliable);
        }

        public void EnableWindowTransform(string name)
        {
            object[] datas = new object[] { name, "EnableWindowTransform", visualizationName };
            PhotonNetwork.RaiseEvent(Global.SCROLLVIEW_LOG_EVENT, datas, RaiseEventOptions.Default, SendOptions.SendUnreliable);
        }

        public void DisableWindowTransform(string name)
        {
            object[] datas = new object[] { name, "DisableWindowTransform", visualizationName };

            PhotonNetwork.RaiseEvent(Global.SCROLLVIEW_LOG_EVENT, datas, RaiseEventOptions.Default, SendOptions.SendUnreliable);
        }


        //private float[] viewWindowScale;
        void Start()
        {
            isLock = false;
            windowNum = 0;
        }

        // Update is called once per frame
        void Update()
        {

            if (CoreServices.InputSystem.GazeProvider.GazeTarget != null && CoreServices.InputSystem.GazeProvider.GazeTarget.CompareTag("ViewWindowQuad"))
            {

                currentGazeTarget = CoreServices.InputSystem.GazeProvider.GazeTarget;
                viewWindowIndicator = currentGazeTarget.transform.Find("ViewWindowIndicator").gameObject;

                Vector3 hitPosLocal = currentGazeTarget.transform.InverseTransformPoint(CoreServices.InputSystem.GazeProvider.HitPosition);
                viewWindowIndicator.transform.localPosition = new Vector3(hitPosLocal.x, hitPosLocal.y, -0.001f);

            }



        }
    }



}
