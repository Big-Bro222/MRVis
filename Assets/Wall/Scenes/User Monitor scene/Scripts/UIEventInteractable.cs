using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIEventInteractable : MonoBehaviour
{
    public enum UI
    {
        ToggleGroup,
        Button,
        RadioBtn,
        Slider
    }
    private int UIEventIndex;
    public UI UIState;
    public int UIid;
    private int curentToggleIndex=0;
    private UIEventInteractable[] UIEventInteractables;
    private ToggleGroup toggleGroup;
    private Toggle[] toggles;
    // Start is called before the first frame update
    private void OnEnable()
    {
        UIEventInteractables = transform.parent.GetComponentsInChildren<UIEventInteractable>();
        for(int i = 0; i < UIEventInteractables.Length; i++)
        {
            if (UIEventInteractables[i] == this)
            {
                UIEventIndex = i;
            }
        }

        switch (UIState)
        {
            case UI.ToggleGroup:
                toggles = GetComponentsInChildren<Toggle>();
                for (int i = 0; i < toggles.Length; i++)
                {
                    toggles[i].onValueChanged.AddListener((bool value) => InvokeToggleChange(value));
                }
                break;
            case UI.Button:
                GetComponent<Button>().onClick.AddListener(Clicked);
                break;
            default:
                Debug.LogError("No UI interactable Element found");
                break;

        }



    }
    #region Toogle method
    private int GetActiveToggle()
    {
        int index = 0;
        for(int j=0;j< toggles.Length;j++)
        {
            if (toggles[j].isOn)
            {
                index = j;
            }
        }
        return index;
    }
    private void InvokeToggleChange(bool value)
    {
        if (value)
        {
            curentToggleIndex = GetActiveToggle();
            Clicked();
        }
    }
    #endregion

    public Action <int,int> OnClicked;

    private void Clicked()
    {
        if (UIState == UI.ToggleGroup)
        {
            Debug.Log("Toggle clicked in UIEventInteractable!!!");
            OnClicked.Invoke(curentToggleIndex,UIEventIndex);
        }else if (UIState == UI.Button)
        {
            //button click is 999
            OnClicked.Invoke(999, UIEventIndex);
        }
    }


    private void OnDisable()
    {
        //Unregister for toggle
        if (UIState == UI.ToggleGroup)
        {
            for (int i = 0; i < toggles.Length; i++)
            {
                toggles[i].onValueChanged.RemoveAllListeners();
            }
        }
        //Unregister for Button
        else if (UIState == UI.Button)
        {
            GetComponent<Button>().onClick.RemoveAllListeners();
        }

    }

}
