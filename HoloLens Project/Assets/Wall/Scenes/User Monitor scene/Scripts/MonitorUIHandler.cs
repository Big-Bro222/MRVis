using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MonitorUI
{
    public class MonitorUIHandler : MonoBehaviour
    {

        
        public Slider MarkerSlider;
        public Transform Marker;
        public Transform Card;
        private float scale = 2;

        public void MarkerScaleUpdate()
        {
            scale = MarkerSlider.value*8;
            Marker.transform.localScale = new Vector3(scale, scale, scale);
            Card.transform.localScale = new Vector3(scale, scale, scale);
        }


        public void StoreScale()
        {
            Debug.Log("storescale" + scale);
            CalibrationData.ResolutionScale = scale;
        }
    }
}

