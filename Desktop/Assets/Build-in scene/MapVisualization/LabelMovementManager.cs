using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class LabelMovementManager : MonoBehaviour
{
    private float rotationSpeed = 0.01f;


    // Update is called once per frame
    void Update()
    {
        if (Camera.main)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Camera.main.transform.rotation, Time.time * rotationSpeed);
        }
    }
}
