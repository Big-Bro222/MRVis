using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteInEditMode]
public class DataParser : MonoBehaviour
{
    private List<string> csvParseData;
    private List<Dictionary<string, string>> csvDataList;
    private GameObject text;
    public GameObject Node;
    public GameObject Bar;

    // Start is called before the first frame update
    void Start()
    {
        csvDataList = new List<Dictionary<string, string>>();
        CsvParser.GetInstance().LoadFile(Application.dataPath + "/Build-in scene/ScaterPlot", "cars.csv");
        for (int i = 2; i < CsvParser.GetInstance().m_ArrayData.Count; i++)
        {
            Dictionary<string, string> csvData = new Dictionary<string, string>();
            string csvArray = CsvParser.GetInstance().m_ArrayData[i];
            string[] sArray = csvArray.Split(new char[1] { ';' });
            csvData.Add("name", sArray[0]);
            csvData.Add("Horsepower", sArray[4]);
            csvData.Add("Acceleration", sArray[6]);
            csvData.Add("Weight", sArray[5]);
            ////name,calories,protein,fat,sugars
            csvDataList.Add(csvData);
        }

        DrawScatterPlot();
        DrawBarchart();

    }


    private void DrawBarchart()
    {



        GameObject DataPointFolder = new GameObject("BarFolder");
        DataPointFolder.transform.SetParent(transform);

        int chartscale = 8;
        for (int i = 0; i < csvDataList.Count; i++)
        {
            float Horsepower = float.Parse(csvDataList[i]["Horsepower"]);
            float Acceleration = float.Parse(csvDataList[i]["Acceleration"]);
            float Weight= float.Parse(csvDataList[i]["Weight"]);
            string name = csvDataList[i]["name"]+"- Bar";
            Debug.Log(Weight + " is weight");
            float zScale = 0.0001f;
            GameObject Datapoint = Instantiate(Node, new Vector3((Horsepower - 40) / (chartscale * 20) - 0.5f, (Acceleration - 5) / (chartscale * 3) - 0.5f, -(Weight* zScale) /2), Quaternion.identity, DataPointFolder.transform);
            Datapoint.transform.localScale = new Vector3(Datapoint.transform.localScale.x, Datapoint.transform.localScale.y, Weight* zScale);
            Datapoint.name = name;
        }
    }
    private void DrawScatterPlot()
    {


        GameObject DataPointFolder = new GameObject("DataPointFolder");
        DataPointFolder.transform.SetParent(transform);

        LineRenderer lineRenderer = DataPointFolder.AddComponent<LineRenderer>();
        lineRenderer.positionCount = 3;

        lineRenderer.startWidth = 0.01f;
        lineRenderer.endWidth = 0.01f;
        lineRenderer.useWorldSpace = false;
        Vector3[] pos = new Vector3[3] { new Vector3(0.5f, -0.5f, 0),
                                         new Vector3(-0.5f, -0.5f, 0),
                                         new Vector3(-0.5f, 0.5f, 0)};
        lineRenderer.SetPositions(pos);

        int chartscale = 8;
        for (int i = 0; i < csvDataList.Count; i++)
        {
            float Horsepower = float.Parse(csvDataList[i]["Horsepower"]);
            float Acceleration = float.Parse(csvDataList[i]["Acceleration"]);

            string name = csvDataList[i]["name"];
            GameObject Datapoint = Instantiate(Node, new Vector3((Horsepower-40) / (chartscale*20) - 0.5f, (Acceleration-5) /(chartscale*3) - 0.5f,0), Quaternion.identity, DataPointFolder.transform);
            Datapoint.name = name;
            //DataPoint dataPointInfo = Datapoint.AddComponent<DataPoint>();
            //dataPointInfo.calories = calories;
            //dataPointInfo.protein = protein;
            //dataPointInfo.fat = fat;
            //dataPointInfo.sugars = sugars;
            //Debug.Log(calories + " is calories");
        }




    }
}
