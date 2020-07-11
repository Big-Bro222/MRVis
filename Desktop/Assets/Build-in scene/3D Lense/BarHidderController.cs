using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarHidderController : MonoBehaviour
{
    MeshRenderer meshrender;

    private void Start()
    {
        meshrender = GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ViewWindowQuad"))
        {
            meshrender.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ViewWindowQuad"))
        {
            meshrender.enabled = false;
        }
    }
}
