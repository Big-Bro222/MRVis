using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using ExitGames.Client.Photon;

namespace HoloLensinterface
{
    public class EventHandler : MonoBehaviourPun
    {
        public photonviewController photonviewController;
        private void OnEnable()
        {
            PhotonNetwork.NetworkingClient.EventReceived += NetworkingClient_EventReceived;
        }
        private void OnDisable()
        {
            PhotonNetwork.NetworkingClient.EventReceived -= NetworkingClient_EventReceived;
        }


        [PunRPC]
        void RPC_LoadNextVis(string visualizationnameTobeSet)
        {
            photonviewController.setVisualization(visualizationnameTobeSet);
            Debug.Log("message recieved!");
        }

        private void NetworkingClient_EventReceived(EventData obj)
        {
            if (obj.Code == Global.LOAD_LEVEL_EVENT)
            {
                object[] datas = (object[])obj.CustomData;

                string visualizationName = (string)datas[0];
                string visualization = (string)datas[1];
                photonviewController.setVisualization(visualizationName);
            }
        }
        }
}
