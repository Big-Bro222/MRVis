using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit;

public class Bar : MonoBehaviour
{
    public int year;
    public int num;
    public string type;
    public GameObject Panel;
    
    void Start()
    {

    }

    public void SetPanel()
    {
        //Panel.transform.SetParent(transform);

       
        float yOffset = GetComponent<BoxCollider>().bounds.size.y;
        Panel.transform.position = transform.position + new Vector3(0, yOffset/2, 0);
        Transform target = Panel.transform.Find("Target");
        target.position = transform.position + new Vector3(0, yOffset / 2, 0);


        string text = "the year is: " + year + ",\r\n the number is: " + num+",\r\n the type is: "+type;
        Panel.GetComponentInChildren<TextMeshPro>().SetText(text);
    }

    // Update is called once per frame
    void Update()
    {
        if(CoreServices.InputSystem.GazeProvider.GazeTarget == gameObject)
        {
            
            SetPanel();
        }
    }
}
