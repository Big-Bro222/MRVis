using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Microsoft.MixedReality.Toolkit;

public class LabelRotationHandler : MonoBehaviour
{
    // Start is called before the first frame update

    private TextMeshProUGUI lable;
    private GameObject canvas;
    private Quaternion cameraRotation;
    private Quaternion startPointRotation;
    private float rotationSpeed = 0.01f;

    void Start()
    {
        lable = GetComponentInChildren<TextMeshProUGUI>();
        canvas = lable.transform.parent.gameObject;
        InitialRotationInfo();
    }

    private void InitialRotationInfo()
    {
        startPointRotation = transform.rotation;
        cameraRotation = Camera.main.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if(cameraRotation != Camera.main.transform.rotation)
        {
            InitialRotationInfo();
        }
        transform.rotation = Quaternion.Lerp(startPointRotation,cameraRotation,Time.time*rotationSpeed);
    }
}
