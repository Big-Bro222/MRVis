using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorManager : MonoBehaviour
{
    public Transform Anchor;

    void Start()
    {
        Anchor.rotation = Global.wallRotation;
        Anchor.position = Global.wallPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
