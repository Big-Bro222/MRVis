using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDebug : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(transform.parent.name + " is the parent name");
        Debug.Log(transform.localPosition + " is the localPosition");

    }
}
