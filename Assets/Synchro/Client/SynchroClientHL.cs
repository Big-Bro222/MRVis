using System;
using MessagePack;
using UnityEngine;

namespace Synchro
{
    [RequireComponent(requiredComponent: typeof(NetMqSubscriber))]
    public class SynchroClientHL : Singleton<SynchroClientHL>
    {
        private NetMqSubscriber sub;
        protected void Start()
        {
            sub = gameObject.GetComponent<NetMqSubscriber>();
            sub.MessageReceived += OnMessageReceived;
        }

        public override void OnDestroy()
        {
            sub.MessageReceived -= OnMessageReceived;
            base.OnDestroy();
        }

        void OnMessageReceived(object sender, NetMqMessageEventArgs e)
        {
            var cmd = MessagePackSerializer.Deserialize<ISynchroCommand>(e.Content);
            cmd.Apply();
        }
    }
}