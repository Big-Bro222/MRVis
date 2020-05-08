using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace WallRemote
{
    public class UIHandler : MonoBehaviourPunCallbacks
    {
        public void OnClick_JoinRoom()
        {
            PhotonNetwork.JoinRoom("DefaultRoom", null);
            Debug.Log("Joining...");
        }


        public void OnClick_CreateRoom()
        {
            PhotonNetwork.CreateRoom("DefaultRoom", new RoomOptions { MaxPlayers = 4 }, null);
            Debug.Log("Create");
        }


        public override void OnJoinedRoom()
        {
            Debug.Log("Room Joined Sucess");
            PhotonNetwork.LoadLevel("BaseWallRemote");


        }

        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            Debug.LogError("RoomFailed" + returnCode + " Message " + message);
        }
    }
}
