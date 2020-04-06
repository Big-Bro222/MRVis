using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//[ExecuteInEditMode ]
public class FileController : MonoBehaviour
{
    private List<string> csvParseData;
    private List<Dictionary<string,string>>csvDataList;
    public GameObject text;
    // Start is called before the first frame update
    void Start()
    {
        csvDataList = new List<Dictionary<string, string>>();
        Debug.Log(Application.dataPath + "Assets/Build-in scene/BarChart");
        CsvParser.GetInstance().LoadFile(Application.dataPath + "/Build-in scene/BarChart" ,"rerb.csv");
        for(int i = 1; i < CsvParser.GetInstance().m_ArrayData.Count; i++)
        {
            Dictionary<string,string>csvData = new Dictionary<string, string>();
            string csvArray = CsvParser.GetInstance().m_ArrayData[i];
            //Debug.Log(csvArray);
            string[] sArray=csvArray.Split(new char[1] {','});
            csvData.Add("type", sArray[0]);
            csvData.Add("date", sArray[1]);
            csvData.Add("time", sArray[2]);
            csvData.Add("reason", sArray[3]);
            //type,date,time,reason
            csvDataList.Add(csvData);
        }

        DrawBarChart();

       
    }

    private void DrawBarChart()
    {
        int[] pertubationCount = new int[8] {0,0,0,0,0,0,0,0 };
        int[] interruptionCount = new int[8] { 0, 0, 0, 0, 0, 0, 0, 0 };
        int[] otherCount = new int[8] { 0, 0, 0, 0, 0, 0, 0, 0 };

       for(int i=0; i<csvDataList .Count; i++)
        {

            if (csvDataList[i]["type"].Equals("perturbation"))
            {
                int year = int.Parse(csvDataList[i]["date"].Substring(0, 4));
                pertubationCount[year - 2012]++;
            }
            else if (csvDataList[i]["type"].Equals("interruption")) {
                int year = int.Parse(csvDataList[i]["date"].Substring(0, 4));
                
                interruptionCount[year - 2012]++;
            }
            else
            {
                int year = int.Parse(csvDataList[i]["date"].Substring(0, 4));
                otherCount[year - 2012]++;
            }
        }

        foreach (int pertubation in pertubationCount)
        {
            Debug.Log(pertubation);
        }

        drawRect(pertubationCount,Color.blue,0);
        drawRect(interruptionCount, Color.red, 1);
        drawRect(otherCount, Color.green, 2);
        drawAxis();
    }

    private void drawAxis()
    {
        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.positionCount = 3;
       
        lineRenderer.startWidth = 0.01f;
        lineRenderer.endWidth = 0.01f;
        lineRenderer.useWorldSpace = false;
        Vector3[] pos = new Vector3[3] { new Vector3(1, -0.001f, 0), new Vector3(-0.001f, -0.001f, 0), new Vector3(-0.001f, 1, 0) };
        lineRenderer.SetPositions(pos);


        for(int i=1; i < 9; i++)
        {
            GameObject txt = Instantiate(text, new Vector3(0.03f * (i* 4), -0.05f, 0), Quaternion.identity, transform);
            txt.GetComponent<TextMeshPro>().SetText((i+2011).ToString());
            txt.name = (i + 2011).ToString();

        }

        
    }

    private void drawRect(int[] typeCount,Color color, int barIndex)
    {

        for(int i = 0; i < typeCount.Length; i++)
        {

            float barWidth = 0.03f;
            GameObject rect = GameObject.CreatePrimitive(PrimitiveType.Cube);
            rect.GetComponent<MeshRenderer>().material.color = color;
            Bar bar= rect.AddComponent<Bar>();
            bar.num = typeCount[i];
            bar.year = i + 2012;
            switch (barIndex)
            {
                case 0:
                    bar.type = "pertubation";
                    break;
                case 1:
                    bar.type = "interruption";
                    break;
                default:
                    bar.type = "other";
                    break;
            }
            rect.name = i.ToString();
            rect.transform.SetParent(transform);
            rect.transform.localScale = new Vector3(barWidth, 0.001f*typeCount[i], 0.003f);
            float yOffset = 0.001f * typeCount[i];
            rect.transform.localPosition = rect.transform.localPosition + new Vector3(4* barWidth * i+ barWidth*barIndex, yOffset/2, 0);
        }

    }
}
