using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InvisiableInteractableButton : MonoBehaviour
{
    public string UIid;
    public UnityEvent onClick;


    void Start()
    {
        MouseEventCoreService.Instance.OnButtonClicked += OnClick;

    }

    private void OnClick(string uiId)
    {
        if (uiId.Equals(UIid))
        {
            Debug.Log("Invock UIid: " + uiId);
            onClick.Invoke();
        }
    }
}
