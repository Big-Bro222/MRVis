using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleUpdater : MonoBehaviour
{
    public GameObject quad;
    public GameObject viewWindowMarker;


    private Vector3 quadstartScale;
    private Vector3 viewWindowScale;

    // Start is called before the first frame update
    void Start()
    {
        quadstartScale = quad.transform.localScale;
        viewWindowScale = viewWindowMarker.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        float scale = quad.transform.localScale.x / quadstartScale.x;



        viewWindowMarker.transform.localScale = new Vector3(scale * viewWindowScale.x, scale * viewWindowScale.y, viewWindowScale.z);

    }
}
