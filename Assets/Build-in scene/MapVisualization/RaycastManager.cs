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
    public Vector3 hitpos;
    // Start is called before the first frame update
    void Start()
    {
        focusobj = FindObjectOfType<FocusObj>();
        hitpos = new Vector2(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit[] hits;
        hits = Physics.RaycastAll(camera.transform.position, CoreServices.InputSystem.GazeProvider.GazeDirection.normalized, Mathf.Infinity, (1 << 11 | 1 << 12));
        float mindist = 20;
        for (int i = 0; i < hits.Length; i++)
        {

            RaycastHit hit = hits[i];
            if (!new Vector2(hit.point.x, hit.point.y).Equals(hitpos))
            {
                hitpos = hit.point;
                Debug.Log("hitpoint changed");
            }

            //if the hit point is on Annotation, don't calculate hitchange
            bool inLayerMaskAnnotation = hit.transform.gameObject.IsInLayerMask(1 << 12);
            if (inLayerMaskAnnotation)
            {
                return;
            }

            float dist = Vector3.Distance(hit.point, hit.transform.position);


            if (dist < mindist)
            {

                mindist = dist;

                focus = hit.transform.parent.gameObject;
                if (hit.transform.name == "Wall")
                {
                    focus = null;
                }

            }


        }

        if (focus != prefocus)
        {
            //Debug.Log(focus + " and "+prefocus);
            focusobj.SetFocus(focus, prefocus);
            prefocus = focus;

        }


        //Debug.DrawRay(Camera.main.transform.position, CoreServices.InputSystem.GazeProvider.GazeDirection.normalized, Color.blue);
    }
}
