using MessagePack;
using UnityEngine;

namespace Synchro.Test
{
    [MessagePackObject]
    public class TestCommand : ISynchroCommand
    {
        [Key(0)]
        public string MyParameter { get; set; }

        [Key(1)]
        public Vector3 MyPosition { get; set; }

        
        public void Apply()
        {
            Debug.Log(MyParameter);
            Debug.Log($"{MyPosition.x} {MyPosition.y} {MyPosition.z}");
        }

        public string GetCommandType()
        {
            return "Useless";
        }

        public string GetFocusObject()
        {
            return MyParameter;
        }
    }
    
    public class SynchroTestServer : MonoBehaviour
    {
        public bool IsPublisher;
        public string Topic = "S";

        private void Start()
        {
            if (IsPublisher)
                SynchroManager.Instance.NetworkUpdate += OnSynchroUpdate;
        }

        private void OnDestroy()
        {
            if (IsPublisher)
                SynchroManager.Instance.NetworkUpdate -= OnSynchroUpdate;            
        }

        void OnSynchroUpdate(object sender, SynchroManager.SynchroEventArgs e)
        {
            TestCommand cmd = new TestCommand();
            cmd.MyParameter = "Hoyoyo !";
            cmd.MyPosition = new Vector3(1,2,3);
            Debug.Log($"Sending {cmd.MyParameter} {cmd.MyPosition}");

            SynchroServer.Instance.SendCommand(Topic,cmd);   
        }        
    }
}