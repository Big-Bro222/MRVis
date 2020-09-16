using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class photonviewController : MonoBehaviourPun
{
    //the order should be same with the one in wall scene,for the sake of photon communication
    public List<GameObject> visualizationCollection;
    public string visualizationName;
    private Dictionary<string, GameObject> visualizationCollectionDictionary;
    private GameObject currentVisualization;

    private void Start()
    {

        AddVisualizations2ObservedComponents();
        //Set up visualizations
        visualizationCollectionDictionary = new Dictionary<string, GameObject>();

        foreach (GameObject visualization in visualizationCollection)
        {
            visualizationCollectionDictionary.Add(visualization.name, visualization);
            visualization.SetActive(false);
        }
        visualizationCollectionDictionary[visualizationName].SetActive(true);
        currentVisualization = visualizationCollectionDictionary[visualizationName];
    }

    private void AddVisualizations2ObservedComponents()
    {
        for (int i = 0; i < transform.parent.childCount; i++)
        {
            //Define visualizations: each visualization contains a photonSynChroManager component.
            //This component is responsible for synchronize game object positions using Photon
            //The sequence of the visualization needs to be the same with the one in HoloLens project and in Monitor project.
            PhotonSynChroManager photonSynChroManager = transform.parent.GetChild(i).GetComponentInChildren<PhotonSynChroManager>();
            if (photonSynChroManager)
            {
                if (photonView.ObservedComponents[0] == null)
                {
                    photonView.ObservedComponents[0] = photonSynChroManager;
                }
                else
                {
                    photonView.ObservedComponents.Add(photonSynChroManager);

                }
            }
        }
    }

    public void setVisualization(int i)
    {
        GameObject visualizationTobeSet = visualizationCollection[i];
        if (visualizationTobeSet != currentVisualization)
        {
            currentVisualization.SetActive(false);
            visualizationTobeSet.SetActive(true);
            currentVisualization = visualizationTobeSet;
            visualizationName = visualizationTobeSet.name;
        }
    }

    public void setVisualization(string visualizationName)
    {
        GameObject visualizationTobeSet = visualizationCollectionDictionary[visualizationName];
        if (visualizationTobeSet == currentVisualization)
        {
            visualizationName=visualizationTobeSet.name;
        }
        else
        {
            currentVisualization.SetActive(false);
            visualizationTobeSet.SetActive(true);
            currentVisualization = visualizationTobeSet;
            visualizationName = visualizationTobeSet.name;
        }

    }
}
