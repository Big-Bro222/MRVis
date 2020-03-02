using System.Linq;
using UnityEngine;

namespace Synchro.Test
{
	public class NetMqTestPublisher : NetMqPublisher
	{
		public string Topic = "TOPIC";
		public string Message = "Hoyoyo !";

		private static int count = 0;
		
		private void Start()
		{			
			SynchroManager.Instance.NetworkUpdate += OnSynchroUpdate;
		}

		private void OnDestroy()
		{
			SynchroManager.Instance.NetworkUpdate -= OnSynchroUpdate;            
		}

		void OnSynchroUpdate(object sender, SynchroManager.SynchroEventArgs e)
		{
			Send(Topic,System.Text.Encoding.UTF8.GetBytes(Message + count++));
		}   
	}
}
