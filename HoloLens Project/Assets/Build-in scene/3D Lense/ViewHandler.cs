using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloLensPyrimad;

public class ViewHandler : MonoBehaviour
{
    public GameObject quad;
    public GameObject viewWindowMarker;
    private ViewWindowController viewWindowController;
    public List<GameObject> ChildWindows;
    public List<GameObject> ParentWindowsLine;


    private RenderTexture cameraTexture;
    private Material cameraMaterial;

    private Vector3 quadstartScale;
    //private Vector3 viewWindowScale;

    private float cameraSize;
    private new Camera camera;

    private Color lineRendererColor;
    private PhotonSynChroManager photonSynChroManager;
    private new string name ;

    void Start()
    {
        name = gameObject.name;
        //setup the public variable mannually
        viewWindowController = gameObject.transform.parent.GetComponentInChildren<ViewWindowController>();
        photonSynChroManager = gameObject.transform.parent.GetComponentInChildren<PhotonSynChroManager>();
        camera = transform.GetComponentInChildren<Camera>();
        camera.enabled = true;


        cameraTexture = new RenderTexture(512, 512, 16);
        camera.targetTexture = cameraTexture;
        cameraMaterial = new Material(Shader.Find("Standard"));
        cameraMaterial.mainTexture = cameraTexture;
        quad.GetComponent<MeshRenderer>().material = cameraMaterial;

        quadstartScale = quad.transform.localScale;
        //viewWindowScale = viewWindowMarker.transform.localScale;
        cameraSize = transform.GetComponentInChildren<Camera>().orthographicSize;



    }

    public void ShowHierarchy()
    {
        viewWindowController.ShowHierarchy(name);
        lineRendererColor = transform.GetComponent<LineRenderer>().material.color;
        transform.GetComponent<LineRenderer>().material.color = Color.red;
        foreach (GameObject parentWindow in ParentWindowsLine)
        {
            parentWindow.GetComponent<LineRenderer>().material.color = Color.red;
        }
    }


    public void HideHierarchy()
    {
        viewWindowController.HideHierarchy(name);
        transform.GetComponent<LineRenderer>().material.color = lineRendererColor;
        foreach (GameObject parentWindow in ParentWindowsLine)
        {
            parentWindow.GetComponent<LineRenderer>().material.color = lineRendererColor;
        }
    }


    public void DisableClip(bool clipable)
    {
        viewWindowController.clipable = !clipable;
    }

    private void OnDestroy()
    {
        
        photonSynChroManager.RemovesyncronizeObj(name);
        viewWindowController.RaiseDestroyWallEvent(name);
        if (ChildWindows.Count != 0)
        {
            foreach (GameObject ChildWindow in ChildWindows)
            {
                Destroy(ChildWindow);
                Destroy(ChildWindow.GetComponent<ViewHandler>().viewWindowMarker);
            }

        }


    }

    public void DestroyOnClick(bool down)
    {
        if (down)
        {
            viewWindowController.clipable = false;
        }
        else
        {
            Destroy(gameObject);
            Destroy(viewWindowMarker);
            viewWindowController.clipable = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float scale = quad.transform.localScale.x / quadstartScale.x;
        //viewWindowMarker.transform.localScale = new Vector3(scale * viewWindowScale.x, scale * viewWindowScale.y, viewWindowScale.z);
        transform.GetComponentInChildren<Camera>().orthographicSize = scale * cameraSize;
    }
}
