using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewHandler : MonoBehaviour
{
    public Camera camera;
    public GameObject quad;
    public GameObject viewWindowIndicator;
    public ViewWindowController viewWindowController;


    private RenderTexture cameraTexture;
    private Material cameraMaterial;

    private Vector3 quadstartScale;
    private Vector3 viewWindowScale;

    private float cameraSize;
    private float cameraPortXRatio;
    private float cameraPortYRatio;



    void Start()
    {
        cameraTexture = new RenderTexture(512, 512, 16);
        camera.targetTexture = cameraTexture;
        cameraMaterial = new Material(Shader.Find("Standard"));
        cameraMaterial.mainTexture = cameraTexture;
        quad.GetComponent<MeshRenderer>().material = cameraMaterial;

        quadstartScale = quad.transform.localScale;
        viewWindowScale = viewWindowIndicator.transform.localScale;
        cameraSize = viewWindowIndicator.transform.parent.GetComponentInChildren<Camera>().orthographicSize;
        cameraPortXRatio = viewWindowIndicator.transform.parent.GetComponentInChildren<Camera>().rect.width;
        cameraPortYRatio = viewWindowIndicator.transform.parent.GetComponentInChildren<Camera>().rect.height;



        //////setup the public variable mannually
        ////viewWindowController = gameObject.transform.parent.GetComponentInChildren<ViewWindowController>();
        //////camera = viewWindowController.viewWindowIndicator.transform.parent.GetComponentInChildren<Camera>();
        ////viewWindowIndicator = viewWindowController.viewWindowIndicator;
        ////transform.GetComponent<LineRendererUpdate>().pivot = viewWindowIndicator.transform;


        //cameraTexture = new RenderTexture(512, 512, 16);
        //camera.targetTexture = cameraTexture;
        //cameraMaterial = new Material(Shader.Find("Standard"));
        //cameraMaterial.mainTexture = cameraTexture;
        //quad.GetComponent<MeshRenderer>().material = cameraMaterial;

        //quadstartScale = quad.transform.localScale;
        //viewWindowScale = viewWindowIndicator.transform.localScale;
        //cameraSize = viewWindowIndicator.transform.parent.GetComponentInChildren<Camera>().orthographicSize;
        //cameraPortXRatio = viewWindowIndicator.transform.parent.GetComponentInChildren<Camera>().rect.width;
        //cameraPortYRatio = viewWindowIndicator.transform.parent.GetComponentInChildren<Camera>().rect.height;


        //////prepare for the next group
        ////viewWindowController.viewWindowIndicator = transform.Find("ViewWindowIndicator").gameObject;
        ////viewWindowController.viewWindow = transform.Find("ViewWindow").gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        float scale = quad.transform.localScale.x / quadstartScale.x;
     


        viewWindowIndicator.transform.localScale = new Vector3(scale * viewWindowScale.x, scale * viewWindowScale.y,viewWindowScale.z);
        viewWindowIndicator.transform.parent.GetComponentInChildren<Camera>().orthographicSize = scale * cameraSize;
    }
}
