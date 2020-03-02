using System;
using System.Collections.Concurrent;
using MessagePack;
using UnityEngine;

namespace Synchro
{
    [RequireComponent(requiredComponent: typeof(NetMqPublisher))]
    public class SynchroServer : Singleton<SynchroServer>
    {
        private NetMqPublisher pub;
        
        private readonly ConcurrentQueue<SynchroMessage> commandQueue = new ConcurrentQueue<SynchroMessage>();


        protected void Start()
        {
            pub = gameObject.GetComponent<NetMqPublisher>();                           
        }

        private void LateUpdate()
        {
            while (!commandQueue.IsEmpty)
            {
                if (commandQueue.TryDequeue(out var message))
                {
                    byte[] serialized = MessagePackSerializer.Serialize(message.Command);
                    pub.Send(message.Topic,serialized);
                }
                else
                {
                    break;
                }
            }
        }

        public void SendCommand(string topic, ISynchroCommand cmd)
        {            
            commandQueue.Enqueue(new SynchroMessage(topic, cmd));            
        }
    }
    
    
    public struct SynchroMessage
    {
        public string Topic;
        public ISynchroCommand Command;

        public SynchroMessage(string topic, ISynchroCommand cmd)
        {
            Topic = topic;
            Command = cmd;
        }

    }
}