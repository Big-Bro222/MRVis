using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshrenderHiddenHandler : MonoBehaviour
{
    private int count;

    // Start is called before the first frame update
    void Start()
    {
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        count++;
        if (count < 20)
        {
            MeshRenderer[] meshrenderers = GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer meshRenderer in meshrenderers)
            {
                meshRenderer.enabled = false;
            }
            Debug.Log(count);
        }
        else
        {
            count = 20;
            Debug.Log("20");
        }

    }
}
