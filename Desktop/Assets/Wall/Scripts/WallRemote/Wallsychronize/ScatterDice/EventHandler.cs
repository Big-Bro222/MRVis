using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using ExitGames.Client.Photon;

namespace ScatterDice
{
    public class EventHandler : MonoBehaviour
    {
        public ScrollBehaviorHandler scrollBehaviorHandler;
        public DiceBehaviorHandler diceBehaviorHandler;
        

        private void OnEnable()
        {
            PhotonNetwork.NetworkingClient.EventReceived += NetworkingClient_EventReceived;
        }
        private void OnDisable()
        {
            PhotonNetwork.NetworkingClient.EventReceived -= NetworkingClient_EventReceived;
        }

        private void NetworkingClient_EventReceived(EventData obj)
        {
            if (obj.Code == Global.CHANGE_DIMENSION_EVENT)
            {
                object[] datas = (object[])obj.CustomData;

                string formerState = (string)datas[0];
                string presentStateToBe = (string)datas[1];
                string visualization = (string)datas[2];
                //set up dice rolling behavior
                diceBehaviorHandler.Translate(presentStateToBe);
                scrollViewLogUpdate(formerState, presentStateToBe);
            }
            else if (obj.Code == Global.SCROLLVIEW_LOG_EVENT)
            {
                object[] datas = (object[])obj.CustomData;

                string objectname = (string)datas[0];
                string eventname = (string)datas[1];
                string visualization = (string)datas[2];
                scrollViewLogUpdate(objectname, eventname);
            }

        }

        private void scrollViewLogUpdate(string name, string eventname)
        {
            scrollBehaviorHandler.TextEventReciever(name + " acting event: " + eventname);
        }
    }

}
