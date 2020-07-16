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


        private void OnEnable()
        {
            PhotonNetwork.NetworkingClient.EventReceived += NetworkingClient_EventReceived;
        }
        private void OnDisable()
        {
            PhotonNetwork.NetworkingClient.EventReceived -= NetworkingClient_EventReceived;
        }


        [PunRPC]
        void RPC_LoadNextVis(string visualizationnameTobeSet)
        {
            photonviewController.setVisualization(visualizationnameTobeSet);
            Debug.Log("message recieved!");
        }

        private void NetworkingClient_EventReceived(EventData obj)
        {
            if (obj.Code == Global.LOAD_LEVEL_EVENT)
            {
                object[] datas = (object[])obj.CustomData;

                string visualizationName = (string)datas[0];
                string visualization = (string)datas[1];
                photonviewController.setVisualization(visualizationName);
            }
            else if (obj.Code==Global.RESCALE_WINDOW)
            {
                object[] datas = (object[])obj.CustomData;
                float xScale = (float)datas[0]/2;
                float yScale = (float)datas[1]/2;
                WindowClip.transform.localScale = new Vector3(WindowClip.transform.localScale.x * xScale, WindowClip.transform.localScale.y * yScale, WindowClip.transform.localScale.z);
            }
            else if (obj.Code == Global.START_RECALIBRATION)
            {
                photonviewController.getCurrentVisualization().SetActive(false);
                Debug.Log("start recalibration");

            }
            else if (obj.Code == Global.REQUEST_RECALIBRATION)
            {
                Debug.Log("request recalibration");
                freezeTrackingHandler.Enable();
            }
            else if (obj.Code == Global.REQUEST_ADJUSTMENT)
            {
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
                //To do: find another way to set active
                //vistrans.Find(photonviewController.getCurrentVisualization().name).gameObject.SetActive(true);           


            }
        }
        }
}
