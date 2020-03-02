using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MessagePack;

namespace Synchro
{
    public class MessageType
    {
    }

    [MessagePackObject]
    public class SpatialStatus : ISynchroCommand
    {
        [Key(0)] public string name { get; set; }
        [Key(1)] public Vector3 pos { get; set; }
        [Key(2)] public Quaternion rot { get; set; }
        [Key(3)] public Vector3 scale { get; set; }


        public SpatialStatus()
        {
        }

        public SpatialStatus(string name, Vector3 pos, Quaternion rot, Vector3 scale)
        {
            this.name = name;
            this.pos = pos;
            this.rot = rot;
            this.scale = scale;
        }

        public void Apply()
        {
            GameObject current = SynchroManager.Instance.GetObject(name);
            if (SynchroManager.Instance.IsLocked(name))
                return;


            if (current == null)
            {
                Debug.LogError("Can't find " + name);
                return;
            }

            bool changeMemory = current.transform.hasChanged;

            current.transform.localPosition = pos;
            current.transform.localRotation = rot;
            current.transform.localScale = scale;
            current.transform.hasChanged = changeMemory;
        }

        public override string ToString()
        {
            return Time.time + " SpatialStatus " + name + " " + pos.ToString("F3").Replace(" ", "") + " " + rot.ToString().Replace(" ", "") + " " + scale.ToString().Replace(" ", "");
        }
    }

    [MessagePackObject]
    public class TransformsStatusUpdate : ISynchroCommand
    {
        [Key(0)] public string owner;
        [Key(1)] public List<string> names { get; set; } = new List<string>();
        [Key(2)] public List<Vector3> poses { get; set; } = new List<Vector3>();
        [Key(3)] public List<Quaternion> rots { get; set; } = new List<Quaternion>();
        [Key(4)] public List<Vector3> scales { get; set; } = new List<Vector3>();


        public TransformsStatusUpdate()
        {
        }

        public TransformsStatusUpdate(string owner, List<string> names, List<Vector3> poses, List<Quaternion> rots, List<Vector3> scales)
        {
            this.owner = owner;
            this.names = names;
            this.poses = poses;
            this.rots = rots;
            this.scales = scales;
        }

        public void AddChange(string name, Vector3 pos, Quaternion rot, Vector3 scale)
        {
            names.Add(name);
            poses.Add(pos);
            rots.Add(rot);
            scales.Add(scale);
        }

        public void Apply()
        {
            for (int i = 0; i < names.Count; i++)
            {
                GameObject current = SynchroManager.Instance.GetObject(names[i]);
                if (SynchroManager.Instance.IsLocked(names[i]))
                    continue;


                if (current == null)
                {
                    Debug.LogError("Can't find " + names[i]);
                    continue;
                }

                bool changeMemory = current.transform.hasChanged;

                current.transform.localPosition = poses[i];
                current.transform.localRotation = rots[i];
                current.transform.localScale = scales[i];
                current.transform.hasChanged = changeMemory;
            }
        }

        public override string ToString()
        {
            string totalMov = "";
            for (int i = 0; i < names.Count; i++)
            {
                totalMov += Time.time + " " + owner + " TransformStatusUpdate " + names[i] + " " + poses[i].ToString("F3").Replace(" ", "") + " " + rots[i].ToString().Replace(" ", "") + " " + scales[i].ToString().Replace(" ", "");
                if (i != names.Count - 1) totalMov += System.Environment.NewLine;
            }
            return totalMov;
        }

        public void Reset()
        {
            names.Clear();
            poses.Clear();
            rots.Clear();
            scales.Clear();
        }
    }
    
    [MessagePackObject]
    public class Register : ISynchroCommand
    {
        [Key(0)] public string name { get; set; }
        [Key(1)] public string owner { get; set; }


        public Register()
        {
        }

        public Register(string name, string owner)
        {
            this.name = name;
            this.owner = owner;
        }

        public void Apply()
        {
            if (owner == SynchroManager.Instance.ownerId)
                return;

            GameObject p = Object.Instantiate((GameObject)Resources.Load("Person"));
            p.transform.localScale = Vector3.one;
            p.GetComponent<PersonAuthorViewRights>().isGenerated = true;
            p.GetComponent<PersonAuthorViewRights>().selfColor = SynchroManager.Instance.fullOwners.IndexOf(int.Parse(owner));
            p.name = name;
            p.transform.parent = SynchroManager.Instance.transform;
            SynchroManager.Instance.AddShared(p);
        }

        public override string ToString()
        {
            return Time.time + " " + owner + " Register " + name;
        }
    }

    [MessagePackObject]
    public class UpdatePresence : ISynchroCommand
    {
        [Key(0)] public List<string> name { get; set; }
        [Key(1)] public string owner { get; set; }


        public UpdatePresence()
        {
        }

        public UpdatePresence(List<string> name, string owner)
        {
            this.name = name;
            this.owner = owner;
        }

        public void Apply()
        {
        }

        public override string ToString()
        {
            string nameList = "";
            foreach (string n in name) nameList += n + "-";
            nameList += "null";
            return Time.time + " " + owner + " UpdatePresence " + " " + nameList;
        }
    }

    [MessagePackObject]
    public class SpawnObject : ISynchroCommand
    {
        [Key(0)] public string owner;
        [Key(1)] public string name { get; set; }
        [Key(2)] public string prefabName { get; set; }
        [Key(3)] public string parentName { get; set; }
        [Key(4)] public Vector3 startPos { get; set; }
        [Key(5)] public Quaternion startRot { get; set; }
        [Key(6)] public Vector3 startScale { get; set; }

        [Key(7)] public int privacy;
        [Key(8)] public List<int> owners;


        public SpawnObject()
        {
        }

        public SpawnObject(string owner, string name, string prefabName, string parentName, Vector3 startPos, Quaternion startRot, Vector3 startScale, int privacy, List<int> owners)
        {
            this.owner = owner;
            this.name = name;
            this.prefabName = prefabName;
            this.parentName = parentName;
            this.startPos = startPos;
            this.startRot = startRot;
            this.startScale = startScale;
            this.privacy = privacy;
            this.owners = owners;
        }

        public void Apply()
        {
            if (SynchroManager.Instance.ownerId == owner)
                return;

            if (SynchroManager.Instance.IsDevice() == DeviceType.WALL && prefabName == "FloatingWindowQuad")
                prefabName = "FloatingWindowNetwork";

            GameObject g = Object.Instantiate((GameObject)Resources.Load(prefabName));
            g.GetComponent<AuthorViewRights>().isGenerated = true;

            if (parentName == null)
            {
                g.transform.parent = SynchroManager.Instance.transform;
            }
            else
            {
                g.transform.parent = SynchroManager.Instance.GetObject(parentName).transform;
            }
            g.transform.localPosition = startPos;
            g.transform.localRotation = startRot;
            g.transform.localScale = startScale;
            g.name = name;
            g.GetComponent<AuthorViewRights>().AnticipateStart();

            if (owners.Count > 0 && owners.Contains(int.Parse(SynchroManager.Instance.ownerId)))
            {
                switch (Permissions.PrivacyConverter(privacy))
                {
                    case PrivacyState.Shared:
                        g.GetComponent<AuthorViewRights>().RemoteMakeShared(owners);
                        break;
                    case PrivacyState.Public:
                        g.GetComponent<AuthorViewRights>().RemoteMakePublic();
                        break;
                    default:
                        Debug.LogError("Impossible case of Foreign or Private for changePermission with state : " + privacy);
                        break;
                }
            }
            else
            {
                g.GetComponent<AuthorViewRights>().RemoteMakeForeign();
            }
        }

        public override string ToString()
        {
            string ownerList = "";
            foreach (int o in owners) ownerList += o + "-";
            ownerList += "null";
            return Time.time + " " + owner + " SpawnObject " + name + " " + prefabName + " " + parentName + " " + startPos.ToString("F3").Replace(" ", "") + " " + startRot.ToString().Replace(" ", "") + " " + startScale.ToString().Replace(" ", "") + " " + privacy + " " + ownerList;
        }
    }

    [MessagePackObject]
    public class DeleteObject : ISynchroCommand
    {
        [Key(0)] public string owner { get; set; }
        [Key(1)] public string name { get; set; }


        public DeleteObject()
        {
        }

        public DeleteObject(string owner, string name)
        {
            this.owner = owner;
            this.name = name;
        }

        public void Apply()
        {
            if (SynchroManager.Instance.ownerId != owner)
                SynchroManager.Instance.DestroyItem(name);

        }

        public override string ToString()
        {
            return Time.time + " " + owner + " DeleteObject " + name;
        }
    }

    [MessagePackObject]
    public class ChangePermission : ISynchroCommand
    {
        [Key(0)] public string owner { get; set; }
        [Key(1)] public string objectName { get; set; }
        [Key(2)] public int permissionState { get; set; }
        [Key(3)] public List<int> owners { get; set; }


        public ChangePermission()
        {
        }

        public ChangePermission(string owner, string objectName, int permissionState, List<int> owners)
        {
            this.owner = owner;
            this.objectName = objectName;
            this.permissionState = permissionState;
            this.owners = owners;
        }

        public void Apply()
        {
            if (owner == SynchroManager.Instance.ownerId)
                return;

            GameObject obj = SynchroManager.Instance.GetObject(objectName);
            if (obj == null)
                return;

            if (SynchroManager.Instance.IsDevice() != DeviceType.HOLOLENS || owners.Contains(int.Parse(SynchroManager.Instance.ownerId)))
            {
                switch (Permissions.PrivacyConverter(permissionState))
                {
                    case PrivacyState.Shared:
                        obj.GetComponent<AuthorViewRights>().RemoteMakeShared(owners);
                        break;
                    case PrivacyState.Public:
                        obj.GetComponent<AuthorViewRights>().RemoteMakePublic();
                        break;
                    default:
                        Debug.LogError("Impossible case of Foreign or Private for changePermission with state : " + Permissions.PrivacyConverter(permissionState));
                        break;
                }
            }
            else
            {
                obj.GetComponent<AuthorViewRights>().RemoteMakeForeign();
            }
        }

        public override string ToString()
        {
            string ownersListed = "";
            foreach (int o in owners) ownersListed += o.ToString() + "-";
            ownersListed += "null";
            return Time.time + " " + owner + " ChangePermission " + objectName + " " + permissionState + " " + ownersListed;
        }
    }

    [MessagePackObject]
    public class ReCalibrate : ISynchroCommand
    {
        [Key(0)] public string owner { get; set; }
        [Key(1)] public string target { get; set; }

        public void Apply()
        {
            if(SynchroManager.Instance.ownerId == target)
            {
                SynchroManager.Instance.ReCalibrate();
            }
        }
    }

    [MessagePackObject]
    public class Ping : ISynchroCommand
    {
        [Key(0)] public string owner { get; set; }

        public Ping()
        {

        }

        public Ping(string ownerId)
        {
            owner = ownerId;
        }

        public void Apply()
        {
            
        }
    }
}