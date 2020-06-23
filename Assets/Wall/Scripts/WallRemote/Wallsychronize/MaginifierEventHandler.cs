using ExitGames.Client.Photon;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaginifierEventHandler : MonoBehaviour
{


    public ScaleHandler scaleHandler;
    // Start is called before the first frame update
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
            Debug.Log("Onscale change recieved in VR!");
            scaleHandler.OnScaleChange();
        }
        else if (obj.Code==Global.DESTROY_WINDOW_EVENT)
        {
            scaleHandler.SetLock();
        }
    }

}
