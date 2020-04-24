using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageController : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject quadimg1;
    public GameObject quadimg2;
    private bool isImg1;
    void Start()
    {
        isImg1 = true;
    }
    
    public void imgChanger()
    {
        isImg1 = !isImg1;
        quadimg1.SetActive(isImg1);
        quadimg2.SetActive(!isImg1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
