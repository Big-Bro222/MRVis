using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using ExitGames.Client.Photon;

public class MapHololensEventHandler : MonoBehaviour
{
    public List<GameObject> visableObjects;
    [SerializeField]
    private GameObject InformationPannel;


    private void OnEnable()
    {
        foreach (GameObject visableobject in visableObjects)
        {
            visableobject.SetActive(false);
        }
        PhotonNetwork.NetworkingClient.EventReceived += NetworkingClient_EventReceived;
    }


    private void OnDisable()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= NetworkingClient_EventReceived;
    }


    private void NetworkingClient_EventReceived(EventData obj)
    {
        if (obj.Code == Global.TASK_APPROVE)
        {
            foreach(GameObject visableobject in visableObjects)
            {
                visableobject.SetActive(true);
            }
            InformationPannel.SetActive(false);

        }
    }
}
