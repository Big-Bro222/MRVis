using UnityEngine;

namespace Synchro.Test
{
	public class NetMqTestSubscriber : NetMqSubscriber
	{

		void OnMessageReceived(object sender, NetMqMessageEventArgs e)
		{
			Debug.Log($"Receiving {e.Topic} -> {System.Text.Encoding.UTF8.GetString(e.Content)}.");

		}

		protected void Start()
		{
			MessageReceived += OnMessageReceived;
		}
	}
}
