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
            // Here is only on room created, multiple rooms can be also implemented
            PhotonNetwork.CreateRoom("DefaultRoom", new RoomOptions { MaxPlayers = 10 }, null);
            Debug.Log("Create Room!");
        }


        public override void OnJoinedRoom()
        {
            Debug.Log("Room Joined Sucess");
            //PhotonNetwork.LoadLevel(LoadScene);
            PhotonNetwork.LoadLevel(1);


        }

        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            Debug.LogError("RoomFailed" + returnCode + " Message " + message);
        }
    }
}
