using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionConstrain : MonoBehaviour
{

    public string axis;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.localPosition;     }

    // Update is called once per frame
    void Update()
    {
        
    }
}
