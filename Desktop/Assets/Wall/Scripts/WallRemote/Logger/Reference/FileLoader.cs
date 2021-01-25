using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;

public class TransformNode : MonoBehaviour
{
    public string transformPosition;
    public string transformRotation;
    public string transformScale;
    public string path;

}

public class FileLoader : MonoBehaviour
{
    public Loadscene loadscene;

    public Transform Wall;
    public int VisualizationNum;

    private string TransformxmlFilePath = "F:/Game project/Bas/Bas/Assets/DataXML.xml";
    private string EventxmlFilePath = "F:/Game project/Bas/Bas/Assets/EventXML.xml";
    private XmlDocument doc;
    private XmlDocument eventdoc;

    private List<Dictionary<string, TransformNode>> TransformInfo;
    private List<List<Dictionary<string, TransformNode>>> TransformInfoList;

    private float recordingClock;
    private float recordingRate = 0.01f;

    private int CurrentEventNodeindex;
    private XmlNodeList EventNodeList;

    private int VisualiztionIndex;

    private float TotalTime;

    //public GameObject LoadingImage;


    private void Start()
    {
        TransformInfoList = new List<List<Dictionary<string, TransformNode>>>();
        TransformInfo = new List<Dictionary<string, TransformNode>>();
        doc = new XmlDocument();
        eventdoc = new XmlDocument();
        recordingClock = 0;
        VisualiztionIndex = 0;

        if (!File.Exists("F:/Game project/Bas/Bas/Assets/DataXML.xml"))
        {
            Debug.LogError("No transform storage File founded");
        }

        if (!File.Exists("F:/Game project/Bas/Bas/Assets/EventXML.xml"))
        {
            Debug.LogError("No event storage File founded");
        }
        doc.Load(TransformxmlFilePath);
        eventdoc.Load(EventxmlFilePath);

        EventNodeList = eventdoc.GetElementsByTagName("Event");
        CurrentEventNodeindex = 0;

        GeneratePositioninfo();

        string TotalTimestring = doc.FirstChild.LastChild.Attributes?["TotalTime"].Value;
        TotalTime = float.Parse(TotalTimestring);

        //Debug.Log(TransformInfoList[0].Count);
        //Debug.Log(TransformInfoList[1].Count);

    }

    private Vector3 StringtoVector3(string sVector)
    {
        if (sVector.StartsWith("(") && sVector.EndsWith(")"))
        {
            sVector = sVector.Substring(1, sVector.Length - 2);
        }

        // split the items
        string[] sArray = sVector.Split(',');

        // store as a Vector3
        Vector3 result = new Vector3(
            float.Parse(sArray[0]),
            float.Parse(sArray[1]),
            float.Parse(sArray[2]));

        return result;
    }

    public Quaternion StringtoQuaternion(string name)
    {
        name = name.Replace("(", "").Replace(")", "");
        string[] s = name.Split(',');
        return new Quaternion(float.Parse(s[0]), float.Parse(s[1]), float.Parse(s[2]), float.Parse(s[3]));
    }

    public void LoadFile()
    {
        //Invoke("LoadFileOnceTime", 0);
        InvokeRepeating("LoadFileOnceTime", 0, recordingRate);
    }



    private void GeneratePositioninfo()
    {
        // Cannot instantiate in as field for overlaping previous data while using overlap
        List<List<Dictionary<string, TransformNode>>> transforminfolist = new List<List<Dictionary<string, TransformNode>>>();
        XmlNodeList VisualiztionList = doc.GetElementsByTagName("Visualization");
        VisualizationNum = VisualiztionList.Count;
        for (int i = 0; i < VisualizationNum; i++)
        {
            List<Dictionary<string, TransformNode>> transforminfo = new List<Dictionary<string, TransformNode>>();
            XmlNode VisualizationNode = VisualiztionList[i];
            XmlNodeList ClockList = VisualizationNode.ChildNodes;
            Debug.Log(ClockList.Count+"vis "+i);
            //TransformInfo.Clear();
            for (int j = 0; j < ClockList.Count; j++)
            {
                XmlNode Clock = ClockList[i];
                if (!Clock.Name.ToString().Equals("Clock"))
                {
                    return;
                }
                
                Dictionary<string, TransformNode> transformObjectsDic = new Dictionary<string, TransformNode>();
                XmlNodeList TransformList = Clock.ChildNodes;
                foreach (XmlNode Transformnode in TransformList)
                {
                    TransformNode transformObject = new TransformNode();
                    string transformLocalPosition = Transformnode.Attributes?["Postion"].Value;
                    string transfromLocalRotation = Transformnode.Attributes?["Rotation"].Value;
                    string transformLocalScale = Transformnode.Attributes?["Scale"].Value;
                    string Path = Transformnode.Attributes?["Path"].Value;
                    
                    transformObject.transformPosition = transformLocalPosition;
                    transformObject.transformRotation = transfromLocalRotation;
                    transformObject.transformScale = transformLocalScale;
                    transformObject.path = Path;
                    transformObjectsDic.Add(Path, transformObject);
                }
                transforminfo.Add(transformObjectsDic);
            }

            transforminfolist.Add(transforminfo);
        }

        TransformInfoList = transforminfolist;
    }

    private void LoadNextVis()
    {
        loadscene.LoadVisualization();
    }

    private void LoadFileOnceTime()
    {
        //deal with the event first
        string FirstEventTime= EventNodeList[CurrentEventNodeindex].Attributes?["Clock"].Value;
        if (recordingClock.ToString().Equals(FirstEventTime))
        {
            string EventType= EventNodeList[CurrentEventNodeindex].Attributes?["name"].Value;
            switch (EventType)
            {
                case "ChangeVisualization":
                    LoadNextVis();
                    VisualiztionIndex++;
                    break;
                case "blabla":
                    break;
                default:
                    Debug.LogError("Unknown Type: " + EventType);
                    break;

            }

            CurrentEventNodeindex++;
            
        }
        List<Dictionary<string, TransformNode>> TransformIn =TransformInfoList[0];
        Dictionary<string, TransformNode> transformObjectsDic = TransformIn[(int)(recordingClock/recordingRate)];

        //Debug.Log(transformObjectsDic.Count);
        foreach (KeyValuePair<string, TransformNode> transformObjectPair in transformObjectsDic)
        {
            string path = transformObjectPair.Key;
            TransformNode transforminfo = transformObjectPair.Value;
            Transform moveableObject = Wall.Find(path);

            //Debug.Log(path + transforminfo.transformPosition + transforminfo.transformRotation);
            moveableObject.localPosition = StringtoVector3(transforminfo.transformPosition);
            moveableObject.localRotation = StringtoQuaternion(transforminfo.transformRotation);
            moveableObject.localScale = StringtoVector3(transforminfo.transformScale);
        }
        recordingClock += recordingRate;
        if (recordingClock == TotalTime)
        {
            Debug.Log("stop!!");
            CancelInvoke("LoadFileOnceTime");
        }
    }
}
