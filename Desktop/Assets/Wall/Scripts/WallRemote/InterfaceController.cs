using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class InterfaceController : MonoBehaviourPun
{
    private int currentInterfaceIndex;
    private PhotonView pv;
    private string visualizationNameTobeSet;
    public GameObject TaskApproveUI;

    private GameObject UIDestroyable;
    void Start()
    {
        pv = GetComponent<PhotonView>();
        currentInterfaceIndex = 0;
    }

    // Update is called once per frame
    public void LoadNextVisualization()
    {
        int num = GetComponent<photonviewController>().visualizationCollection.Count;
        if (currentInterfaceIndex < num - 1)
        {
            currentInterfaceIndex++;
        }
        else
        {
            currentInterfaceIndex = 0;
        }

        visualizationNameTobeSet = GetComponent<photonviewController>().visualizationCollection[currentInterfaceIndex].name;

        if (!pv.IsMine)
        {
            pv.RPC("RPC_LoadNextVis", RpcTarget.All, visualizationNameTobeSet);
        }

        //sending the data by RaiseEvent only work on one part
        //RaiseNextLevelEvent(visualizationNameTobeSet);
    }

    [PunRPC]
    void RPC_LoadNextVis(string visualizationnameTobeSet)
    {
        //Debug.Log(visualizationnameTobeSet);
        GetComponent<photonviewController>().setVisualization(visualizationnameTobeSet);
    }


    public void RaiseTaskApproveEvent()
    {
        TaskApproveUIhandle();
        object[] datas = new object[] {};
        PhotonNetwork.RaiseEvent(Global.TASK_APPROVE, datas, RaiseEventOptions.Default, SendOptions.SendUnreliable);
    }

    private void TaskApproveUIhandle()
    {
        UIDestroyable = Instantiate(TaskApproveUI);
        Invoke("DestroyUI",2);
    }

    private void DestroyUI()
    {
        Destroy(UIDestroyable);
    }

}
