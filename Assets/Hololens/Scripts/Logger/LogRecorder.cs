using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogRecorder : MonoBehaviour
{
    List<string> tasks;
    private float TimeDuration;
    private int Currenttask;
    private string taskname;
    private int StationNumber;

    void Start()
    {
        tasks = new List<string>();
        TimeDuration = 0;
        taskname = "Task0";
        StationNumber = 0;
        Currenttask = 0;
    }

    public void StartTask()
    {
        Debug.Log("Starttask");
        InvokeRepeating("Timer", 0, 0.02f);
    }


    private void Timer()
    {
        TimeDuration += 0.02f;
    } 

    public void CountFocus()
    {
        StationNumber++;
    }

    public void EndTask()
    {
        Debug.Log("nexttask");
        string taskobject = taskname+","+TimeDuration+","+ StationNumber;
        Debug.Log(taskobject);
        tasks.Add(taskobject);
        Resettask();
        if (tasks.Count == 3)
        {
            Debug.Log("task ends!");
            object[] datas = new object[] { tasks[0],tasks[1],tasks[2] };
            PhotonNetwork.RaiseEvent(Global.LOG_FINISH, datas, RaiseEventOptions.Default, SendOptions.SendUnreliable);
        }
    }

    private void Resettask()
    {
        Currenttask ++;
        taskname = "Task" + Currenttask;
        StationNumber = 0;
        CancelInvoke("Timer");
        TimeDuration = 0;

    }
}
