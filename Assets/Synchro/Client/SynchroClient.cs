using System;
using System.Collections.Generic;
using MessagePack;
using NaughtyAttributes;
using UnityEngine;

namespace Synchro
{
    public class SynchroClient : Singleton<SynchroClient>
    {
        [ReorderableList]
        public List<NetMqSubscriber> Subscribers;
        public float lastCmd = 0f;

        protected void Start()
        {
            foreach (NetMqSubscriber sub in Subscribers)
               sub.MessageReceived += OnMessageReceived;
        }

        public override void OnDestroy()
        {
            foreach (NetMqSubscriber sub in Subscribers)
                sub.MessageReceived -= OnMessageReceived;
            base.OnDestroy();
        }

        void OnMessageReceived(object sender, NetMqMessageEventArgs e)
        {
            var cmd = MessagePackSerializer.Deserialize<ISynchroCommand>(e.Content);            
            
            lastCmd = Time.time;
            cmd.Apply();
        }

        public virtual void CheckIfUp(float uptime)
        {

        }
    }
}