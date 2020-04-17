using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//[ExecuteInEditMode]
public class ScaterPlotParser : MonoBehaviour
{
    private List<string> csvParseData;
    private List<Dictionary<string, string>> csvDataList;
    private GameObject text;
    public GameObject Node;
    // Start is called before the first frame update
    void Start()
    {
        csvDataList = new List<Dictionary<string, string>>();
        Debug.Log(Application.dataPath + "Assets/Build-in scene/ScaterPlot");
        CsvParser.GetInstance().LoadFile(Application.dataPath + "/Build-in scene/ScaterPlot", "cereal.csv");
        for (int i = 2; i < CsvParser.GetInstance().m_ArrayData.Count; i++)
        {
            Dictionary<string, string> csvData = new Dictionary<string, string>();
            string csvArray = CsvParser.GetInstance().m_ArrayData[i];
            string[] sArray = csvArray.Split(new char[1] { ';' });
            csvData.Add("name", sArray[0]);
            csvData.Add("calories", sArray[3]);
            csvData.Add("protein", sArray[4]);
            csvData.Add("fat", sArray[5]);
            csvData.Add("sugars", sArray[9]);
            ////name,calories,protein,fat,sugars
            csvDataList.Add(csvData);
        }

        Draw();


    }


    private void Draw()
    {
        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.positionCount = 5;

        lineRenderer.startWidth = 0.01f;
        lineRenderer.endWidth = 0.01f;
        lineRenderer.useWorldSpace = false;
        Vector3[] pos = new Vector3[5] { new Vector3(0.5f, -0.5f, 0.5f),
                                         new Vector3(-0.5f, -0.5f, 0.5f),
                                         new Vector3(-0.5f, 0.5f, 0.5f),
                                         new Vector3(-0.5f, -0.5f, 0.5f),
                                         new Vector3(-0.5f, -0.5f, -0.5f)};
        lineRenderer.SetPositions(pos);

        GameObject DataPointFolder = new GameObject("DataPointFolder");
        DataPointFolder.transform.SetParent(transform);

        int chartscale = 8;
        for(int i = 0; i < csvDataList.Count; i++)
        {
            float calories = float.Parse(csvDataList[i]["calories"]);
            float protein= float.Parse(csvDataList[i]["protein"]);
            float fat= float.Parse(csvDataList[i]["fat"]);
            float sugars= float.Parse(csvDataList[i]["sugars"]);
            string name = csvDataList[i]["name"];
            GameObject Datapoint = Instantiate(Node,new Vector3(protein/ chartscale-0.5f, fat/ chartscale - 0.5f, -sugars/ (2*chartscale)+0.5f),Quaternion.identity, DataPointFolder.transform);
            Datapoint.name = name;
            DataPoint dataPointInfo = Datapoint.AddComponent<DataPoint>();
            dataPointInfo.calories = calories;
            dataPointInfo.protein = protein;
            dataPointInfo.fat = fat;
            dataPointInfo.sugars = sugars;
            Debug.Log(calories+ " is calories");
        }
         
        //for (int i = 1; i < 9; i++)
        //{
        //    GameObject txt = Instantiate(text, new Vector3(0.03f * (i * 4), -0.05f, 0), Quaternion.identity, transform);
        //    txt.GetComponent<TextMeshPro>().SetText((i + 2011).ToString());
        //    txt.name = (i + 2011).ToString();

        //}


    }

}
