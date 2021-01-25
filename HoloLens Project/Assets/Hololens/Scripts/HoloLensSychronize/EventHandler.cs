using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using ExitGames.Client.Photon;

namespace HoloLensinterface
{
    public class EventHandler : MonoBehaviourPun
    {
        public photonviewController photonviewController;
        public Transform WindowClip;
        public FreezeTrackingHandler freezeTrackingHandler;
        [SerializeField] private GameObject recalibrationMarker;
        // This script manage all the event that needs to be recieved from the monitor during the recalibration/adjustment process(currently useable in Map vis, possible to be used in other visualizations as well )

        private void OnEnable()
        {
            PhotonNetwork.NetworkingClient.EventReceived += NetworkingClient_EventReceived;
        }
        private void OnDisable()
        {
            PhotonNetwork.NetworkingClient.EventReceived -= NetworkingClient_EventReceived;
        }


        //[PunRPC]
        //void RPC_LoadNextVis(string visualizationnameTobeSet)
        //{
        //    photonviewController.setVisualization(visualizationnameTobeSet);
        //    Debug.Log("message recieved!");
        //}

        private void NetworkingClient_EventReceived(EventData obj)
        {
            if (obj.Code == Global.LOAD_LEVEL_EVENT)
            {
                // Load next task in Map vis
                object[] datas = (object[])obj.CustomData;

                string visualizationName = (string)datas[0];
                string visualization = (string)datas[1];
                photonviewController.setVisualization(visualizationName);
            }
            else if (obj.Code==Global.RESCALE_WINDOW)
            {
                //Rescale the cliped window.
                object[] datas = (object[])obj.CustomData;
                float xScale = (float)datas[0]/2;
                float yScale = (float)datas[1]/2;
                WindowClip.transform.localScale = new Vector3(WindowClip.transform.localScale.x * xScale, WindowClip.transform.localScale.y * yScale, WindowClip.transform.localScale.z);
            }
            else if (obj.Code == Global.START_RECALIBRATION)
            {
                //Start the recalibration process
                photonviewController.getCurrentVisualization().SetActive(false);
                Debug.Log("start recalibration");

            }
            else if (obj.Code == Global.REQUEST_RECALIBRATION)
            {
                //request recalibration, disable the visualization and give the camera permission to Vuforia
                Debug.Log("request recalibration");
                freezeTrackingHandler.Enable();
            }
            else if (obj.Code == Global.REQUEST_ADJUSTMENT)
            {
                //Request adjustment for slightly change.
                Debug.Log("request adjustment");
                recalibrationMarker.SetActive(true);
            }
            else if (obj.Code == Global.FINISH_RECALIBRATION)
            {
                Debug.Log("finish recalibration");
                recalibrationMarker.SetActive(false);
                Debug.Log(photonviewController.getCurrentVisualization().name);
                photonviewController.getCurrentVisualization().SetActive(true);
                Transform vistrans = photonviewController.getCurrentVisualization().transform;
                vistrans.Find("RecalibrationController").GetComponent<RecalibrationController>().FinishCalibartion();      


            }
        }
        }
}
