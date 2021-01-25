using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEditor;
using UnityEngine;


public class EventLogger : MonoBehaviour
{
    //public GameObject Wall;
    private XmlDocument xmlDocument;
    private XmlElement root;
    private Dictionary<string, Transform> RecordabletransformDic;
    private float recordingClock;
    private XmlElement VisualizationElement;

    private GameObject child;

    //recordingRate per second
    private float recordingRate = 0.01f;

    void Start()
    {
        xmlDocument = new XmlDocument();
        root = xmlDocument.CreateElement("Save");
        root.SetAttribute("FileName", "File_01");
        xmlDocument.AppendChild(root);

        recordingClock = 0;

        RecordabletransformDic = new Dictionary<string, Transform>();
        List<string> keyList = new List<string>(RecordabletransformDic.Keys);
        //foreach (string key in keyList)
        //{
        //    Debug.Log(key);
        //}
    }


    public void StartRecording()
    {
        Debug.Log("start Recording");
        InvokeRepeating("TimeRecording", 0, recordingRate);
    }

    public void OnVisChange()
    {
        CancelInvoke("TimeRecording");
        XmlElement EventElement = xmlDocument.CreateElement("Event");
        EventElement.SetAttribute("name", "ChangeVisualization");
        EventElement.SetAttribute("Clock", recordingClock.ToString("F3"));
        //To setAttribute of visualization
        root.AppendChild(EventElement);
        InvokeRepeating("TimeRecording", 0f, recordingRate);
    }

    private void TimeRecording()
    {
        recordingClock += recordingRate;
    }

    public void StopRecording()
    {
        CancelInvoke("TimeRecording");
    }


    

}
