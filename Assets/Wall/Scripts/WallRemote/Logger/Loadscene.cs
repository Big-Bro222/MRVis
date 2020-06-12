using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loadscene : MonoBehaviour
{
    public GameObject vis1;
    public GameObject vis2;
    public LocationLogger locationLogger;

    void Start()
    {
        
    }
    public void LoadScene()
    {
        LoadVisualization();
        locationLogger.OnVisChange();
        Debug.Log("Change2");
    }

    public void LoadVisualization()
    {
        Debug.Log("Change");
        vis1.SetActive(false);
        vis2.SetActive(true);
    }

    public void Load()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
