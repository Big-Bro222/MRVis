using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WallRemote
{
    public class CameraRaycastManager : MonoBehaviour
    {
        public GameObject currentGazeGameObject;
        public Vector3 currentHitPoint;
        public Vector3 currnetRelativeHitPoint;

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, (1 << 11)))
            {
                currentGazeGameObject = hit.transform.gameObject;
                currentHitPoint = hit.point;
                currnetRelativeHitPoint = hit.transform.InverseTransformPoint(currentHitPoint);
            };

        }
    }
}
