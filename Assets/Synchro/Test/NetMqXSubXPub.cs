using System.Threading.Tasks;
using NetMQ;
using UnityEngine;
using NetMQ.Sockets;
using System.Collections.Concurrent;


namespace Synchro
{
	public class NetMqXSubXPub : MonoBehaviour
	{
		public string IpAddress = "127.0.0.1";
		public int IpPortIn = 9090;
		public int IpPortOut = 9091;

		//Publish
		private Task proxyTask;

		private XPublisherSocket xPubSocket;
		private XSubscriberSocket xSubSocket;
		private Proxy proxy;	
		
		protected void OnEnable()
		{

			xSubSocket = new XSubscriberSocket($"@tcp://{IpAddress}:{IpPortIn}");
			xPubSocket = new XPublisherSocket($"@tcp://{IpAddress}:{IpPortOut}");
		
			proxy = new Proxy(xSubSocket, xPubSocket);
			
			
			proxyTask = new Task(
				async() =>
				{
					await proxyWork();
				},TaskCreationOptions.LongRunning
			);
			proxyTask.Start();
						
		}

		private Task proxyWork()
		{
			proxy.Start();
			return Task.CompletedTask;
		}


		protected void OnDisable()
		{
			proxy.Stop();
			xSubSocket.Disconnect($"@tcp://{IpAddress}:{IpPortIn}");
			xPubSocket.Disconnect($"@tcp://{IpAddress}:{IpPortOut}");
			
			xSubSocket.Dispose();
			xPubSocket.Dispose();
		}

	}
	

}
