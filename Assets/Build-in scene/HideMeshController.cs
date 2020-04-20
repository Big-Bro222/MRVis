using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideMeshController : MonoBehaviour
{
    MeshRenderer[] Meshrenderers;
    bool isShow;

    void Start()
    {
        
        isShow = false;
    }

    public void MeshrenderController()
    {
        Meshrenderers = GetComponentsInChildren<MeshRenderer>();
        Debug.Log("show " + isShow);

        foreach(MeshRenderer meshRenderer in Meshrenderers)
        {
            meshRenderer.enabled = isShow;          
        }
        isShow = !isShow;
    }
}
