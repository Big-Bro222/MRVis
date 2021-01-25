using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableController : MonoBehaviour
{

    Interactable[] interactables;

    void Start()
    {
        interactables = GetComponentsInChildren<Interactable>();
        OnToggle(false);
    }

    public void OnToggle(bool select)
    {
        
        foreach(Interactable interactable in interactables)
        {
            interactable.enabled = select;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
