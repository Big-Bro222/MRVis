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

    public Transform parentVisCollection;
    private int UIEventIndex;
    public UI UIState;
    public string UIid;
    private float curentToggleIndex=0;
    private float currentSliderValue = 1;
    private UIEventInteractable[] UIEventInteractables;
    private ToggleGroup toggleGroup;
    private Toggle[] toggles;
    // Start is called before the first frame update
    private void OnEnable()
    {
        UIEventInteractables = parentVisCollection.GetComponentsInChildren<UIEventInteractable>();
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
            case UI.Slider:
                GetComponent<Slider>().onValueChanged.AddListener((float sliderValue)=>ValueChanged(sliderValue));
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

    //public Action <int,int> OnClicked;

    public Action<float, int> OnClicked;

    //public Action<float> OnValueChanged;
    private void ValueChanged(float sliderValue)
    {
        currentSliderValue = sliderValue;
        Clicked();
    }

    private void Clicked()
    {
        if (UIState == UI.ToggleGroup)
        {
            //Debug.Log("Toggle clicked in UIEventInteractable!!!");
            OnClicked.Invoke(curentToggleIndex,UIEventIndex);
        }else if (UIState == UI.Button)
        {
            //button click is 999
            //Debug.Log("Clickinfor sending!");
            OnClicked.Invoke(999, UIEventIndex);
        }else if(UIState == UI.Slider)
        {
            Debug.Log(transform.parent.name);
            OnClicked.Invoke(currentSliderValue, UIEventIndex);
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
