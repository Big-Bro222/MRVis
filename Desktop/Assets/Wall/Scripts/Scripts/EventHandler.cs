using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
namespace Monitor
{

    public class EventHandler : MonoBehaviour
    {


        public VisualizationController visualizationController;
        private PhotonView pv;
        private int CurrentVisualizationIndex;

        private void Start()
        {
            CurrentVisualizationIndex = 0;
            pv = GetComponent<PhotonView>();
        }
        public void LoadNextVis()
        {
            int num = visualizationController.visualizationCollection.Count;
            if (CurrentVisualizationIndex < num - 1)
            {
                CurrentVisualizationIndex++;
            }
            else
            {
                CurrentVisualizationIndex = 0;
            }

            string VisName = visualizationController.visualizationCollection[CurrentVisualizationIndex].name;
            //RPC_LoadNextVis(VisName);
            if (!pv.IsMine)
            {
                pv.RPC("RPC_LoadNextVis", RpcTarget.All, VisName);
            }
        }



        [PunRPC]
        void RPC_LoadNextVis(string visualizationnameTobeSet)
        {
            visualizationController.setVisualization(visualizationnameTobeSet);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
