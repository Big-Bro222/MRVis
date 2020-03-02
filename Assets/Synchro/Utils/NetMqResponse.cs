using System;
using System.Collections.Concurrent;
using System.Threading;
using NetMQ;
using NetMQ.Monitoring;
using UnityEngine;
using NetMQ.Sockets;
using UnityEngine.Serialization;
using Object = System.Object;


namespace Synchro
{
	public class NetMqResponse : MonoBehaviour
	{
		public int IpPort = 12345;
		public int HighWaterMark = 1000;
		
		private Thread listener;
		private ResponseSocket responseSocket;
		private bool listenerCancelled;
		private readonly ConcurrentQueue<byte[]> messageQueue = new ConcurrentQueue<byte[]>();
		private Object listenerLock_ = new Object();


		protected virtual void Start()
		{
			listenerCancelled = false;
			listener = new Thread(ListenerWork);
			listener.Start();

		}

//		protected virtual void OnDestroy()
//		{
//			listenerCancelled = true;
//			listener.Join();
//		}

		private void ListenerWork()
		{
			using (responseSocket = new ResponseSocket())
			{
				responseSocket.Options.ReceiveHighWatermark = HighWaterMark;
				responseSocket.Bind($"tcp://*:{IpPort}");
				while (!listenerCancelled)
				{
					Byte [] content;
					if (responseSocket.TryReceiveFrameBytes(out content))
					{
						messageQueue.Enqueue(content);
						//Debug.Log($"Received {System.Text.Encoding.UTF8.GetString(content)}");
						Console.WriteLine($"Received {content}");
						responseSocket.SendFrame("OK");
					}
					else
						Console.WriteLine("No message received");

					Thread.Sleep(NetMqPublisher.ThreadSleepTime);
				}
				responseSocket.Disconnect($"tcp://*:{IpPort}");
				responseSocket.Dispose();
			}
		}
		
		void OnApplicationQuit()
		{
			lock (listenerLock_) listenerCancelled = true;
			listener.Join();
		}
		
		public void Update()
		{			
			while (!messageQueue.IsEmpty)
			{
				
				byte[] eventArgs;
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
		
		protected virtual void OnMessageReceived(byte[] e)
		{
			EventHandler<byte[]> handler = MessageReceived;
			handler?.Invoke(this, e);
		}
		
		public event EventHandler<byte[]> MessageReceived;


		
	}
	
}
