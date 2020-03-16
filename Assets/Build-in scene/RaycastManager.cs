using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit;

public class RaycastManager : MonoBehaviour
{
    private GameObject focus;
    private GameObject prefocus;
    private FocusObj focusobj;
    public Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        focusobj = FindObjectOfType<FocusObj>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Maincamera is: "+ Camera.main.name);
        Debug.Log("WTFcamera is : " + CoreServices.CameraSystem.Name);
        RaycastHit[] hits;
        hits = Physics.RaycastAll(camera.transform.position, CoreServices.InputSystem.GazeProvider.GazeDirection.normalized , 100.0f);
        float mindist = 20;
        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            float dist = Vector3.Distance(hit.point, hit.transform.position);
            if (dist < mindist)
            {

                mindist = dist;
                focus = hit.transform.gameObject;

            }
        }
        bool Iswall=(focus.name.Equals("Wall"));
        
        if (focus!=null && focus.name.Equals("Wall"))
        {
            focusobj.setFocus(null, prefocus);
            prefocus = null;
        }
        else if (focus != prefocus)
        {
            focusobj.setFocus(focus,prefocus);
            prefocus = focus;
            
        }


        //Debug.DrawRay(Camera.main.transform.position, CoreServices.InputSystem.GazeProvider.GazeDirection.normalized, Color.blue);
    }
}
