using ExitGames.Client.Photon;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralEventHandler : MonoBehaviour
{
    // the size of the clip window is determined by this window clip collider.
    public Transform WindowClip;
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
        if (obj.Code == Global.RESCALE_WINDOW)
        {
            // set up the window clip space in the Remote wall scene.
            object[] datas = (object[])obj.CustomData;
            float xScale = (float)datas[0];
            float yScale = (float)datas[1];
            WindowClip.transform.localScale = new Vector3(WindowClip.transform.localScale.x * xScale, WindowClip.transform.localScale.y * yScale, WindowClip.transform.localScale.z);
        }
    }

    }
