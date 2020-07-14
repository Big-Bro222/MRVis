using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality;
using Photon.Pun;
using ExitGames.Client.Photon;
using Photon.Realtime;
using System;

public class MouseEventCoreService : MonoBehaviourPun, IPunObservable, IPunOwnershipCallbacks
{
    public static MouseEventCoreService Instance;

    public GameObject GazeTarget;
    [Range(0.0f, 5.0f)]
    public float MovingSensity;

    public float MouseY;
    private bool isMoving = false;
    public string currentEvent;

    private enum FocusState
    {
        Focusing,
        Observing
    }
    private void Awake()
    {

        //singletons assign
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private FocusState focusState = FocusState.Observing;
    private void OnEnable()
    {
        PhotonNetwork.NetworkingClient.EventReceived += NetworkingClient_EventReceived;
    }
    private void OnDisable()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= NetworkingClient_EventReceived;
    }

    private void Start()
    {
        PhotonNetwork.SendRate = 60;
        PhotonNetwork.SerializationRate = 60;
    }


    #region Eventsetup
    public event Action OnDoubleClicked;
    public void DoubleClicked()
    {
        Debug.Log("DoubleClick");
        OnDoubleClicked();
    }

    public event Action OnRightClicked;
    public void RightClicked()
    {
        Debug.Log("MouseRight Clicked HoloLens");
        OnRightClicked();
    }

    public Action<int, string> OnToggleClicked;
    public void ToggleClicked(int ToggleIndex, string UIid)
    {
        OnToggleClicked(ToggleIndex, UIid);
    }

    public Action<string> OnButtonClicked;
    public void ButtonClicked(string UIid)
    {
        OnButtonClicked(UIid);
    }

    public Action<float, string> OnSliderChanged;
    public void SliderChanged(float sliderValue, string UIid)
    {
        Debug.Log(UIid);
        OnSliderChanged(sliderValue, UIid);
    }

    public Action<string> OnKeypressed;
    public void Keypressed(string Keycode)
    {
        OnKeypressed(Keycode);
    }

    #endregion


    //Input event handler
    private void NetworkingClient_EventReceived(EventData obj)
    {
        if (obj.Code == Global.DOUBLE_CLICKED)
        {
            object[] datas = (object[])obj.CustomData;

            if (!GazeTarget)
            {
                photonView.RPC("LogError", RpcTarget.All);
            }
            else
            {

                isMoving = !isMoving;
                DoubleClicked();
                focusState = isMoving ? FocusState.Focusing : FocusState.Observing;
            }
        }
        else if (obj.Code == Global.RIGHT_BTN_CLICKED)
        {
            if (!isMoving)
            {
                Debug.LogError("Not focusing on GameObject");
            }
            else
            {
                RightClicked();
            }
        }
        else if (obj.Code == Global.UI_BTN_CLICKED)
        {

            object[] datas = (object[])obj.CustomData;
            string UIstate = (string)datas[0];
            float UIrelatedData = (float)datas[1];
            string UIid = (string)datas[2];
            if (UIstate.Equals("ToggleGroup"))
            {
                int ToggleIndex = (int)UIrelatedData;
                ToggleClicked(ToggleIndex, UIid);
            }
            else if (UIstate.Equals("Button"))
            {
                ButtonClicked(UIid);
            }
            else if (UIstate.Equals("Slider"))
            {
                Debug.Log(UIid + "firsthand in HoloLens");
                SliderChanged(UIrelatedData, UIid);
            }
        }
        else if (obj.Code == Global.KEY_PRESSED)
        {
            object[] datas = (object[])obj.CustomData;
            string Keycode = (string)datas[0];
            Keypressed(Keycode);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (!isMoving)
        {
            //Update GazeTarget
            if (CoreServices.InputSystem.GazeProvider.GazeTarget)
            {
                if (CoreServices.InputSystem.GazeProvider.GazeTarget.GetComponent<MouseInteractable>())
                {
                    GazeTarget = CoreServices.InputSystem.GazeProvider.GazeTarget;
                }
                else
                {
                    GazeTarget = null;
                }

            }
            else
            {
                GazeTarget = null;
            }
        }


    }


    public event Action<bool> onScroll;

    [PunRPC]
    private void Scroll(bool Delta)
    {
        Debug.Log("Core" + Delta);
        onScroll(Delta);
    }

    //Error message sending back to monitor scene
    [PunRPC]
    private void LogError()
    {
        Debug.LogError("you are not focusing on GameObjects!");

    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (isMoving)
        {
            if (stream.IsReading)
            {
                MouseY = (float)stream.ReceiveNext() / MovingSensity;
                currentEvent = (string)stream.ReceiveNext();
            }
        }

    }

    public void OnOwnershipRequest(PhotonView targetView, Player requestingPlayer)
    {
        Debug.Log("requesting");
    }

    public void OnOwnershipTransfered(PhotonView targetView, Player previousOwner)
    {
        Debug.Log("Transfered");
    }
}
