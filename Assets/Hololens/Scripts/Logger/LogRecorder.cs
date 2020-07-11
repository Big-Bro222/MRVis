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
    private int Hotelsexplored;
    private string taskname;
    private int StationNumber;
    [SerializeField] InvisiableInteractableSlider LabelSlider;
    [SerializeField] InvisiableInteractableSlider AnnotationSlider;
    [SerializeField] InvisiableInteractableSlider HighLightSlider;
    private List<string> UniqueHotelNames;

    void Start()
    {
        UniqueHotelNames = new List<string>();
        tasks = new List<string>();
        TimeDuration = 0;
        taskname = "Task0";
        StationNumber = 0;
        Currenttask = 0;
        Hotelsexplored = 0;
    }

    public void StartTask()
    {
        Debug.Log("Starttask");
        Resettask();
        InvokeRepeating("Timer", 0, 0.02f);
    }

    public void HotelsexploreCount(string HotelName)
    {
        if (!UniqueHotelNames.Contains(HotelName))
        {
            UniqueHotelNames.Add(HotelName);
        }
        Hotelsexplored++;
    }
    private void Timer()
    {
        TimeDuration += 0.02f;
    } 

    public void CountFocus()
    {
        StationNumber++;
    }

    public void EndTask(string taskName)
    {
        Debug.Log("nexttask");
        string taskobject = "";
        if (taskName.Equals("Customize"))
        {
            taskobject = taskName + "," + LabelSlider.SliderValue + "," + AnnotationSlider.SliderValue+","+ HighLightSlider.SliderValue;
        }
        else
        {
            taskobject = taskName + "," + TimeDuration + "," + StationNumber+","+ Hotelsexplored + ","+ UniqueHotelNames.Count;
        }
        Debug.Log(taskobject+" is recorded");
        tasks.Add(taskobject);
        Resettask();
        if (tasks.Count == 4)
        {
            object[] datas = new object[] { tasks[0],tasks[1],tasks[2],tasks[3] };
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
        Hotelsexplored = 0;
        UniqueHotelNames = new List<string>();
    }
}
