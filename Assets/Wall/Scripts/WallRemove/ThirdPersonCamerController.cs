﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamerController : MonoBehaviour
{

    public float moveSpeed = 1;

    public enum RotationAxes
    {
        MouseXAndY = 0,
        MouseX = 1,
        MouseY = 2
    }

    public RotationAxes m_axes = RotationAxes.MouseXAndY;
    public float m_sensitivityX = 10f;
    public float m_sensitivityY = 10f;

    // 水平方向的 镜头转向
    public float m_minimumX = -360f;
    public float m_maximumX = 360f;
    // 垂直方向的 镜头转向 (这里给个限度 最大仰角为45°)
    public float m_minimumY = -45f;
    public float m_maximumY = 45f;

    float m_rotationY = 0f;



    public float maxView = 90;
    public float minView = 10;

    [Range(0.0F, 10.0F)]
    public float scrollSensity = 3f;


    private Camera thirdPersonCamera;

    private void Start()
    {
        thirdPersonCamera=GetComponent<Camera>();
    }
    // Update is called once per frame
    void Update()
    {
        ProcessMovementInput();
        ProcessRotationInput();
        CameraViewInput();
    }


    private void ProcessMovementInput()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        //Get the value of the Horizontal input axis.

        float verticalInput = Input.GetAxis("Vertical");
        //Get the value of the Vertical input axis.

        transform.Translate(new Vector3(horizontalInput, 0, verticalInput) * moveSpeed * Time.deltaTime);
    }

    private void CameraViewInput()
    {
        float offsetView = -Input.GetAxis("Mouse ScrollWheel") * scrollSensity;
        float tmpView = offsetView + thirdPersonCamera.fieldOfView;
        tmpView = Mathf.Clamp(tmpView, minView, maxView);
        thirdPersonCamera.fieldOfView = tmpView;
    }

    private void ProcessRotationInput()
    {
        if (m_axes == RotationAxes.MouseXAndY)
        {
            float m_rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * m_sensitivityX;
            m_rotationY += Input.GetAxis("Mouse Y") * m_sensitivityY;
            m_rotationY = Mathf.Clamp(m_rotationY, m_minimumY, m_maximumY);

            transform.localEulerAngles = new Vector3(-m_rotationY, m_rotationX, 0);
        }
        else if (m_axes == RotationAxes.MouseX)
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * m_sensitivityX, 0);
        }
        else
        {
            m_rotationY += Input.GetAxis("Mouse Y") * m_sensitivityY;
            m_rotationY = Mathf.Clamp(m_rotationY, m_minimumY, m_maximumY);

            transform.localEulerAngles = new Vector3(-m_rotationY, transform.localEulerAngles.y, 0);
        }
    }

}
