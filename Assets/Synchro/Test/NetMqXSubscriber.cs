using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using NetMQ;
using UnityEngine;
using NetMQ.Sockets;
using Object = System.Object;


namespace Synchro
{
	public class NetMqXSubscriber : MonoBehaviour
	{
		public string IpAddress = "127.0.0.1";
		public int IpPort = 9091;
		public int HighWaterMark = 1000;
		
		//temp
		public String Topic = "";

		private Task subTask;
		private SubscriberSocket subSocket;
		private bool listenerCancelled;
		private readonly ConcurrentQueue<NetMqMessageEventArgs> messageQueue = new ConcurrentQueue<NetMqMessageEventArgs>();
		private readonly Object listenerLock_ = new Object();


		protected virtual void OnEnable()
		{
			listenerCancelled = false;
			
			subSocket = new SubscriberSocket();
			subSocket.Connect($"tcp://{IpAddress}:{IpPort}");
			subSocket.Options.ReceiveHighWatermark = HighWaterMark;
			subSocket.Subscribe(Topic);
			
			subTask = new Task(
				async() =>
				{
					await subscriberWork();
				},TaskCreationOptions.LongRunning
			);
			subTask.Start();

		}

		private void OnDisable()
		{
			lock (listenerLock_) listenerCancelled = true;
		}


		private Task subscriberWork()
		{
			String topic;
			Byte [] content;
			while (!listenerCancelled)
			{
				bool receivedTopic, receivedContent;
				receivedTopic = receivedContent = true;
				while (receivedTopic && receivedContent)
				{
					receivedTopic = subSocket.TryReceiveFrameString(out topic);
					receivedContent = subSocket.TryReceiveFrameBytes(out content);
					if (receivedTopic && receivedContent)
					{
						NetMqMessageEventArgs eventArgs = new NetMqMessageEventArgs();
						eventArgs.IpAddress = IpAddress;
						eventArgs.IpPort = IpPort;
						eventArgs.Topic = topic;
						eventArgs.Content = content;						
						messageQueue.Enqueue(eventArgs);
					}
				}

				Task.Delay(NetMqPublisher.ThreadSleepTime);
			}
			subSocket.Disconnect($"tcp://{IpAddress}:{IpPort}");
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

}
