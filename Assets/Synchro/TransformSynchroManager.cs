using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Synchro
{
    public class TransformSynchroManager : SynchroManager
    {
        public FocusObj focusObj;
        

        private TransformsStatusUpdate transformsStatusUpdate = new TransformsStatusUpdate();
        private InteractionCommand interactionCommand = new InteractionCommand();
        private bool isStopped = false;
        private GameObject focus = null;
        


        public override void Start()
        {
            
            base.Start();
            transformsStatusUpdate.owner = this.ownerId;
            NetworkUpdate += OnSynchroUpdate;
        }
        
        void OnSynchroUpdate(object sender, SynchroManager.SynchroEventArgs e)
        {
            if (isStopped)
                return;
            TransformUpdate();
            InteractionUpdate();
        }
        private void InteractionUpdate()
        {
            interactionCommand.Reset();
            bool hasInteracion = false;
            if (focusObj.GetFocus() != focus)
            {
                string[] focusNDefocus = new string[2];
                
                bool isWallItem = IsWallItem(focusObj.GetFocus()); ;


                focusNDefocus[0] = (focusObj.GetFocus() == null|| !IsWallItem(focusObj.GetFocus())) ? "null" : focusObj.GetFocus().name;
                focusNDefocus[1]= (focus == null||!IsWallItem(focus)) ? "null" : focus.name;
                Debug.Log(focusNDefocus[0]+" and "+focusNDefocus[1]);
                
                interactionCommand.AddChange(focusNDefocus, "OnHover");
                focus = focusObj.GetFocus();
                
                hasInteracion = true;
                
            }

            if (hasInteracion)
                SynchroServer.Instance.SendCommand(topic, interactionCommand);
        }

        private bool IsWallItem(GameObject testobj)
        {
            bool iswallitem = false;
            foreach (GameObject wallItem in wallItems)
            {
                if (testobj == wallItem)
                {
                    iswallitem = true;
                }
            }
            return iswallitem;
        }

        private void TransformUpdate()
        {
            transformsStatusUpdate.Reset();
            bool hasChangedtransforms = false;
            foreach (KeyValuePair<string, ObjectOwnershipStatus> de in sharedItems)
            {
                
                GameObject gameObject = de.Value.obj;
                if (gameObject.transform.hasChanged)
                {
                    hasChangedtransforms = true;
                    gameObject.transform.hasChanged = false;
                    transformsStatusUpdate.AddChange(gameObject.name, gameObject.transform.localPosition,
                        gameObject.transform.localRotation, gameObject.transform.localScale);

                }
            }

            if (hasChangedtransforms)
                SynchroServer.Instance.SendCommand(topic, transformsStatusUpdate);
        }


        public override void StartInteraction()
        {
            base.StartInteraction();
            isStopped = false;
        }

        public override void StopInteraction()
        {
            base.StopInteraction();
            isStopped = true;
        }
    }
}