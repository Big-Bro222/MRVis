using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using ExitGames.Client.Photon;
using System;

namespace WallRemote
{
    public class InstantiateController : MonoBehaviourPun
    {

        public GameObject ExtrudeWindow;


        private byte EXTURDE_WINDOW_EVENT = 0;
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
            Debug.Log("recieveEventWall");
            if (obj.Code == EXTURDE_WINDOW_EVENT)
            {
                object[] datas = (object[])obj.CustomData;
                int ViewID = (int)datas[0];
                string windowName = (string)datas[1];
                Debug.Log(ViewID + " , " + windowName);

                GameObject newextrudeWindow = Instantiate(ExtrudeWindow,transform.parent);

                //GameObject newextrudeWindow = PhotonNetwork.Instantiate("ExtrudeWindow", new Vector3(0, 0, 0), Quaternion.identity);
                newextrudeWindow.name = windowName;
                GetComponent<PhotonSynChroManager>().syncronizeObjs.Add(newextrudeWindow);

                if (ViewID == base.photonView.ViewID)
                {
                    Debug.Log("View ID equals");
                }
            }
        }



    }
}

