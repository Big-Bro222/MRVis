using NaughtyAttributes;
using Synchro;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReplayController : MonoBehaviour
{
    public TextAsset CinematicFile;
    public MasterRedirection CommandServer;
    public float StartDelay = 0f;
    public float ReplaySpeed = 1f;
    public Slider Progress;
    public Text TimeProgress;
    
    private Queue<ReplayCommand> ReplayCmdQueue;

    private float StartTime = 0f;
    private bool start = false;
    private int cmdCounter = 1;

    private float endTime = 0f;
    private int cmdCount = 0;

    public bool showLogs = false;

    // Start is called before the first frame update
    void Start()
    {        
        ReplayCmdQueue = new Queue<ReplayCommand>();
        ParseCinematic(CinematicFile);
        TimeProgress.text = "Loaded";
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            ReplayCommand rc = ReplayCmdQueue.Peek();
            while(ReplayCmdQueue.Count > 0 && rc.triggerTime + StartTime < Time.time)
            {
                if (showLogs)
                    Debug.Log(cmdCounter + " " + rc.cmd.ToString());
                CommandServer.ReplayCommandSent(ReplayCmdQueue.Dequeue().cmd);
                cmdCounter++;

                if (ReplayCmdQueue.Count == 0)
                {
                    Debug.Log("<color=#009900>End Of Replay</color>");
                    start = false;
                    continue;
                }

                rc = ReplayCmdQueue.Peek();
            }

            TimeProgress.text = (Time.time - StartTime).ToString() + " / " +  endTime.ToString();
            Progress.value = cmdCounter;
        }
    }

    private void OnDestroy()
    {
        Time.timeScale = 1;
    }

    private void ParseCinematic(TextAsset ta)
    {
        string[] commands = ta.text.Split(new string[] { System.Environment.NewLine }, StringSplitOptions.None);

        foreach(string s in commands)
        {
            string[] command = s.Split(' ');
            ISynchroCommand isc;

            List<int> ownerList = new List<int>();

            switch (command[2])
            {
                case "SpatialStatus":
                    isc = new SpatialStatus(command[3], ParseToVector3(command[4]), ParseToQuaternion(command[5]), ParseToVector3(command[6]));
                    break;
                case "TransformStatusUpdate":
                    isc = new SpatialStatus(command[3], ParseToVector3(command[4]), ParseToQuaternion(command[5]), ParseToVector3(command[6]));
                    break;
                case "Register":
                    isc = new Register(command[3], command[1]);
                    break;
                case "UpdatePresence":
                    List<string> nameList = new List<string>();
                    foreach (string owner in command[3].Split('-'))
                        if(owner != "null")
                            nameList.Add(owner);
                    isc = new UpdatePresence(nameList, command[1]);
                    break;
                case "ChangePermission":
                    foreach (string owner in command[5].Split('-'))
                        if(owner != "null")
                            ownerList.Add(int.Parse(owner));
                    isc = new ChangePermission(command[1], command[3], int.Parse(command[4]), ownerList);
                    break;
                default:
                    isc = new SpatialStatus();

                    Debug.LogError("Invalid command type REally ?");
                    break;

            }

            ReplayCmdQueue.Enqueue(new ReplayCommand(float.Parse(command[0]), isc));            
        }

        cmdCount = ReplayCmdQueue.Count;
        endTime = float.Parse((commands[cmdCount - 1].Split(' '))[0]);
        Debug.Log((commands[cmdCount - 1].Split(' '))[0]);
        Debug.Log("Cinematic File parsed. Size : " + ReplayCmdQueue.Count);
    }

    private Vector3 ParseToVector3(string v)
    {
        string[] values = v.Trim(new char[] { '(', ')' }).Split(',');
        return new Vector3(float.Parse(values[0]), float.Parse(values[1]), float.Parse(values[2]));

    }

    private Quaternion ParseToQuaternion(string q)
    {
        string[] values = q.Trim(new char[] { '(', ')' }).Split(',');
        return new Quaternion(float.Parse(values[0]), float.Parse(values[1]), float.Parse(values[2]), float.Parse(values[3]));
    }

    [Button]
    public void StartReplay()
    {
        StartTime = Time.time - StartDelay;
        start = true;
        Time.timeScale = ReplaySpeed;

        Progress.minValue = 0;
        Progress.maxValue = cmdCount;
    }

    [Button]
    public void Pause()
    {
        Time.timeScale = 0f;
    }
    
    [Button]
    public void UnPause()
    {
        Time.timeScale = ReplaySpeed;
    }
}

public class ReplayCommand
{
    public float triggerTime;
    public ISynchroCommand cmd;

    public ReplayCommand(float triggerTime, ISynchroCommand cmd)
    {
        this.triggerTime = triggerTime;
        this.cmd = cmd;
    }

    public void Apply()
    {
        cmd.Apply();
    }

    public override string ToString()
    {
        return cmd.ToString();
    }
}