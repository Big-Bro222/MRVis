using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;

public class TransformSynchronizeCameraContoller : MonoBehaviourPun,IPunObservable
{

    public PhotonView pv;

    private float moveSpeed = 1;

    private Transform mixRealityPlaySpace;
    public Transform Controller;

    private Vector3 smoothMove;
    private Quaternion smoothRotate;

    

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            //ProcessInput();
        }
        else
        {
            smoothMovement();
            smoothRotation();
        }
    }

    private void smoothMovement()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, smoothMove, Time.deltaTime * 10);
    }

    private void smoothRotation()
    {
        transform.localRotation = Quaternion.Lerp(transform.localRotation, smoothRotate, Time.deltaTime * 5);
    }

    //private void ProcessInput()
    //{
    //    float horizontalInput = Input.GetAxis("Horizontal");
    //    //Get the value of the Horizontal input axis.

    //    float verticalInput = Input.GetAxis("Vertical");
    //    //Get the value of the Vertical input axis.

    //    transform.Translate(new Vector3(horizontalInput, 0, verticalInput) * moveSpeed * Time.deltaTime);
    //}

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //Transfer the absolute transforms to a local transform.
        Vector3 cameraWorldPos = transform.position;
        Vector3 cameraControllerPos = Controller.transform.InverseTransformPoint(cameraWorldPos);

        Quaternion cameraWorldRotate = transform.rotation;
        Quaternion cameraControllerRotation = Quaternion.Inverse(Controller.transform.rotation) * cameraWorldRotate;

        if (stream.IsWriting)
        {
            stream.SendNext(cameraControllerPos);
            stream.SendNext(cameraControllerRotation);
        }
        else if (stream.IsReading)
        {
            smoothMove = (Vector3)stream.ReceiveNext();
            smoothRotate = (Quaternion)stream.ReceiveNext();
        }
    }
}
