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
    private void OnUIClicked(int ButtonIndex,int UIEventIndex)
    {
        if (uIEventInteractables[UIEventIndex].UIState == UIEventInteractable.UI.Button)
        {
            RaiseOnUiClickEvent("Button",ButtonIndex, uIEventInteractables[UIEventIndex].UIid);
        }
        else if (uIEventInteractables[UIEventIndex].UIState == UIEventInteractable.UI.ToggleGroup)
        {
            RaiseOnUiClickEvent("ToggleGroup", ButtonIndex, uIEventInteractables[UIEventIndex].UIid);
        }


    }

    private void RaiseOnUiClickEvent(string UIState, int ButtonIndex,int UIEventIndex)
    {
        object[] datas = new object[] {UIState,ButtonIndex,UIEventIndex };
        PhotonNetwork.RaiseEvent(Global.UI_BTN_CLICKED, datas, RaiseEventOptions.Default, SendOptions.SendReliable);
    }
}
