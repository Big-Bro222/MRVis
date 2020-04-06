using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScaterPlotParser : MonoBehaviour
{
    private List<string> csvParseData;
    private List<Dictionary<string, string>> csvDataList;
    private GameObject text;
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

        DrawBarChart();


    }

    private void DrawBarChart()
    {

        drawAxis();
    }

    private void drawAxis()
    {
        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.positionCount = 5;

        lineRenderer.startWidth = 0.01f;
        lineRenderer.endWidth = 0.01f;
        lineRenderer.useWorldSpace = false;
        Vector3[] pos = new Vector3[5] { new Vector3(1, 0, 0),
                                         new Vector3(0, 0, 0),
                                         new Vector3(0, 1, 0),
                                         new Vector3(0, 0, 0),
                                         new Vector3(0, 0, -1)};
        lineRenderer.SetPositions(pos);


        //for (int i = 1; i < 9; i++)
        //{
        //    GameObject txt = Instantiate(text, new Vector3(0.03f * (i * 4), -0.05f, 0), Quaternion.identity, transform);
        //    txt.GetComponent<TextMeshPro>().SetText((i + 2011).ToString());
        //    txt.name = (i + 2011).ToString();

        //}


    }

    private void drawRect(int[] typeCount, Color color, int barIndex)
    {

        for (int i = 0; i < typeCount.Length; i++)
        {

            float barWidth = 0.03f;
            GameObject rect = GameObject.CreatePrimitive(PrimitiveType.Cube);
            rect.GetComponent<MeshRenderer>().material.color = color;
            Bar bar = rect.AddComponent<Bar>();
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
            rect.transform.localScale = new Vector3(barWidth, 0.001f * typeCount[i], 0.003f);
            float yOffset = 0.001f * typeCount[i];
            rect.transform.localPosition = rect.transform.localPosition + new Vector3(4 * barWidth * i + barWidth * barIndex, yOffset / 2, 0);
        }

    }
}
