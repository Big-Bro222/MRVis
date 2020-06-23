using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InvisiableInteractableButton : MonoBehaviour
{
    public int UIid;
    public UnityEvent onClick;


    void Start()
    {
        MouseEventCoreService.Instance.OnButtonClicked += OnClick;

    }

    private void OnClick(int uiId)
    {
        if (uiId.Equals(UIid))
        {
            Debug.Log("Invock UIid: " + uiId);
            onClick.Invoke();
        }
    }
}
