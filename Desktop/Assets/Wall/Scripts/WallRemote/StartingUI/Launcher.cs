using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
namespace WallRemote
{
    public class Launcher : MonoBehaviourPunCallbacks
    {

        public GameObject connectedScreen;
        public GameObject disconnectedScreen;
        public GameObject HomePage;

        public void OnClick_ConnectBtn()
        {
            HomePage.SetActive(false);
            PhotonNetwork.ConnectUsingSettings();
        }

        public override void OnConnectedToMaster()
        {
            PhotonNetwork.JoinLobby(TypedLobby.Default);
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            //if the connection are failed, this interface will occur. The disconnect
            //from room function is not implemented
            disconnectedScreen.SetActive(true);
        }

        public override void OnJoinedLobby()
        {
            //if the connection is in good condition, then jump to the lobby waiting for connect room or join room
            if (disconnectedScreen.activeSelf)
            {
                disconnectedScreen.SetActive(false);
            }
            connectedScreen.SetActive(true);
        }


    }

}
