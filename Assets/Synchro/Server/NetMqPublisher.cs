using System;
using System.Threading;
using NetMQ;
using UnityEngine;
using NetMQ.Sockets;
using System.Diagnostics;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using NetMQ.Monitoring;
using Debug = UnityEngine.Debug;
using Object = System.Object;


namespace Synchro
{
	

	public class NetMqPublisher : MonoBehaviour
	{
		public int IpPort = 12345;
		public long ContactThreshold = 1000;
		public bool isHost;
		public string HostIpAddress;

		//Publish
		private Task pubTask;
		private PublisherSocket pubSocket;
		private Object listenerLock_ = new Object();

		private bool cancelled;

		private readonly ConcurrentQueue<NetMqMessage> messageQueue = new ConcurrentQueue<NetMqMessage>();

		public static int ThreadSleepTime = 15; 
		
		protected virtual void OnEnable()
		{
			cancelled = false;
			pubSocket = new PublisherSocket();
			if (isHost)
				pubSocket.Bind($"tcp://*:{IpPort}");
			else
				pubSocket.Connect($"tcp://{HostIpAddress}:{IpPort}");

			pubTask = new Task(
				async() =>
				{
					await PublisherWork();
				},TaskCreationOptions.LongRunning
			);
			pubTask.Start();
		}

		void OnDisable()
		{
			lock (listenerLock_) cancelled = true;
		}
		
		private Task PublisherWork()
		{
			while (!cancelled)
			{
				while (!messageQueue.IsEmpty)
				{
					if (messageQueue.TryDequeue(out var message))
					{
						pubSocket.SendMoreFrame(message.Topic).SendFrame(message.Content);

						//Debug.Log($"Sending Topic:{message.Topic} content {System.Text.Encoding.UTF8.GetString(message.Content)}.");
					}
					else
					{
						break;
					}
				}
				
				Task.Delay(ThreadSleepTime);
			}
			pubSocket.Disconnect($"tcp://*:{IpPort}");
			pubSocket.Dispose();
			return Task.CompletedTask;

		}




		
		public void Send(string Topic, byte [] Content)
		{
			messageQueue.Enqueue(new NetMqMessage(Topic,Content));
		}

	}
	
	public class NetMqMessage
	{
		public NetMqMessage(string topic, byte [] content)
		{
			Topic = topic;
			Content = content;
		}
		public byte [] Content { get; }
		public string Topic { get; }
	}
}
