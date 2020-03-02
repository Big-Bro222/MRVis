using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class FreezeTrackingHandler : DefaultTrackableEventHandler
{
    public Transform Controller;
    public Transform CameraTransform;
    private bool recalibrating = false;

    protected override void OnTrackingFound()
    {
        base.OnTrackingFound();
        Debug.Log(Controller.name);
        Controller.parent = null;        
        CameraTransform.parent = Controller;
        CameraTransform.GetComponentInChildren<VuforiaMonoBehaviour>().enabled = false;
        this.enabled = false;
        
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
            mTrackableBehaviour.UnregisterTrackableEventHandler(this);
            
    }

    public void Enable(Transform controller)
    {
        Controller = controller;
        Debug.Log("Plop " + Controller.name);

        Controller.parent = this.transform;
        Controller.transform.localPosition = Vector3.zero;
        Controller.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);

        CameraTransform.parent = null;
        CameraTransform.GetComponentInChildren<VuforiaMonoBehaviour>().enabled = true;

        recalibrating = true;

        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
    }

    private void Update()
    {
        Debug.Log(" Tracker " + m_PreviousStatus.ToString() + " " + m_NewStatus.ToString());
    }

    public override void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        if(recalibrating && newStatus == TrackableBehaviour.Status.TRACKED)
        {            
            m_PreviousStatus = previousStatus;
            m_NewStatus = newStatus;
            recalibrating = false;
            return;
        }

        base.OnTrackableStateChanged(previousStatus, newStatus);
    }
}
