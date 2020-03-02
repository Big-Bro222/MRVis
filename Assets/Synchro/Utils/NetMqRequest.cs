using System;
using System.Threading;
using NetMQ;
using UnityEngine;
using NetMQ.Sockets;
using System.Diagnostics;
using System.Collections.Concurrent;
using Debug = UnityEngine.Debug;
using Object = System.Object;


namespace Synchro
{
	

	public class NetMqRequest : MonoBehaviour
	{
		public int IpPort = 12345;
		public string IpAddress = "127.0.0.1";
		public long ContactThreshold = 1000;

		//Publish
		private Thread requestThread;
		private RequestSocket reqSocket;
		private Object listenerLock_ = new Object();

		private bool cancelled;

		private Stopwatch contactWatch;
		[HideInInspector]public bool Connected;
		private readonly ConcurrentQueue<byte[]> messageQueue = new ConcurrentQueue<byte[]>();

		public static int ThreadSleepTime = 15; 
		
		protected virtual void Start()
		{
			contactWatch = new Stopwatch();
			cancelled = false;
			requestThread = new Thread(PublisherWork);
			requestThread.Start();
		}

		
		private void PublisherWork()
		{
			using (reqSocket = new RequestSocket($"tcp://{IpAddress}:{IpPort}"))
			{
				while (!cancelled)
				{
					Connected = contactWatch.ElapsedMilliseconds < ContactThreshold;
					contactWatch.Restart();
					byte[] message;
					while (!messageQueue.IsEmpty)
					{
						if (messageQueue.TryDequeue(out  message))
						{
							Debug.Log($"Sending request from port :{IpPort} ");
							Debug.Log($"PublisherWork content {System.Text.Encoding.UTF8.GetString(message)}.");
							reqSocket.SendFrame(message);
							
							var response = reqSocket.ReceiveFrameString();
							Debug.Log($"Received response {response}");
						}
						else
						{
							break;
						}
					}
					
					Thread.Sleep(ThreadSleepTime);
				}
				reqSocket.Disconnect($"tcp://{IpAddress}:{IpPort}");
				reqSocket.Dispose();
			}
			
		}


		void OnApplicationQuit()
		{
			lock (listenerLock_) cancelled = true;
			requestThread.Join();			
		}

		
		public void Send(byte [] Content)
		{
			messageQueue.Enqueue(Content);
		}

	}
}
