using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using System;

public class Node : MonoBehaviour
{
    // Start is called before the first frame update

    public string name;
    public string id;

    private TextMeshPro label;
    private GameObject nodeChild;


    void Start()
    {
        label = GetComponentInChildren<TextMeshPro>();
        label.SetText(gameObject.name);
        label.enabled = false;
        //label.gameObject.AddComponent<NodeInteractionController>();
        label.gameObject.AddComponent<LabelMovementManager>();
    }



    // Update is called once per frame
    void Update()
    {
        
    }

}
