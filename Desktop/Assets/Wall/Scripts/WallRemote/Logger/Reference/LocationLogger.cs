using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEditor;
using UnityEngine;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System;

public class LocationLogger : MonoBehaviour
{
    public GameObject Wall;
    private XmlDocument xmlDocument;
    private XmlElement root;
    private Dictionary<string,Transform> RecordabletransformDic;
    private float recordingClock;
    private XmlElement VisualizationElement;
    private EventLogger eventLogger;
    private GameObject child;


    private string CompentPath;//recieved path
    private string UnityPath;//Unity path

    //recordingRate per second
    private float recordingRate=0.01f;

    void Start()
    {
        xmlDocument = new XmlDocument();
        root = xmlDocument.CreateElement("Save");
        root.SetAttribute("FileName", "File_01");
        xmlDocument.AppendChild(root);
        eventLogger = GetComponent<EventLogger>();
        recordingClock = 0;

        RecordabletransformDic = new Dictionary<string, Transform>();
        RegisterTransformtoLogger();
        List<string> keyList = new List<string>(RecordabletransformDic.Keys);
        ////foreach (string key in keyList)
        ////{
        ////    Debug.Log(key);
        ////}
    }

    //自定义文件保存文件夹;
    private void SaveCutScreenPath()
    {
        SaveFileDialog saveFileDialog = new SaveFileDialog();//new folderbrowser
        saveFileDialog.Filter = "XML-File | *.xml";
        saveFileDialog.FileName = "yes";
        saveFileDialog.AddExtension = true;
        //saveFileDialog.CheckPathExists = true;
        //saveFileDialog.CheckFileExists = true;
        saveFileDialog.OverwritePrompt = true;   //enable overwrite;

        saveFileDialog.Title = "Select file";
        saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);  //set default path



        //If pressing OK
        if (saveFileDialog.ShowDialog() == DialogResult.OK)
        {
            xmlDocument.Save(saveFileDialog.FileName);
        }

        ////transfer to Unity Path
        // UnityPath = CompentPath.Replace(@"\", "/");
        // print(UnityPath);
        // //如果 \  比较多的话      
        // //if (UnityPath.IndexOf("/") > 2)
        // //{
        // //UnityPath = CompentPath+ "/";
        // //print("大于了");
        // //}
        // //else {

        // //print("小于了");
        // //}


    }

    public void RegisterTransformtoLogger()
    {
        RecordabletransformDic.Clear();
        Transform[]RecordabletransformArr = Wall.GetComponentsInChildren<Transform>();
        
        //get the transformPath to a dictionary
        //the idea is to get the parent of the transform, so that we can get the path of it.
        foreach (Transform wallchild in RecordabletransformArr)
        {
            if (!wallchild.GetComponent<Recordable>()||wallchild.name == "Wall" || wallchild.parent == Wall.transform)
            {
                continue;
            }

            //string path = AnimationUtility.CalculateTransformPath(wallchild,Wall.transform);
            //Debug.Log(path);
            string path = "";
            child = wallchild.gameObject;
            while (child.name != "Wall" && child.transform.parent != Wall.transform)
            {
                GameObject other = child.transform.parent.gameObject;
                path = child.transform.parent.name + "/" + path;
                child = other;
            }
            Debug.Log(path);
            RecordabletransformDic.Add(path, wallchild);
        }
        
    }


    public void StartRecording()
    {
        Debug.Log("start Recording");
        VisualizationElement = xmlDocument.CreateElement("Visualization");
        root.AppendChild(VisualizationElement);
        eventLogger.StartRecording();
        InvokeRepeating("WriteXMLOnce", 0, recordingRate);

    }

    public void OnVisChange()
    {
        CancelInvoke("WriteXMLOnce");
        RegisterTransformtoLogger();
        eventLogger.OnVisChange();
        VisualizationElement = xmlDocument.CreateElement("Visualization");
        root.AppendChild(VisualizationElement);
        InvokeRepeating("WriteXMLOnce", 0, recordingRate);
    }

    public void StopRecording()
    {
        CancelInvoke("WriteXMLOnce");
        XmlElement EndElement = xmlDocument.CreateElement("End");
        EndElement.SetAttribute("TotalTime", recordingClock.ToString("F3"));
        root.AppendChild(EndElement);
        eventLogger.StopRecording();
        SaveCutScreenPath();
    }

    private void WriteXMLOnce()
    {
        Debug.Log("Writing...");
        XmlElement ClockElement = xmlDocument.CreateElement("Clock");
        ClockElement.SetAttribute("Clock", recordingClock.ToString("F3"));
        //Debug.Log("recording at"+ recordingClock);
        foreach(KeyValuePair<string,Transform> RecordabletransformPair in RecordabletransformDic)
        {
            Transform recordabletransform = RecordabletransformPair.Value;
            string path = RecordabletransformPair.Key;
            string Position = recordabletransform.localPosition.ToString("F3");
            string Rotation = recordabletransform.localRotation.ToString("F3");
            string Scale = recordabletransform.localScale.ToString("F3");

            XmlElement transformElement = xmlDocument.CreateElement("Transform");
            transformElement.SetAttribute("Postion", Position);
            transformElement.SetAttribute("Rotation", Rotation);
            transformElement.SetAttribute("Scale", Scale);
            
            transformElement.SetAttribute("Path",path);
            ClockElement.AppendChild(transformElement);
        }

        VisualizationElement.AppendChild(ClockElement);
        recordingClock += recordingRate;
    }


}
