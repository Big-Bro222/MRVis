using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class photonviewController : MonoBehaviourPun
{
    public List<GameObject> visualizationCollection;
    public string visualizationName;
    private Dictionary<string, GameObject> visualizationCollectionDictionary;


    void Start()
    {
        visualizationCollectionDictionary = new Dictionary<string, GameObject>();

        for(int i = 0; i < transform.parent.childCount; i++)
        {
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

        foreach (GameObject visualization in visualizationCollection)
        {
            visualizationCollectionDictionary.Add(visualization.name, visualization);
            visualization.SetActive(false);
        }
        visualizationCollectionDictionary[visualizationName].SetActive(true);




    }

    // Update is called once per frame
    void Update()
    {

    }
}
