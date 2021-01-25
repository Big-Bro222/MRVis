using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCameraController : MonoBehaviour
{
    [Range(0.0F, 10.0F)]
    public float scrollSensity = 3f;

    public float maxView = 90;
    public float minView = 10;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CameraViewInput();
    }

    private void CameraViewInput()
    {
        if (GetComponent<Camera>())
        {
            float offsetView = -Input.GetAxis("Mouse ScrollWheel") * scrollSensity;
            float tmpView = offsetView + GetComponent<Camera>().fieldOfView;
            tmpView = Mathf.Clamp(tmpView, minView, maxView);
            GetComponent<Camera>().fieldOfView = tmpView;
        }
    }
}
