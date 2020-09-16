using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MouseEventHandler : MonoBehaviourPun, IPunObservable
{
    // Start is called before the first frame update
    public TextMeshProUGUI ScrollState;
    public TextMeshProUGUI ScrollEvent;
    public TextMeshProUGUI TransformState;

    public GameObject MouseEventIndicator;
    public int clickedCount = 2;
    public float clickedInterval = 0.5f;
    private float lastClickedTime = 0;
    private float count = 0;
    private PhotonView pv;
    private float MouseY;
    private float MouseScroll;
    private string CurrentEvent;

    private enum State
    {
        Static,
        Moving,
        Scaling,
    }
    private State currentState;
    void Start()
    {
        //setup the Serialization rate.
        PhotonNetwork.SendRate = 60;
        PhotonNetwork.SerializationRate = 60;

        ScrollState.SetText("Interaction Off");
        ScrollEvent.SetText("Null");
        TransformState.SetText("Null");
        CurrentEvent = "null";
        currentState = State.Static;
        pv = GetComponent<PhotonView>();
    }
    private void OnLeftClicked()
    {
        float interval = Time.realtimeSinceStartup - lastClickedTime;
        if (interval <= clickedInterval)
        {
            count++;
            if (count == clickedCount - 1)
            {
                //Double Clicked Event
                OnDoubleClicked();
            }
        }
        else
        {
            count = 0;
        }
        lastClickedTime = Time.realtimeSinceStartup;

    }

    private void OnDoubleClicked()
    {
        GetComponent<PhotonView>().RequestOwnership();
        //Transform Event state machine
        if (currentState == State.Static)
        {
            ScrollState.SetText("Interaction On");
            TransformState.SetText("Moving");
            currentState = State.Moving;
            RaiseOnFocusEvent();
        }
        else
        {
            ScrollState.SetText("Interaction Off");
            TransformState.SetText("Null");
            currentState = State.Static;
            RaiseOnFocusEvent();
        }


    }

    void Update()
    {

        //reset MouseY to 0
        MouseY = 0;
        CurrentEvent = "null";

        if (Input.GetMouseButtonDown(0))
        {
            //Only working for double Click
            OnLeftClicked();
        }

        if (Input.GetMouseButtonDown(1))
        {
            // The default mode is moving mode.
            // Once an object in HoloLens is focused, the user will turn into Moving state
            // Use double click to switch between different states.
            if (currentState == State.Moving)
            {
                currentState = State.Scaling;
                TransformState.SetText("Scaling");

            }
            else if (currentState == State.Scaling)
            {
                currentState = State.Moving;
                TransformState.SetText("Moving");
            }
            RaiseRightClickEvent();
            Debug.Log("RightMouseClicked Monitor");
        }
        StateEventHandler();


    }






    private void RaiseOnFocusEvent()
    {
        object[] datas = new object[] { };
        PhotonNetwork.RaiseEvent(Global.DOUBLE_CLICKED, datas, RaiseEventOptions.Default, SendOptions.SendReliable);
    }

    private void RaiseRightClickEvent()
    {
        object[] datas = new object[] { };
        PhotonNetwork.RaiseEvent(Global.RIGHT_BTN_CLICKED, datas, RaiseEventOptions.Default, SendOptions.SendReliable);
    }

    private void StateEventHandler()
    {
        if (currentState == State.Static)
        {
            Debug.Log("there is no event happening");
            return;
        }
        else
        {
            ScrollEvent.SetText("Nothing Happening");
            if (Input.GetMouseButton(0))
            {
                CurrentEvent = "Dragging";
                MouseY = Input.GetAxis("Mouse Y");

                ScrollEvent.SetText("Dragging" + MouseY);
                if (currentState == State.Scaling)
                {
                    MouseEventIndicator.transform.localScale += new Vector3(MouseY/5, MouseY/5, MouseY/5);
                }
                else if (currentState == State.Moving)
                {
                    MouseEventIndicator.transform.Translate(new Vector3(0, MouseY / 10, 0));
                }
            }
            else if (Input.GetAxis("Mouse ScrollWheel") != 0)
            {
                bool Delta = true;
                CurrentEvent = "Scrolling";
                if(Input.GetAxis("Mouse ScrollWheel") < 0)
                {
                    Delta = false;
                }
                MouseScroll = Input.GetAxis("Mouse ScrollWheel");
                pv.RPC("Scroll", RpcTarget.All,Delta);
                ScrollEvent.SetText("Scrolling" + MouseScroll);

            }

        }

    }


    [PunRPC]
    private void Scroll(bool Delta)
    {
        MouseEventIndicator.transform.Rotate(new Vector3(0, MouseScroll * 180, 0));
    }

    [PunRPC]
    private void LogError()
    {
        ScrollState.SetText("No object focusing!!!");
        Invoke("setText", 1);
        TransformState.SetText("Null");
        currentState = State.Static;
    }

    private void setText()
    {
        ScrollState.SetText("Interaction Off");
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

        
        if (currentState == State.Moving||currentState==State.Scaling)
        {
            if (stream.IsWriting)
            {

                stream.SendNext(MouseY);
                stream.SendNext(CurrentEvent);
            }
        }

    }
}
