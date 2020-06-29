using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using TMPro;

public class MapTaskSwitch : MonoBehaviourPun
{
    public enum TaskState
    {
        OnScreen,
        InFront,
        OnHoloLens,
        Fixedlabel,
        Customize
    }
    public TaskState taskState;
    public RectTransform scalable;
    public RectTransform Image;
    public TextMeshProUGUI text;

    [SerializeField] PhotonView pv;
    void Start()
    {
        taskState = TaskState.OnScreen;
    }

    public void NextTask()
    {
        scalable.localScale = new Vector3(1, 1, 1);
        Image.localPosition = new Vector3(0,0,0);
        if (taskState == TaskState.Customize)
        {
            text.text = "Task is over!!!";
            return;
        }
        taskState++;
        text.text = "Task " + ((int)taskState+1);
        Debug.Log("Monitor send");
        object[] datas = new object[] { };
        PhotonNetwork.RaiseEvent( Global.NEXT_TASK, datas, RaiseEventOptions.Default, SendOptions.SendReliable);
    }



}
