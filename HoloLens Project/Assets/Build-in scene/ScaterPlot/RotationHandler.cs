using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationHandler : MonoBehaviour
{
    Vector3 endRotation;
    float YRotation;

    void Awake()
    {
        endRotation = new Vector3(0,0,0);
        YRotation = endRotation.y;
    }


    public void Rotate()
    {
        YRotation -= 90;
        YRotation = YRotation % 360;
    }

    
    void Update()
    {
        Quaternion endRotationQ = Quaternion.Euler (new Vector3(endRotation.x, YRotation, endRotation.z));

        if (transform.localRotation.eulerAngles!= new Vector3(endRotation.x, YRotation, endRotation.z))
        {
            transform.localRotation = Quaternion.Lerp(transform.localRotation, endRotationQ, 0.2f);
        }
    }
}
