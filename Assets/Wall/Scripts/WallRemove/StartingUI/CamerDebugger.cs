using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WallRemote
{
    public class CamerDebugger : MonoBehaviour
    {
        LineRenderer cameraDebugLine;
        void Start()
        {
            if (!GetComponent<LineRenderer>())
            {
                cameraDebugLine = gameObject.AddComponent<LineRenderer>();
            }
            else
            {
                cameraDebugLine = GetComponent<LineRenderer>();
            }
            cameraDebugLine.startWidth = 0.01f;
            cameraDebugLine.endWidth = 0.01f;
        }

        // Update is called once per frame
        void Update()
        {
            cameraDebugLine.SetPositions(new Vector3[2] {transform.position, transform.forward * 50000000 });

            
        }
    }
}
