using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public GameObject Mapviz;
    private Canvas[] canvases;

    private void Start()
    {
        canvases = Mapviz.GetComponentsInChildren<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            foreach (Canvas canvas in canvases)
            {
                canvas.GetComponent<RectTransform>().localPosition += Vector3.down * 200 * Time.deltaTime;
            }
            
        }else if (Input.GetKeyDown(KeyCode.Z)){
            foreach (Canvas canvas in canvases)
            {
                canvas.GetComponent<RectTransform>().localPosition += Vector3.up * 200 * Time.deltaTime;
            }
        }
    }
}
