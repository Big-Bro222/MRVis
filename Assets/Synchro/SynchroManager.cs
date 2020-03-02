using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System.Net.NetworkInformation;
using System.Net.Sockets;

using UnityEngine.EventSystems;

#if !UNITY_STANDALONE_LINUX
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using Microsoft.MixedReality.Toolkit;
#endif

#if WINDOWS_UWP
using Windows.Networking.Sockets;
using Windows.Networking.Connectivity;
using Windows.Networking;
using System.Linq;
#endif

namespace Synchro
{
    public enum ADDRESSFAM
    {
        IPv4, IPv6
    }

    public enum DeviceType
    {
        MASTER,
        WALL,
        HOLOLENS
    }

    public struct ObjectLockOwnership
    {
        public string objectName;
        public string owner;
    }

    public struct ObjectOwnershipStatus
    {
        public GameObject obj;
        public string prefabName;
        public string owner;
    }

    public class SynchroManager : Singleton<SynchroManager>
    {
        public DeviceType ownType;
        [ShowIf("CheckTypeHololens")]
        public GameObject Anchor;

        public bool CheckTypeHololens() { return ownType == DeviceType.HOLOLENS; }

        public float NetworkFps = 60;
        public string topic;        

        private float timeSinceLastNetworkUpdate = 0;
        private float networkFrameTime;


        public bool useVicon;
        public string ownerId;
        public List<int> fullOwners { get; private set; } = new List<int>() { 180, 133, 190 };
        public GameObject myGameObjectPosition; 

        public GameObject Wall;

        public GameObject[] startItems = new GameObject[] { };
        public GameObject[] wallItems = new GameObject[] { };        
        public GameObject[] otherItems = new GameObject[] { };

        protected Dictionary<string, ObjectOwnershipStatus> ownItems = new Dictionary<string, ObjectOwnershipStatus>();
        protected Dictionary<string, ObjectOwnershipStatus> sharedItems = new Dictionary<string, ObjectOwnershipStatus>();
        protected Dictionary<string, ObjectOwnershipStatus> foreignItems = new Dictionary<string, ObjectOwnershipStatus>();
        protected List<ObjectLockOwnership> lockedObjects = new List<ObjectLockOwnership>();

        private ReCalibrate r = new ReCalibrate();
        private EventSystem eventSystem;

#if !UNITY_STANDALONE_LINUX
        private IMixedRealityInputSystem inputSystem;
#endif

        private bool isInteractionOn = true;

        public bool standAlone = false;
        public Canvas WaitForReconnection;
        Ping ping;
        bool interupt = false;
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void OnBeforeSceneLoadRuntimeMethod()
        {
            MessagePack.Resolvers.CompositeResolver.RegisterAndSetAsDefault(
                // use generated resolver first, and combine many other generated/custom resolvers
                MessagePack.Resolvers.GeneratedResolver.Instance,

                // finally, use builtin/primitive resolver(don't use StandardResolver, it includes dynamic generation)
                MessagePack.Unity.UnityResolver.Instance,
                MessagePack.Resolvers.BuiltinResolver.Instance,
                MessagePack.Resolvers.AttributeFormatterResolver.Instance,
                MessagePack.Resolvers.PrimitiveObjectResolver.Instance,
                MessagePack.Resolvers.StandardResolver.Instance
            );
        }

        public virtual void Awake()
        {
            ownItems = new Dictionary<string, ObjectOwnershipStatus>();
            sharedItems = new Dictionary<string, ObjectOwnershipStatus>();
            foreignItems = new Dictionary<string, ObjectOwnershipStatus>();

            if (startItems.Length != 0)
            {
                Debug.Log(startItems);
                for (int i = 0; i < startItems.Length; i++)
                {
                    ownItems.Add(startItems[i].name, new ObjectOwnershipStatus { obj = startItems[i], prefabName = null, owner = null });
                }
            }

            if (wallItems != null)
            {
                for (int i = 0; i < wallItems.Length; i++)
                {
                    sharedItems.Add(wallItems[i].name, new ObjectOwnershipStatus { obj = wallItems[i], prefabName = null, owner = null });
                }
            }

            if (otherItems != null)
            {
                for (int i = 0; i < otherItems.Length; i++)
                {
                    foreignItems.Add(otherItems[i].name, new ObjectOwnershipStatus { obj = otherItems[i], prefabName = null, owner = null });
                }
            }
        }

        public virtual void Start()
        {
            networkFrameTime = 1.0f/NetworkFps;
            synchroEventArgs = new SynchroEventArgs();

            ownerId = GetOwnerId();            

            Debug.Log("MY SUPER IP " + GetLocalIp());

            eventSystem = EventSystem.current;
            ping = new Ping(ownerId);


            if (ownType == DeviceType.HOLOLENS)
            {
                GameObject following = Instantiate(Resources.Load<GameObject>("Person"));
                myGameObjectPosition = following;
                following.GetComponent<AuthorViewRights>().isGenerated = false;
                following.transform.parent = transform;
                following.transform.localScale = Vector3.one * 0.1f;
                Debug.Log("Scale put to 0.1f");
                following.name = "Op" + ownerId;
                sharedItems.Add(following.name, new ObjectOwnershipStatus { obj = following, prefabName = null, owner = ownerId });
                Lock(following.name);
                SynchroServer.Instance.SendCommand("H", new Register
                {
                    name = following.name,
                    owner = ownerId
                });

                if (!standAlone)
                    InvokeRepeating("TestConnection", 1f, 0.5f);
            }
            else if (ownType == DeviceType.MASTER)
            {
                InvokeRepeating("PingDevices", 1f, 0.5f);
            }
            

            r.owner = ownerId.ToString();
        }

        private void Update()
        {
            timeSinceLastNetworkUpdate += Time.deltaTime;
            if (timeSinceLastNetworkUpdate > networkFrameTime)
            {
                synchroEventArgs.ElapsedTime = timeSinceLastNetworkUpdate;
                NetworkUpdate?.Invoke(this,synchroEventArgs);
                timeSinceLastNetworkUpdate = 0;
                
            }

            //Debug.Log("own " + ownItems.Count + " - shared " + sharedItems.Count + " - foreign " + foreignItems.Count);

            /* TRY FOR TIMEOUT
            if(ownType == DeviceType.MASTER && Time.time > timeLastTOCheck + 1f)
            {
                MasterRedirection.Instance.CheckIfUp(Device_TimeOut);
                timeLastTOCheck = Time.time;
            }
            */
        }

        public event EventHandler<SynchroEventArgs> NetworkUpdate;

        public class SynchroEventArgs : EventArgs
        {
            public float ElapsedTime { get; set; }

        }
 
        private SynchroEventArgs synchroEventArgs;

        private void PingDevices()
        {
            if(!interupt)
                SynchroServer.Instance.SendCommand(topic, ping);
        }

        private void TestConnection()
        {
            float lastCmd = SynchroClient.Instance.lastCmd;
            if(isInteractionOn && Time.time - lastCmd > 1f)
            {
                StopInteraction();
                Debug.Log("Stopping Interaction");
            }
            else if(!isInteractionOn && Time.time - lastCmd <= 1f)
            {
                StartInteraction();
            }

            if(!isInteractionOn)
                Debug.Log("InteractionStopped");
        }

        public DeviceType IsDevice()
        {
            return ownType;
        }

        public string GetLocalIp()
        {           
            if (ownType == DeviceType.HOLOLENS)
            {
#if WINDOWS_UWP
                var icp = NetworkInformation.GetInternetConnectionProfile();

                if (icp?.NetworkAdapter == null) return null;
                var hostname =
                    NetworkInformation.GetHostNames()
                        .FirstOrDefault(
                            hn =>
                                hn.Type == HostNameType.Ipv4 &&
                                hn.IPInformation?.NetworkAdapter != null &&
                                hn.IPInformation.NetworkAdapter.NetworkAdapterId == icp.NetworkAdapter.NetworkAdapterId);

                // the ip address
                return hostname?.CanonicalName;

#elif UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
                ADDRESSFAM Addfam = ADDRESSFAM.IPv4;
                //Return null if ADDRESSFAM is Ipv6 but Os does not support it
                if (Addfam == ADDRESSFAM.IPv6 && !Socket.OSSupportsIPv6)
                {
                    return null;
                }

                string output = "";

                foreach (NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces())
                {
                    NetworkInterfaceType _type1 = NetworkInterfaceType.Wireless80211;
                    NetworkInterfaceType _type2 = NetworkInterfaceType.Ethernet;

                    if ((item.NetworkInterfaceType == _type1 || item.NetworkInterfaceType == _type2) && item.OperationalStatus == OperationalStatus.Up)
                    {
                        foreach (UnicastIPAddressInformation ip in item.GetIPProperties().UnicastAddresses)
                        {
                            //IPv4
                            if (Addfam == ADDRESSFAM.IPv4)
                            {
                                if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                                {
                                    output = ip.Address.ToString();
                                }
                            }

                            //IPv6
                            else if (Addfam == ADDRESSFAM.IPv6)
                            {
                                if (ip.Address.AddressFamily == AddressFamily.InterNetworkV6)
                                {
                                    output = ip.Address.ToString();
                                }
                            }
                        }
                    }
                }
                return output;
#else
                return "127.0.0.1";
#endif
            }
            else if(ownType == DeviceType.MASTER)
            {
                return "127.0.0.300";
            }
            else
            {
                return "127.0.0.0";
            }
        }

        private string GetOwnerId()
        {
            string ip = GetLocalIp();
            string[] ipNumbers = ip.Split('.');
            return ipNumbers[3];
        }         

        // Change access list Somehow not working when used, check why
        public bool OwnToShared(string name)
        {
            if(ownItems.TryGetValue(name, out ObjectOwnershipStatus o))
            {
                sharedItems.Add(name, o);
                ownItems.Remove(name);
                return true;
            }
            return false;
        }
        public bool SharedToOwn(string name)
        {
            if (sharedItems.TryGetValue(name, out ObjectOwnershipStatus o))
            {
                ownItems.Add(name, o);
                sharedItems.Remove(name);
                return true;
            }
            return false;
        }
        public bool ForeignToShared(string name)
        {
            if (foreignItems.TryGetValue(name, out ObjectOwnershipStatus o))
            {
                sharedItems.Add(name, o);
                foreignItems.Remove(name);
                o.obj.SetActive(true);
                return true;
            }
            return false;
        }
        public bool SharedToForeign(string name)
        {
            if (sharedItems.TryGetValue(name, out ObjectOwnershipStatus o))
            {
                foreignItems.Add(name, o);
                sharedItems.Remove(name);
                return true;
            }
            return false;
        }
        public bool OwnToForeign(string name)
        {
            if (ownItems.TryGetValue(name, out ObjectOwnershipStatus o))
            {
                foreignItems.Add(name, o);
                ownItems.Remove(name);
                return true;
            }
            return false;
        }
        public bool ForeignToOwn(string name)
        {
            if (foreignItems.TryGetValue(name, out ObjectOwnershipStatus o))
            {
                ownItems.Add(name, o);
                foreignItems.Remove(name);
                return true;
            }
            return false;
        }

        // change access list using previous permission
        public void ToOwn(string name, PrivacyState ps)
        {
            bool isFound = false;
            switch(ps){
                case PrivacyState.Foreign:
                    isFound = ForeignToOwn(name);
                    break;
                case PrivacyState.Private:
                    isFound = true;
                    break;
                case PrivacyState.Public:
                case PrivacyState.Shared:
                    isFound = SharedToOwn(name);
                    break;
            }
            if (!isFound)
                Debug.LogError("Object " + name + " not found for privacyState " + ps);
        }
        public void ToForeign(string name, PrivacyState ps)
        {
            bool isFound = false;
            switch (ps)
            {
                case PrivacyState.Foreign:
                    isFound = true;
                    break;
                case PrivacyState.Private:
                    isFound = OwnToForeign(name);
                    break;
                case PrivacyState.Public:
                case PrivacyState.Shared:
                    isFound = SharedToForeign(name);
                    break;
            }
            if (!isFound)
                Debug.LogError("Object " + name + " not found for privacyState " + ps);
        }
        public void ToShared(string name, PrivacyState ps)
        {
            bool isFound = false;
            switch (ps)
            {
                case PrivacyState.Foreign:
                    isFound = ForeignToShared(name);
                    break;
                case PrivacyState.Private:
                    isFound = OwnToShared(name);
                    break;
                case PrivacyState.Public:
                case PrivacyState.Shared:
                    isFound = true;
                    break;
            }
            if (!isFound)
                Debug.LogError("Object " + name + " not found for privacyState " + ps);
        }

        // Get Object
        public GameObject GetOwn(string name)
        {
            if (ownItems.TryGetValue(name, out ObjectOwnershipStatus o))
            {
                return o.obj;
            }
            else
            {
                return null;
            }
        }
        public GameObject GetShared(string name)
        {
            if (sharedItems.TryGetValue(name, out ObjectOwnershipStatus o))
            {
                return o.obj;
            }
            else
            {
                return null;
            }
        }
        public GameObject GetForeign(string name)
        {
            if (foreignItems.TryGetValue(name, out ObjectOwnershipStatus o))
            {
                return o.obj;
            }
            else
            {
                return null;
            }
        }
        public GameObject GetObject(string name)
        {
            GameObject obj = GetOwn(name);
            if (obj == null)
            {
                obj = GetShared(name);
                if(obj == null)
                {
                    obj = GetForeign(name);                    
                }                    
            }
            if (obj == null)
                Debug.LogError("Object " + name + " not found");

            return obj;
        }
        public GameObject GetObject(string name, PrivacyState ps)
        {
            GameObject obj = null;
            switch (ps)
            {
                case PrivacyState.Foreign:
                    obj = GetForeign(name);
                    break;
                case PrivacyState.Private:
                    obj = GetOwn(name);
                    break;
                case PrivacyState.Public:                                        
                case PrivacyState.Shared:
                    obj = GetShared(name);
                    break;
            }
            if (obj == null)
                Debug.LogError("Object " + name + " not found");

            return obj;
        }
        public void AddOwn(GameObject g)
        {
            ownItems.Add(g.name, new ObjectOwnershipStatus { obj = g, prefabName = null, owner = null });
        }
        public void AddShared(GameObject g)
        {
            sharedItems.Add(g.name, new ObjectOwnershipStatus { obj = g, prefabName = null, owner = null });
            }
        public void AddForeign(GameObject g)
        {
            foreignItems.Add(g.name, new ObjectOwnershipStatus { obj = g, prefabName = null, owner = null });
        }
        public void RemoveObject(string name)
        {
            GameObject obj = GetOwn(name);
            if (obj == null)
            {
                obj = GetShared(name);
                if (obj == null)
                {
                    obj = GetForeign(name);
                }
                else
                {
                    sharedItems.Remove(name);
                }
            } else
            {
                ownItems.Remove(name);
            }
            if (obj == null)
                Debug.LogError("Object " + name + " not found");
            else
            {
                foreignItems.Remove(name);
            }
        }

        public void Lock(string name)
        {
            lockedObjects.Add(new ObjectLockOwnership
            {
                objectName = name,
                owner = ownerId
            });
        }

        public bool Unlock(string name)
        {
            foreach(ObjectLockOwnership olo in lockedObjects)
            {
                if(olo.objectName == name && olo.owner == ownerId)
                {
                    lockedObjects.Remove(olo);
                    return true;
                }                  
            }
            return false;
        }

        public bool IsLocked(string name)
        {
            foreach(ObjectLockOwnership olo in lockedObjects)
            {
                if (olo.objectName == name)
                    return true;
            }
            return false;
        }

        public void SpawnObject(string name, string prefabName, string parentName, Vector3 position, Quaternion rotation, Vector3 scale)
        {
            SpawnObject msg = new SpawnObject
            {
                name = name,
                prefabName = prefabName,
                parentName = parentName,
                startPos = position,
                startRot = rotation,
                startScale = scale
            };
            
            //Create locally
            msg.Apply();
            
            //Send message
            SynchroServer.Instance.SendCommand(topic,msg);
        }

        public void TakeOwnership(string objectName, string owner)
        {
            GameObject g;

            if (ownType == DeviceType.MASTER || ownType == DeviceType.WALL)
            {
                Debug.Log(objectName);
                g = GetShared(objectName);
                if (g == null)
                {
                    Debug.LogWarning("<color=red>Object Called " + objectName + " unknown take</color>");
                    return;
                }

                SharedToForeign(objectName);
                g.GetComponent<VersioningNotifications>().Remove();
            }
            else if(ownType == DeviceType.HOLOLENS && ownerId != owner)
            {
#if WINDOWS_UWP || UNITY_EDITOR_WIN || WINDOWS_WSA
                g = GetShared(objectName);
                if (g == null)
                {
                    Debug.LogWarning("<color=red>Object Called " + objectName + " unknown take</color>");
                    return;
                }

                SharedToForeign(objectName);
                g.GetComponent<VersioningNotifications>().SetForeign(owner);                       
#endif
            }
        }

        public void MakePublic(string objectName, string owner)
        {
            GameObject g;

            if (ownType == DeviceType.MASTER || ownType == DeviceType.WALL)
            {
                Debug.Log(objectName);
                g = GetForeign(objectName);
                if (g == null)
                {
                    Debug.Log("<color=red>Object Called " + objectName + " unknown make public</color>");
                    return;
                }

                ForeignToShared(objectName);
                g.GetComponent<VersioningNotifications>().Add();
            }
            else if (ownType == DeviceType.HOLOLENS && ownerId != owner)
            {
#if WINDOWS_UWP || UNITY_EDITOR_WIN || WINDOWS_WSA

                g = GetForeign(objectName);
                if (g == null)
                {
                    Debug.Log("<color=red>Object Called " + objectName + " unknown make public</color>");
                    return;
                }

                // TODO :      
                ForeignToShared(objectName);
                g.GetComponent<VersioningNotifications>().SetPublic(owner);                       
#endif
            }
        }
        

        public void DestroyItem(string name)
        {
            GameObject item = GetObject(name);

            if(item == null)
            {
                Debug.LogError(" Item " + name + " to destroy do not exist");   
            }

            Destroy(item);
        }
        
        public void ReCalibrate()
        {
#if !UNITY_STANDALONE_LINUX
            Anchor.GetComponent<FreezeTrackingHandler>().enabled = true;
            Anchor.GetComponent<FreezeTrackingHandler>().Enable(this.transform);
#endif
        }


        public void OrderCalibration(string name)
        {
            r.target = name.Remove(0, 2);
            SynchroServer.Instance.SendCommand(topic, r);
        }
        
       
        public virtual void StopInteraction()
        {
#if !UNITY_STANDALONE_LINUX

            isInteractionOn = false;
            eventSystem = EventSystem.current;
            eventSystem.enabled = false;            
            MixedRealityServiceRegistry.TryGetService<IMixedRealityInputSystem>(out inputSystem);
            inputSystem.GazeProvider.Enabled = false;
            WaitForReconnection.gameObject.SetActive(true);
#endif
        }

        public virtual void StartInteraction()
        {
#if !UNITY_STANDALONE_LINUX

            WaitForReconnection.gameObject.SetActive(false);
            isInteractionOn = true;
            eventSystem.enabled = true;
            MixedRealityServiceRegistry.TryGetService<IMixedRealityInputSystem>(out inputSystem);
            inputSystem.GazeProvider.Enabled = true;
#endif
        }        
    }
}