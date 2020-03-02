namespace Synchro.Test
{
	public class NetMqTestRequest : NetMqRequest
	{
		private void Update()
		{
			Send(System.Text.Encoding.UTF8.GetBytes("My content"));
		}
	}
}
