using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogRecorder : MonoBehaviour
{
    [SerializeField] private FocusObj focusObj;
    [SerializeField] private Transform PlaneTarget;
    [SerializeField] private GameObject setfoucusPrefab;
    List<string> tasks;
    private float TimeDuration;
    private int Currenttask;
    private int Hotelsexplored;
    //private string taskname;
    private int StationNumber;
    private string CurrentFocusName;
    [SerializeField] InvisiableInteractableSlider LabelSlider;
    [SerializeField] InvisiableInteractableSlider AnnotationSlider;
    [SerializeField] InvisiableInteractableSlider HighLightSlider;
    private List<string> UniqueHotelNames;
    private Plane plane;
    private List<float> Timerlist;


    void Start()
    {
        plane = new Plane(PlaneTarget.forward, PlaneTarget.position);
        UniqueHotelNames = new List<string>();
        tasks = new List<string>();
        Timerlist = new List<float>();
        TimeDuration = 0;
        //taskname = "Task0";
        StationNumber = 0;
        Currenttask = 0;
        Hotelsexplored = 0;
        CurrentFocusName = "";
        MouseEventCoreService.Instance.OnKeypressed += OnFocusConfirmed;

    }

    #region public Method

    //Called in MapTaskController
    public void StartTask()
    {
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


    public void CountFocus()
    {
        StationNumber++;
    }

    public void EndTask(string taskName)
    {
        string taskobject = "";
        float distance = plane.GetDistanceToPoint(Camera.main.transform.position);
        float TaskDuration = Timerlist[Timerlist.Count - 1];
        //float TaskDuration = TimeDuration;

        if (distance < 0) { distance = -distance; }
        if (taskName.Equals("Customize"))
        {
            taskobject = taskName + "," + LabelSlider.SliderValue + "," + AnnotationSlider.SliderValue+","+ HighLightSlider.SliderValue+","+distance;
        }
        else
        {
            taskobject = taskName + "," + TaskDuration + "," + StationNumber+","+ Hotelsexplored + ","+ UniqueHotelNames.Count+","+distance+","+ CurrentFocusName;

            Debug.Log("switch times: " + Timerlist.Count);
        }
        tasks.Add(taskobject);
        if (tasks.Count == 4)
        {

            Debug.Log("Sending Task avaliable");
            object[] datas = new object[] { tasks[0],tasks[1],tasks[2],tasks[3] };
            PhotonNetwork.RaiseEvent(Global.LOG_FINISH, datas, RaiseEventOptions.Default, SendOptions.SendUnreliable);
        }
    }
    #endregion

    #region private Method

    private void OnFocusConfirmed(string keycode)
    {
        //if (keycode.Equals("Space") && Currenttask != 0 && Currenttask != 5)
        //{

        //    CancelInvoke("Timer");
        //    Timerlist.Add(TimeDuration);
        //    InvokeRepeating("Timer", 0, 0.02f);
        //    CurrentFocusName = "yes";
        //    Instantiate(setfoucusPrefab,new Vector3(0,0,-0.3f),Quaternion.identity,transform);
        //}
        if (keycode.Equals("Space") && Currenttask != 0)
        {
            CancelInvoke("Timer");
            CurrentFocusName = focusObj.GetFocus().name;
            Timerlist.Add(TimeDuration);
            Debug.Log("Space in LogRecorder: "  + Currenttask + ", " + Timerlist[Timerlist.Count - 1]+","+ CurrentFocusName);
            InvokeRepeating("Timer", 0, 0.02f);
            Instantiate(setfoucusPrefab, new Vector3(0, 0, -0.3f), Quaternion.identity, transform);
        }
    }

    private void Timer()
    {
        TimeDuration += 0.02f;
    }

    private void Resettask()
    {
        Currenttask ++;
        CancelInvoke("Timer");
        StationNumber = 0;
        TimeDuration = 0;
        Hotelsexplored = 0;
        UniqueHotelNames = new List<string>();
        CurrentFocusName = "";
        CancelInvoke("Timer");
        Timerlist = new List<float>();
    }

    #endregion 
}
