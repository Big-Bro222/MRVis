using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SynChronizeAssignment : MonoBehaviour
{
    public PhotonSynChroManager photonSynChroManager;
    public Transform Bar;
    public List<GameObject> lineCharts;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < Bar.childCount; i++)
        {
            for(int j=0; j < Bar.GetChild(i).childCount; j++)
            {
                if (Bar.GetChild(i).GetChild(j).Find("lineChart"))
                {
                    GameObject lineChart = Bar.GetChild(i).GetChild(j).Find("lineChart").gameObject;
                    photonSynChroManager.AddsyncronizeObj("lineChart " + i + " , " + j, lineChart);
                    lineCharts.Add(lineChart);
                }

            }
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
