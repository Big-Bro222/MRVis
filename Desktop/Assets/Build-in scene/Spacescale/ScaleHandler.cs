using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Microsoft.MixedReality.Toolkit;
using TMPro;
using Photon.Pun;
using ExitGames.Client.Photon;
using Photon.Realtime;

public class ScaleHandler : MonoBehaviour
{

    public GameObject viewWindow;
    public GameObject scale1;
    public GameObject scale2;
    public GameObject scale3;

    public GameObject camera1;
    public GameObject camera2;
    public GameObject camera3;
    public PhotonView pv;

    public List<GameObject> scales;

    public int scaleint;

    public TextMeshPro textMeshPro;

    public bool isLock;


    private float[] viewWindowScale;
    void Start()
    {
        isLock = false;
        scales.Add(scale1);
        scales.Add(scale2);
        scales.Add(scale3);
        scaleint = 3;
        viewWindowScale =new float[4]{ 0.28f,0.18f,0.077f,0.3f};
    }

    public void SetLock()
    {
        isLock = !isLock;

        if (pv.IsMine)
        {
            //reuse the method in Pyrimad Lens(Hierarchy), the name doesn't mean anything.
            PhotonNetwork.RaiseEvent(Global.DESTROY_WINDOW_EVENT, new object(), RaiseEventOptions.Default, SendOptions.SendReliable);
        }

    }

    public void OnScaleChange()
    {


        float viewWindowLocalscale = viewWindowScale[scaleint];
        viewWindow.transform.localScale = new Vector3(viewWindowLocalscale, viewWindowLocalscale, 1);
        if (scaleint == 3)
        {
            viewWindow.SetActive(false);
            foreach (GameObject scale in scales)
            {
                scale.SetActive(false);
            }
        }
        else
        {
            viewWindow.SetActive(true);
            scales[scaleint].SetActive(true);
            scales[scaleint].layer = 0;
            textMeshPro.fontSize = 36 + 20 * scaleint;

        }
        scaleint++;

        if (scaleint > 3)
        {
            scaleint = 0;
        }

        Debug.Log("OnScaleChange!!!");
        if (pv.IsMine)
        {
            //reuse the method in Pyrimad Lens(Hierarchy), the name doesn't mean anything.
            object[] datas = new object[] { };
            PhotonNetwork.RaiseEvent(Global.INSTANTIATE_EVENT, datas, RaiseEventOptions.Default, SendOptions.SendReliable);
            Debug.Log("HoloLens OnscaleChange");
        }

    }

    // Update is called once per frame
    void Update()
    {


        if (isLock)
        {
            return;
        }
        //if (pv.IsMine)
        //{
        //    viewWindow.transform.position = new Vector3(CoreServices.InputSystem.GazeProvider.HitPosition.x, CoreServices.InputSystem.GazeProvider.HitPosition.y, 0);
        //}

        camera1.transform.position = new Vector3(viewWindow.transform.position.x,viewWindow.transform.position.y,camera1.transform.position.z);
        camera2.transform.position = new Vector3(viewWindow.transform.position.x, viewWindow.transform.position.y, camera2.transform.position.z);
        camera3.transform.position = new Vector3(viewWindow.transform.position.x, viewWindow.transform.position.y, camera3.transform.position.z);


        scale1.transform.localPosition = new Vector3(viewWindow.transform.localPosition.x, viewWindow.transform.localPosition.y, -0.1f);
        scale2.transform.localPosition = new Vector3(viewWindow.transform.localPosition.x, viewWindow.transform.localPosition.y, -0.2f);
        scale3.transform.localPosition = new Vector3(viewWindow.transform.localPosition.x, viewWindow.transform.localPosition.y, -0.3f);

    }
}
