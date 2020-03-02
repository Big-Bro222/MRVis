using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using NetMQ;
using NetMQ.Monitoring;
using UnityEngine;
using NetMQ.Sockets;
using UnityEngine.Serialization;
using Object = System.Object;


namespace Synchro
{
	public class NetMqSubscriber : MonoBehaviour
	{
		public int IpPort = 12345;
		public int HighWaterMark = 1000;
		public bool isHost; 
		public string HostIpAddress = "localhost";
		
		//temp
		public String Topic = "";

		private Task subTask;
		private SubscriberSocket subSocket;
		private bool listenerCancelled;
		private readonly ConcurrentQueue<NetMqMessageEventArgs> messageQueue = new ConcurrentQueue<NetMqMessageEventArgs>();
		private Object listenerLock_ = new Object();


		protected virtual void OnEnable()
		{
			listenerCancelled = false;

			subSocket = new SubscriberSocket();
			subSocket.Options.ReceiveHighWatermark = HighWaterMark;
			if (isHost)
				subSocket.Bind($"tcp://*:{IpPort}");
			else
				subSocket.Connect($"tcp://{HostIpAddress}:{IpPort}");
			subSocket.Subscribe(Topic);
			
			subTask = new Task(
				async() =>
				{
					await subscriberWork();
				},TaskCreationOptions.LongRunning
			);
			subTask.Start();

		}

//		protected virtual void OnDestroy()
//		{
//			listenerCancelled = true;
//			listener.Join();
//		}

		private Task subscriberWork()
		{
			while (!listenerCancelled)
			{
				NetMqMessageEventArgs eventArgs = new NetMqMessageEventArgs();
				eventArgs.IpAddress = HostIpAddress;
				eventArgs.IpPort = IpPort;
                string topic = subSocket.ReceiveFrameString();
				eventArgs.Topic = Topic;
				eventArgs.Content = subSocket.ReceiveFrameBytes();
				messageQueue.Enqueue(eventArgs);
			}
			subSocket.Disconnect($"tcp://{HostIpAddress}:{IpPort}");
			subSocket.Dispose();
			return Task.CompletedTask;
		}
		
		void OnApplicationQuit()
		{
			lock (listenerLock_) listenerCancelled = true;
		}
		
		public void Update()
		{
			while (!messageQueue.IsEmpty)
			{				
				NetMqMessageEventArgs eventArgs;
				if (messageQueue.TryDequeue(out eventArgs))
				{
					OnMessageReceived(eventArgs);
				}
				else
				{
					break;
				}
			}
		}
		
		protected virtual void OnMessageReceived(NetMqMessageEventArgs e)
		{
			EventHandler<NetMqMessageEventArgs> handler = MessageReceived;
			handler?.Invoke(this, e);
		}
		
		public event EventHandler<NetMqMessageEventArgs> MessageReceived;


		
	}
	
	public class NetMqMessageEventArgs : EventArgs
	{
		public byte[] Content { get; set; }
		public string Topic { get; set; }
		public string IpAddress { get; set; }
		public int IpPort { get; set; }

	}
}
