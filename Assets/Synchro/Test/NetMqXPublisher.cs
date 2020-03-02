using NetMQ;
using UnityEngine;
using NetMQ.Sockets;
using System.Diagnostics;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Debug = UnityEngine.Debug;
using Object = System.Object;


namespace Synchro
{
	

	public class NetMqXPublisher : MonoBehaviour
	{
		public string IpAddress = "127.0.0.1";
		public int IpPort = 9090;
		public long ContactThreshold = 1000;
		public int HighWatermark = 1000;

		//Publish
		private Task pubTask;
		private PublisherSocket pubSocket;
		private Object listenerLock_ = new Object();

		private bool cancelled;

		private readonly ConcurrentQueue<NetMqMessage> messageQueue = new ConcurrentQueue<NetMqMessage>();
		
		protected virtual void OnEnable()
		{
			cancelled = false;
			pubSocket = new PublisherSocket();
			pubSocket.Connect($"tcp://{IpAddress}:{IpPort}");

			pubSocket.Options.SendHighWatermark = HighWatermark;

			pubTask = new Task(
				async() =>
				{
					await publisherWork();
				},TaskCreationOptions.LongRunning
			);
			pubTask.Start();
		}

		
		private void OnDisable()
		{
			lock (listenerLock_) cancelled = true;
		}
		
		private Task publisherWork()
		{
			while (!cancelled)
			{
				while (!messageQueue.IsEmpty)
				{
					Debug.Log($"Queue size {messageQueue.Count}");
					if (messageQueue.TryDequeue(out var message))
					{
						pubSocket.SendMoreFrame(message.Topic).SendFrame(message.Content);
						
						Debug.Log($"Sending {message.Topic} -> {System.Text.Encoding.UTF8.GetString(message.Content)}");
					}
					else
					{
						break;
					}
				}

				Task.Delay(NetMqPublisher.ThreadSleepTime);
			}
			pubSocket.Disconnect($"tcp://*:{IpPort}");	
			pubSocket.Dispose();

			return Task.CompletedTask;
		}


		void OnApplicationQuit()
		{
			lock (listenerLock_) cancelled = true;
		}

		
		public void Send(string Topic, byte [] Content)
		{
			messageQueue.Enqueue(new NetMqMessage(Topic,Content));
		}

	}
	

}
