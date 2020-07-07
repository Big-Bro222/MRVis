using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InvisableInteractableToggle : MonoBehaviour
{

    public int UIid;
    public UnityEvent onSelectfirst;
    public UnityEvent onSelectSecond;
    public UnityEvent onSelectThird;
    public UnityEvent onSelectFourth;

    void Start()
    {
        MouseEventCoreService.Instance.OnToggleClicked += OnValueChanged;
    }

    private void OnValueChanged(int ToggleIndex, string uiId)
    {
        if (uiId.Equals(UIid))
        {
            switch (ToggleIndex)
            {
                case 0:
                    onSelectfirst.Invoke();
                    break;
                case 1:
                    onSelectSecond.Invoke();
                    break;
                case 2:
                    onSelectThird.Invoke();
                    break;
                case 3:
                    onSelectFourth.Invoke();
                    break;
                default:
                    Debug.LogError("Events number out of bounds");
                    break;

            }
        }
    }
}
