using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualizationController : MonoBehaviourPun
{
    public UIEventHandler uIEventHandler;
    public List<GameObject> visualizationCollection;
    public string visualizationName;
    private Dictionary<string, GameObject> visualizationCollectionDictionary;
    private GameObject currentVisualization;
    private float scale;

    private void OnEnable()
    {
        //scale = CalibrationData.ResolutionScale;
        visualizationCollectionDictionary = new Dictionary<string, GameObject>();
        foreach (GameObject visualization in visualizationCollection)
        {
            visualizationCollectionDictionary.Add(visualization.name, visualization);
            visualization.SetActive(false);
        }
        visualizationCollectionDictionary[visualizationName].SetActive(true);
        currentVisualization = visualizationCollectionDictionary[visualizationName];
        Setscale();
        //currentVisualization.transform.localScale = new Vector3(scale, scale, scale);
    }

    public void Setscale()
    {
        scale = CalibrationData.ResolutionScale;
        currentVisualization.transform.localScale = new Vector3(scale, scale, scale);

    }

    public void setVisualization(int i)
    {
        
        scale = CalibrationData.ResolutionScale;
        GameObject visualizationTobeSet = visualizationCollection[i];
        if (visualizationTobeSet == currentVisualization)
        {

        }
        else
        {
            uIEventHandler.UnregisterEvent();
            currentVisualization.SetActive(false);
            visualizationTobeSet.SetActive(true);
            currentVisualization = visualizationTobeSet;
            currentVisualization.transform.localScale = new Vector3(scale, scale, scale);
            visualizationName = visualizationTobeSet.name;
            uIEventHandler.RegisterEvent();
        }
    }

    public void setVisualization(string visualizationName)
    {
        scale = CalibrationData.ResolutionScale;
        GameObject visualizationTobeSet = visualizationCollectionDictionary[visualizationName];
        if (visualizationTobeSet == currentVisualization)
        {
            visualizationName = visualizationTobeSet.name;
        }
        else
        {
            uIEventHandler.UnregisterEvent();
            currentVisualization.SetActive(false);
            visualizationTobeSet.SetActive(true);
            currentVisualization = visualizationTobeSet;
            currentVisualization.transform.localScale = new Vector3(scale, scale, scale);
            visualizationName = visualizationTobeSet.name;
            uIEventHandler.RegisterEvent();

        }

    }
}
