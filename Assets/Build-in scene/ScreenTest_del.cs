using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenTest_del : MonoBehaviour
{
    public float r =2;
    public float w = 3;
    public float speed = 0.5f;
    
    float x=5;
    private float y = 5;
    
    void Update()
    {
        w += speed * Time.deltaTime;
        x = Mathf.Cos(w) * r;
        y = Mathf.Sin(w) * r;
        transform.localPosition=new Vector3(x,y,transform.localPosition.z);

    }
}
