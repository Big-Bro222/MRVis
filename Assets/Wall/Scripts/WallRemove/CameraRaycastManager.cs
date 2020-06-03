using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WallRemote
{
    public class CameraRaycastManager : MonoBehaviour
    {
        public GameObject currentGazeGameObject;
        public Vector3 currentExtrudeHitPoint;
        public Vector3 currnetRelativeHitPoint;
        public Vector3 currentHitPoint;

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            RaycastHit Etrudehit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out Etrudehit, Mathf.Infinity, (1 << 11)))
            {
                currentGazeGameObject = Etrudehit.transform.gameObject;

                currentExtrudeHitPoint = Etrudehit.point;
                currnetRelativeHitPoint = Etrudehit.transform.InverseTransformPoint(currentExtrudeHitPoint);
            }

            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
            {
                currentHitPoint = hit.point;
            }
            else {
                currentHitPoint = new Vector3(1000,1000,1000);
                //set currentHitPoint to a insane value to show it's null
            };

        }
    }
}
