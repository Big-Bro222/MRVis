using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Synchro
{
    public class TransformSynchroManager : SynchroManager
    {
        private TransformsStatusUpdate transformsStatusUpdate = new TransformsStatusUpdate();
        private bool isStopped = false;

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

            transformsStatusUpdate.Reset();
            bool hasChanged = false;
            foreach (KeyValuePair<string, ObjectOwnershipStatus> de in sharedItems)
            {
                GameObject gameObject = de.Value.obj;
                if (gameObject.transform.hasChanged)
                {
                    hasChanged = true;
                    gameObject.transform.hasChanged = false;
                    transformsStatusUpdate.AddChange(gameObject.name, gameObject.transform.localPosition,
                        gameObject.transform.localRotation, gameObject.transform.localScale);
                    
                }
            }
            
            if (hasChanged)
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