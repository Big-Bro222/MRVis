using System;
using System.Collections.Generic;
using System.IO;
using MessagePack;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Synchro
{
    [RequireComponent(requiredComponent: typeof(NetMqSubscriber))]
    public class MasterRedirection : Singleton<SynchroClient>
    {
        public Dictionary<string, float> presentPersons { get; set; } = new Dictionary<string, float>();
        public RectTransform IPList;
        
        private FileStream logFile;
        private StreamWriter log;

        [ReorderableList]
        public List<NetMqSubscriber> Subscribers;
        protected void Start()
        {
            logFile = new FileStream("./Cinematic.txt", FileMode.Create, FileAccess.Write);
            log = new StreamWriter(logFile);

            UpdateView();
            foreach (NetMqSubscriber sub in Subscribers)
                sub.MessageReceived += OnMessageReceived;

            InvokeRepeating("CheckOpConnection", 1f, 1f);
        }

        public override void OnDestroy()
        {
            log.Close();
            logFile.Close();
            foreach (NetMqSubscriber sub in Subscribers)
                sub.MessageReceived -= OnMessageReceived;
            base.OnDestroy();
        }

        void OnMessageReceived(object sender, NetMqMessageEventArgs e)
        {
            var cmd = MessagePackSerializer.Deserialize<ISynchroCommand>(e.Content);

            if (cmd.GetType() == typeof(Register))
            {
                log.WriteLine(cmd.ToString());

                Debug.Log(((Register)cmd).ToString() + "YOLO");
                cmd.Apply();
                presentPersons.Add(((Register)cmd).owner, Time.time);
                UpdateView();
                List<string> nameList = new List<string>(presentPersons.Keys);
                SynchroServer.Instance.SendCommand("M", new UpdatePresence() { name = nameList, owner = ((Register)cmd).owner });
                return;
            }                       
            
            if (cmd.GetType().Equals(typeof(TransformsStatusUpdate)))
            {
                TransformsStatusUpdate t = (TransformsStatusUpdate)cmd;
                presentPersons[t.owner] = Time.time;
            }

            log.WriteLine(cmd.ToString());

            SynchroServer.Instance.SendCommand("M", cmd);
            cmd.Apply();
        }

        private void CheckOpConnection()
        {
            foreach(int i in SynchroManager.Instance.fullOwners)
            {
                if (presentPersons.ContainsKey(i.ToString()) && (Time.time - presentPersons[i.ToString()]) > 1f)
                    Debug.Log(i + " is deconnected");
            }
        }

        void UpdateView()
        {
            List<int> owners = TransformSynchroManager.Instance.fullOwners;
            for (int i = 0; i < owners.Count; i++) {
                IPList.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().text = "Op" + owners[i].ToString();
                IPList.GetChild(i).GetComponentInChildren<Image>().color = (presentPersons.ContainsKey("Op" + owners[i].ToString()) ? Color.green : Color.red);
            }
        }

        public void CheckIfUp(float uptime)
        {
            List<int> owners = TransformSynchroManager.Instance.fullOwners;

            foreach (KeyValuePair<string, float> k in presentPersons)
            {
                string t = k.Key.Remove(0, 2);
                if (k.Value + uptime < Time.time)
                {                    
                    IPList.GetChild(owners.IndexOf(int.Parse(t))).GetComponentInChildren<Image>().color = Color.red;   
                } else
                {
                    IPList.GetChild(owners.IndexOf(int.Parse(t))).GetComponentInChildren<Image>().color = Color.green;
                }
            }
        }

        public void ReplayCommandSent(ISynchroCommand cmd)
        {
            if (cmd.GetType() == typeof(Register))
            {
                cmd.Apply();
                presentPersons.Add(((Register)cmd).name, Time.time);
                UpdateView();
                List<string> nameList = new List<string>(presentPersons.Keys);
                SynchroServer.Instance.SendCommand("M", new UpdatePresence() { name = nameList, owner = ((Register)cmd).owner });
                return;
            }

            cmd.Apply();
        }
    }
}