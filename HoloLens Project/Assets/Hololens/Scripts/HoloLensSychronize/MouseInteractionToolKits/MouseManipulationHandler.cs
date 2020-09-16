using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality;
using Photon.Pun;
using Microsoft.MixedReality.Toolkit.UI;

[RequireComponent(typeof(MouseInteractable))]
public class MouseManipulationHandler : MonoBehaviour
{

    //HostTransform must be itself or it's parents
    public Transform HostTransform;
    public bool Scalable;
    public GameObject ScaleHandler;
    public float DistanceMin=0.2f;
    public float DistanceMax=3f;


    private List<Transform> VertextransformList;
    private Vector3 OriginalScale;
    private Vector3 FinalScale;
    private bool isMovable;
    private float MovingRadius;
    private BoundingBox boundingBox;


    //data from mouseEventCoreService
    private string currentEvent;
    private float MouseY;
    private enum TransformState
    {
        Default,
        Dragging,
        Scaling,
    }

    private TransformState transformState;
    void Start()
    {
        
        VertextransformList = new List<Transform>();

        //Register event from MouseEvent
        MouseEventCoreService.Instance.OnDoubleClicked += OnDoubleCliked;
        MouseEventCoreService.Instance.OnRightClicked += OnRightClicked;
        MouseEventCoreService.Instance.onScroll += onScroll;


        transformState = TransformState.Default;
        if (HostTransform == null)
        {
            HostTransform = transform;
        }

        //if (HostTransform.GetComponent<BoundingBox>())
        //{
        //    boundingBox = HostTransform.GetComponent<BoundingBox>();
        //}

        if (HostTransform.GetComponent<BoxCollider>())
        {
            Vector3[] VertextTransformArray = GetBoxColliderVertexPositions(HostTransform.GetComponent<BoxCollider>());
            GameObject Folder = Instantiate(new GameObject("VertexFolder"), HostTransform);
            foreach (Vector3 VertextTransformPosition in VertextTransformArray)
            {
                GameObject point = Instantiate(ScaleHandler, VertextTransformPosition, Quaternion.identity,Folder.transform);

                point.name = "Vertex";
                point.transform.parent = Folder.transform;
                VertextransformList.Add(point.transform);
            }
            HideHandler(true);
        }
        else
        {
            if (Scalable) { Debug.LogError("There is no box collider attached to the hostTransform"); }
            Scalable = false;
        }



        isMovable = false;
        OriginalScale = HostTransform.localScale;
        FinalScale = HostTransform.localScale;
    }

    private void HideHandler(bool hide)
    {

        if (HostTransform.GetComponent<BoundingBox>())
        {
            HostTransform.GetComponent<BoundingBox>().enabled = hide;
        }
        foreach(Transform Handlertransform in VertextransformList)
        {
            Handlertransform.gameObject.SetActive(!hide);
        }
    }

    private void OnDoubleCliked()
    {
        if (MouseEventCoreService.Instance.GazeTarget != transform.gameObject)
        {
            return;
        }
        OriginalScale = HostTransform.localScale;
        FinalScale = HostTransform.localScale;
        isMovable = !isMovable;
        HostTransform.localScale = isMovable ? OriginalScale * 1.2f : FinalScale;
        MovingRadius = isMovable ? Vector3.Distance(Camera.main.transform.position, transform.position) : 0;
        transformState = isMovable ? TransformState.Dragging : TransformState.Default;
    }
    private void OnRightClicked()
    {
        if (MouseEventCoreService.Instance.GazeTarget != transform.gameObject)
        {
            return;
        }
        if (isMovable & Scalable)
        {
            Debug.Log("MouseRight Clicked Recieved");
            if (transformState == TransformState.Dragging)
            {
                HideHandler(false);
                transformState = TransformState.Scaling;
            }
            else if (transformState == TransformState.Scaling)
            {
                HideHandler(true);
                transformState = TransformState.Dragging;
            }
        }
    }

    private void ScaleUpdateApperence()
    {
        Vector3[] VertextTransformArray = GetBoxColliderVertexPositions(HostTransform.GetComponent<BoxCollider>());
        for (int i=0;i< VertextransformList.Count; i++)
        {
            VertextransformList[i].position = VertextTransformArray[i];
        }
    }


    private Vector3[] GetBoxColliderVertexPositions(BoxCollider boxcollider)
    {
        //get eight point of the box collider;
        var vertices = new Vector3[8];
        //Lower four points
        vertices[0] = boxcollider.transform.TransformPoint(boxcollider.center + new Vector3(boxcollider.size.x, -boxcollider.size.y, boxcollider.size.z) * 0.5f);
        vertices[1] = boxcollider.transform.TransformPoint(boxcollider.center + new Vector3(-boxcollider.size.x, -boxcollider.size.y, boxcollider.size.z) * 0.5f);
        vertices[2] = boxcollider.transform.TransformPoint(boxcollider.center + new Vector3(-boxcollider.size.x, -boxcollider.size.y, -boxcollider.size.z) * 0.5f);
        vertices[3] = boxcollider.transform.TransformPoint(boxcollider.center + new Vector3(boxcollider.size.x, -boxcollider.size.y, -boxcollider.size.z) * 0.5f);
        //Upper four points
        vertices[4] = boxcollider.transform.TransformPoint(boxcollider.center + new Vector3(boxcollider.size.x, boxcollider.size.y, boxcollider.size.z) * 0.5f);
        vertices[5] = boxcollider.transform.TransformPoint(boxcollider.center + new Vector3(-boxcollider.size.x, boxcollider.size.y, boxcollider.size.z) * 0.5f);
        vertices[6] = boxcollider.transform.TransformPoint(boxcollider.center + new Vector3(-boxcollider.size.x, boxcollider.size.y, -boxcollider.size.z) * 0.5f);
        vertices[7] = boxcollider.transform.TransformPoint(boxcollider.center + new Vector3(boxcollider.size.x, boxcollider.size.y, -boxcollider.size.z) * 0.5f);

        return vertices;
    }


    private void onScroll(bool Delta)
    {
        if (MouseEventCoreService.Instance.GazeTarget != transform.gameObject)
        {
            return;
        }
        if (Delta)
        {
            HostTransform.Rotate(new Vector3(0, 0.1f * 180, 0), Space.Self);

        }
        else
        {
            HostTransform.Rotate(new Vector3(0, -0.1f * 180, 0), Space.Self);

        }
    }

    //public void OnTramsformMoving(float MouseYCalculated)
    //{
    //    MovingRadius = MovingRadius + MouseYCalculated;
    //}

    private void Update()
    {

        if (MouseEventCoreService.Instance.GazeTarget != transform.gameObject)
        {
            return;
        }
        currentEvent = MouseEventCoreService.Instance.currentEvent;
        MouseY = MouseEventCoreService.Instance.MouseY;

        if (isMovable)
        {
            //event setup in MouseEventHandler.cs
            if (transformState == TransformState.Dragging)
            {
                
                MovingRadius += MouseY;
            }
            else if (transformState == TransformState.Scaling)
            {
                FinalScale += new Vector3(MouseY, MouseY, MouseY);
                HostTransform.localScale = FinalScale;
            }

            //setup the closest distance&&farest distance
            if (MovingRadius >= DistanceMin&&MovingRadius<=DistanceMax)
            {
                Vector3 Direction = CoreServices.InputSystem.GazeProvider.GazeDirection.normalized;
                HostTransform.position = Camera.main.transform.position + Direction * MovingRadius;
                //HostTransform.rotation = Camera.main.transform.rotation;
            }
        }
    }

}
