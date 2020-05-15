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



        public bool isLock;
        public bool clipable;


        private int windowNum;


        public void Clip()
        {
            //if (CoreServices.InputSystem.GazeProvider.GazeTarget && CoreServices.InputSystem.GazeProvider.GazeTarget.transform.GetChild(0)== viewWindow.transform)
            if (CoreServices.InputSystem.GazeProvider.GazeTarget && clipable)
            {


                windowNum++;
                //GameObject newextrudeWindow = PhotonNetwork.Instantiate("ExtrudeWindow", new Vector3(0, 0, 0), Quaternion.identity);
                GameObject newextrudeWindow = Instantiate(extrudeWindow, currentGazeTarget.transform);
                newextrudeWindow.transform.localPosition = new Vector3(viewWindowIndicator.transform.localPosition.x, viewWindowIndicator.transform.localPosition.y, -0.2f);
                newextrudeWindow.name = "extrude window " + windowNum;
                newextrudeWindow.transform.parent = transform.parent;
                newextrudeWindow.transform.localScale = new Vector3(1, 1, 1);

                if (base.photonView.IsMine)
                {
                    RaiseInstantiateWallEvent(newextrudeWindow.name);
                    GameObject quad = newextrudeWindow.transform.Find("Quad").gameObject;
                    transform.GetComponent<PhotonSynChroManager>().AddsyncronizeObj(newextrudeWindow.name,quad);
                    //base.photonView.RPC("OnInstantiateInPC", RpcTarget.Others);
                    
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
            int ViewID = base.photonView.ViewID;
            object[] datas = new object[] { ViewID, name };
            PhotonNetwork.RaiseEvent(Global.EXTURDE_WINDOW_EVENT, datas, RaiseEventOptions.Default, SendOptions.SendReliable);
        }

        
        public void RaiseDestroyWallEvent(string name)
        {
            int ViewID = base.photonView.ViewID;
            object[] datas = new object[] { ViewID, name };
            PhotonNetwork.RaiseEvent(Global.DESTROY_WINDOW_EVENT, datas, RaiseEventOptions.Default, SendOptions.SendUnreliable);
        }

        public void ShowHierarchy(string name)
        {
            object[] datas = new object[] { name };
            PhotonNetwork.RaiseEvent(Global.SHOW_HIERARCHY, datas, RaiseEventOptions.Default, SendOptions.SendUnreliable);
        }

        public void HideHierarchy(string name)
        {
            object[] datas = new object[] { name };
            PhotonNetwork.RaiseEvent(Global.HIDE_HIERARCHY, datas, RaiseEventOptions.Default, SendOptions.SendUnreliable);
        }

        public void EnableWindowTransform(string name)
        {
            object[] datas = new object[] { name };
            PhotonNetwork.RaiseEvent(Global.ENABLE_WINDOW_TRANSFORM, datas, RaiseEventOptions.Default, SendOptions.SendUnreliable);
        }

        public void DisableWindowTransform(string name)
        {
            object[] datas = new object[] { name };
            PhotonNetwork.RaiseEvent(Global.DISABLE_WINDOW_TRANSFORM, datas, RaiseEventOptions.Default, SendOptions.SendUnreliable);
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
