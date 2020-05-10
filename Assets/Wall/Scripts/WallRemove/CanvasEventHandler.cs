using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WallRemote
{
    public class CanvasEventHandler : MonoBehaviour
    {
        public Camera cameraFirstPerson;
        public GameObject cameraThirdPerson;

        void Start()
        {
            cameraFirstPerson.enabled=true;
            cameraThirdPerson.SetActive(false);
        }

        public void OnChangeView_btn()
        {
            cameraThirdPerson.SetActive(!cameraThirdPerson.activeSelf);
            cameraFirstPerson.enabled = !cameraFirstPerson.enabled;
        }

        public void OnExit_btn()
        {
            Application.Quit();
        }
    }
}
