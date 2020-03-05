using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Synchro
{
    public class TransformSynchroManager : SynchroManager
    {
        private TransformsStatusUpdate transformsStatusUpdate = new TransformsStatusUpdate();
        private ColorUpdate colorUpdate = new ColorUpdate();
        private bool isStopped = false;
        private Dictionary<string, Color> previousColors = new Dictionary<string, Color>();

        public override void Start()
        {
            base.Start();
            transformsStatusUpdate.owner = this.ownerId;
            colorUpdate.owner = this.ownerId;
            NetworkUpdate += OnSynchroUpdate;
            InitialColorList();
        }
        
        void OnSynchroUpdate(object sender, SynchroManager.SynchroEventArgs e)
        {
            if (isStopped)
                return;
            TransformUpdate();
            ColorUpdate();


        }
      
        private void ColorUpdate()
        {
            colorUpdate.Reset();
            bool hasChangedcolor = false;
            foreach (KeyValuePair<string, ObjectOwnershipStatus> de in sharedItems)
            {
                GameObject gameObject = de.Value.obj;
                bool ColorChanged = (previousColors[de.Key] != gameObject.GetComponent<MeshRenderer>().material.color);
                if (ColorChanged)
                {
                    hasChangedcolor = true;
                    previousColors[de.Key] = gameObject.GetComponent<MeshRenderer>().material.color;
                    colorUpdate.AddChange(gameObject.name, gameObject.GetComponent<MeshRenderer>().material.color);

                }
            }

            if (hasChangedcolor)
                SynchroServer.Instance.SendCommand(topic, colorUpdate);
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

        private void InitialColorList()
        {
            foreach (KeyValuePair<string, ObjectOwnershipStatus> de in sharedItems)
            {
                GameObject gameObject = de.Value.obj;
                previousColors.Add(de.Key, gameObject.GetComponent<MeshRenderer>().material.color);
            }
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