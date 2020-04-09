using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit;
using UnityEngine;
using TMPro;

//[ExecuteInEditMode ]
public class FileController : MonoBehaviour
{
    private List<string> csvParseData;
    private List<Dictionary<string, string>> csvDataList;
    public GameObject text;
    public GameObject Panel;

    // Start is called before the first frame update
    void Start()
    {
        csvDataList = new List<Dictionary<string, string>>();
        Debug.Log(Application.dataPath + "Assets/Build-in scene/BarChart");
        CsvParser.GetInstance().LoadFile(Application.dataPath + "/Build-in scene/BarChart", "rerb.csv");
        for (int i = 1; i < CsvParser.GetInstance().m_ArrayData.Count; i++)
        {
            Dictionary<string, string> csvData = new Dictionary<string, string>();
            string csvArray = CsvParser.GetInstance().m_ArrayData[i];
            //Debug.Log(csvArray);
            string[] sArray = csvArray.Split(new char[1] { ',' });
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
        int[ , ] pertubationCount = new int[8,5] { { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 } };
        int[,] interruptionCount = new int[8, 5] { { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 } };
        int[,] otherCount = new int[8, 5] { { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 } };




       for(int i=0; i<csvDataList .Count; i++)
        {

            if (csvDataList[i]["type"].Equals("perturbation"))
            {
                int year = int.Parse(csvDataList[i]["date"].Substring(0, 4));
                int timeZone = CalculateTimeZone(csvDataList[i]["time"]);
                pertubationCount[year - 2012, timeZone]++;
            }
            else if (csvDataList[i]["type"].Equals("interruption")) {
                int year = int.Parse(csvDataList[i]["date"].Substring(0, 4));
                int timeZone = CalculateTimeZone(csvDataList[i]["time"]);
                interruptionCount[year - 2012,timeZone]++;
            }
            else
            {
                int year = int.Parse(csvDataList[i]["date"].Substring(0, 4));
                int timeZone = CalculateTimeZone(csvDataList[i]["time"]);
                otherCount[year - 2012,timeZone]++;
            }
        }



        drawRect(pertubationCount,Color.blue,0);
        drawRect(interruptionCount, Color.red, 1);
        drawRect(otherCount, Color.green, 2);
        drawAxis();
    }

    private int CalculateTimeZone(string time)
    {
       int index = 0;
        string[] times = time.Split(new char[1] { ':' });
        int hour = int.Parse(times[0]);

        if (hour < 7)
        {
            index = 0;
        }else if (hour < 11)
        {
            index = 1;
        }else if(hour< 15)
        {
            index = 2;
        }else if (hour < 20)
        {
            index = 3;
        }
        else
        {
            index = 4;
        }


        return index;
    }

    private void drawAxis()
    {
        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.positionCount = 5;
       
        lineRenderer.startWidth = 0.01f;
        lineRenderer.endWidth = 0.01f;
        lineRenderer.useWorldSpace = false;

       
        Vector3[] pos = new Vector3[5] { new Vector3(1, -0.001f, 0), new Vector3(-0.001f, -0.001f, 0), new Vector3(-0.001f, 1, 0),new Vector3(-0.001f, -0.001f, 0),new Vector3(-0.001f, -0.001f, -1) };
        lineRenderer.SetPositions(pos);


        for(int i=1; i < 9; i++)
        {
            GameObject txt = Instantiate(text, new Vector3(0.03f * (i* 4), -0.05f, 0), Quaternion.identity, transform);
            txt.GetComponent<TextMeshPro>().SetText((i+2011).ToString());
            txt.name = (i + 2011).ToString();

        }

        
    }


    private Mesh CreateMesh(List<int> lineChartInfo)
    {

        List<string> strList = new List<string>();

        for (int i = 0; i < 3; i++)
        {
            strList.Add("str" + i);//循环添加元素
        }

        string[] strArray = strList.ToArray();//strArray=[str0,str1,str2]

        List<Vector3> verticesList = new List<Vector3>();
        verticesList.Add(new Vector3(0, 0, 0));
        for (int i = 0; i < lineChartInfo.Count; i++)
        {
            verticesList.Add(new Vector3(0, lineChartInfo[i]*0.004f, i*0.1f));
        }
        verticesList.Add(new Vector3(0, 0, (lineChartInfo.Count - 1)*0.1f));

        Vector3[] meshVertices = verticesList.ToArray();

        Mesh mesh = new Mesh();
        mesh.name = "rectMesh";
        mesh.vertices = meshVertices;
        //mesh.uv = new Vector2[] { new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1), new Vector2(1, 0) };
        mesh.triangles = new int[] { 0, 1, 2, 0, 2, 3,0,3,4,0,4,5,0, 5, 6 };
        return mesh;

    }


    private void drawRect(int[,] typeCount,Color color, int barIndex)
    {

        for (int i = 0; i < typeCount.GetLength(0); i++)
        {
            
            int totalNum = 0;
            List<int> lineChartInfo = new List<int>();
            for(int j = 0; j < typeCount.GetLength(1); j++)
            {
                totalNum += typeCount[i,j];
                lineChartInfo.Add(typeCount[i, j]);
            }

            GameObject barfolder = new GameObject();
            barfolder.transform.SetParent(transform);
            barfolder.name = i + " folder";
            barfolder.AddComponent<FolderContoller>();


            float barWidth = 0.03f;
            GameObject rect = GameObject.CreatePrimitive(PrimitiveType.Cube);
            rect.GetComponent<MeshRenderer>().material.color = color;
            Bar bar= rect.AddComponent<Bar>();
            bar.num = totalNum;
            bar.year = i + 2012;
            bar.Panel = Panel;
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
            rect.transform.SetParent(barfolder.transform);
            float Y = 0.004f * totalNum;
            rect.transform.localScale = new Vector3(barWidth, Y, 0.003f);
           
            rect.transform.localPosition = rect.transform.localPosition + new Vector3(4* barWidth * i+ barWidth*barIndex, Y / 2, 0);


            GameObject lineChart = new GameObject("lineChart");
            lineChart.transform.SetParent(barfolder.transform);
            MeshFilter meshFilter=lineChart.AddComponent<MeshFilter>();
            meshFilter.mesh = CreateMesh(lineChartInfo);
            MeshRenderer meshRenderer = lineChart.AddComponent<MeshRenderer>();
            meshRenderer.material.color = Color.yellow;
            meshRenderer.material.shader = Shader.Find("UI/Default");
            lineChart.AddComponent<BoxCollider>();
            lineChart.transform.localPosition = lineChart.transform.localPosition + new Vector3(4 * barWidth * i + barWidth * barIndex, 0, 0);
        }



        //GameObject rectangle = new GameObject("rectangle");
        //rectangle.AddComponent<MeshFilter>();
        //rectangle.GetComponent<MeshFilter>().mesh = CreateMesh();//创建Mesh
        ////赋予Mesh一个UI/Default材质，颜色为白色，此材质不受灯光影响
        //MeshRenderer meshRenderer = rectangle.AddComponent<MeshRenderer>();
        //meshRenderer.material.shader = Shader.Find("UI/Default");
        //meshRenderer.material.color = Color.white;
    }
}
