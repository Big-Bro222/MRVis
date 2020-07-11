using ExitGames.Client.Photon;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LogEventHandler : MonoBehaviour
{
    public GameObject SaveXMLConfirm;
    public XMLFileGenerator xMLFileGenerator;
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
        if (obj.Code == Global.LOG_FINISH)
        {

            Debug.Log("LogFinish!!!!");
            object[] datas = (object[])obj.CustomData;
            List<string> tasks = new List<string>();
            for(int i = 0; i < datas.Length; i++)
            {
                tasks.Add((string)datas[i]);
            }
            xMLFileGenerator.logs = tasks;
            SaveXMLConfirm.SetActive(true);
        }
    }
}
