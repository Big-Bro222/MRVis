using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshContoller : MonoBehaviour
{
    bool isShow;
    void Start()
    {
        isShow = false;
    }

    public void MeshControl()
    {
        MeshRenderer[] meshRenderers = GetComponentsInChildren<MeshRenderer>();
        foreach(MeshRenderer meshRenderer in meshRenderers)
        {
            meshRenderer.enabled = isShow;
        }

        isShow = !isShow;
    }
}
