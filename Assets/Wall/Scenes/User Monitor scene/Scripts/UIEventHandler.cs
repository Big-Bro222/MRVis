using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEventHandler : MonoBehaviour
{
    public Transform Canvas;
    private UIEventInteractable[] uIEventInteractables;

    private void Start()
    {
        RegisterEvent();
    }

    public void UnregisterEvent()
    {
        foreach (UIEventInteractable uIEventInteractable in uIEventInteractables)
        {
            uIEventInteractable.OnClicked -= OnUIClicked;
        }
    }

    public void RegisterEvent()
    {
        uIEventInteractables = Canvas.GetComponentsInChildren<UIEventInteractable>();
        //subscribe all the UI buttons
        foreach (UIEventInteractable uIEventInteractable in uIEventInteractables)
        {
            uIEventInteractable.OnClicked += OnUIClicked;
        }
    }
    private void OnUIClicked(float UIrelatedValue,int UIEventIndex)
    {
        //UIrelatedValue, 999 is null
        if (uIEventInteractables[UIEventIndex].UIState == UIEventInteractable.UI.Button)
        {
            RaiseOnUiClickEvent("Button",UIrelatedValue, uIEventInteractables[UIEventIndex].UIid);
        }
        else if (uIEventInteractables[UIEventIndex].UIState == UIEventInteractable.UI.ToggleGroup)
        {
            RaiseOnUiClickEvent("ToggleGroup", UIrelatedValue, uIEventInteractables[UIEventIndex].UIid);
        }else if(uIEventInteractables[UIEventIndex].UIState == UIEventInteractable.UI.Slider)
        {
            RaiseOnUiClickEvent("Slider", UIrelatedValue, uIEventInteractables[UIEventIndex].UIid);
        }


    }

    private void RaiseOnUiClickEvent(string UIState, float UIrelatedValue, int UIEventIndex)
    {
        object[] datas = new object[] {UIState, UIrelatedValue, UIEventIndex };
        PhotonNetwork.RaiseEvent(Global.UI_BTN_CLICKED, datas, RaiseEventOptions.Default, SendOptions.SendUnreliable);
    }
}
