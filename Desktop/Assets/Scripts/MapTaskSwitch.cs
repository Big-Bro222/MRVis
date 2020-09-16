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
        PracticeMode,
        OnScreen,
        InFront,
        Fixedlabel,
        Customize
    }
    public TaskState taskState;
    public RectTransform scalable;
    public RectTransform Image;
    public TextMeshProUGUI text;
    public GameObject CustomizeSliderGroup;

    public List<Edge> Edges;
    public List<int> MetroAccidentNums;
    public List<TaskState>taskstatessetup;

    private int currentStateIndex;

    void Start()
    {
        currentStateIndex = 0;
        taskState = taskstatessetup[currentStateIndex];
        CustomizeSliderGroup.SetActive(false);
        text.text = "Task " + taskState.ToString();
    }

    public void NextTask()
    {
        scalable.localScale = new Vector3(1, 1, 1);
        Image.localPosition = new Vector3(0,0,0);
        if (taskState == TaskState.Fixedlabel)
        {
            CustomizeSliderGroup.SetActive(true);
        }
        else if (taskState == TaskState.Customize)
        {
            text.text = "Task is over!!!";
            object[] data = new object[] { "Over" };
            PhotonNetwork.RaiseEvent(Global.NEXT_TASK, data, RaiseEventOptions.Default, SendOptions.SendReliable);
            return;
        }
        currentStateIndex++;
        taskState= taskstatessetup[currentStateIndex];
        for (int j = 0; j<Edges.Count; j++)
        {
            if((j+ currentStateIndex) < MetroAccidentNums.Count)
            {
                Edges[j].MetroaccidentNum = MetroAccidentNums[j + currentStateIndex];
            }
            else
            {
                Edges[j].MetroaccidentNum = MetroAccidentNums[j + currentStateIndex - MetroAccidentNums.Count];

            }
        }

        text.text = "Task " + taskState.ToString();
        object[] datas = new object[] { taskState.ToString()};
        Debug.Log(taskState.ToString() + " sent from monitor");
        PhotonNetwork.RaiseEvent( Global.NEXT_TASK, datas, RaiseEventOptions.Default, SendOptions.SendReliable);
    }

    public void StartTask()
    {
        if(taskState== TaskState.PracticeMode)
        {
            return;
        }
        else
        {
            object[] datas = new object[] { };
            PhotonNetwork.RaiseEvent(Global.START_TASK, datas, RaiseEventOptions.Default, SendOptions.SendReliable);
        }

    }



}
