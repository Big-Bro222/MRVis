using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.Text;

public class PhotonSynChroManager : MonoBehaviourPun, IPunObservable
{

    public List<GameObject> syncronizeObjs;
    public PhotonView pv;
    public GameObject gameObjectTobedestroy;

    private List<Vector3> syncronizeObjLocalpositionList = new List<Vector3>();
    private List<Quaternion> syncronizeObjLocalrotationList=new List<Quaternion>();
    private List<Vector3> syncronizeObjLocalscaleList=new List<Vector3>();

    private Vector3[] StreamObjLocalpositionArray;
    private Quaternion[] StreamObjLocalrotationArray;
    private Vector3[] StreamObjLocalscaleArray;

    private Dictionary<string, GameObject> syncronizeObjsDictionary;




    void Awake()
    {
        syncronizeObjsDictionary = new Dictionary<string, GameObject>();
        syncronizeObjLocalpositionList = new List<Vector3>();
        for (int i = 0; i < syncronizeObjs.Count; i++)
        {
            syncronizeObjsDictionary.Add(syncronizeObjs[i].name, syncronizeObjs[i]);
            syncronizeObjLocalpositionList.Add(syncronizeObjs[i].transform.localPosition);
        }


        syncronizeObjLocalrotationList = new List<Quaternion>();
        for (int i = 0; i < syncronizeObjs.Count; i++)
        {
            syncronizeObjLocalrotationList.Add(syncronizeObjs[i].transform.localRotation);
        }

        syncronizeObjLocalscaleList = new List<Vector3>();
        for (int i = 0; i < syncronizeObjs.Count; i++)
        {
            syncronizeObjLocalscaleList.Add(syncronizeObjs[i].transform.localScale);
        }


        StreamObjLocalpositionArray = new Vector3[] { };
        StreamObjLocalrotationArray = new Quaternion[] { };
        StreamObjLocalscaleArray = new Vector3[] { };

    }

    public GameObject GetGameObjectByName(string name)
    {
        GameObject obj = syncronizeObjsDictionary[name];
        return obj;
    }

    public void AddsyncronizeObj(string name, GameObject obj)
    {
        syncronizeObjsDictionary.Add(name, obj);
        syncronizeObjs.Clear();
        foreach(GameObject syncronizeObj in syncronizeObjsDictionary.Values)
        {
            syncronizeObjs.Add(syncronizeObj);
        }
    }

    public void RemovesyncronizeObj(string name)
    {
        gameObjectTobedestroy = syncronizeObjsDictionary[name];
        syncronizeObjsDictionary.Remove(name);
        syncronizeObjs.Clear();
        foreach (GameObject syncronizeObj in syncronizeObjsDictionary.Values)
        {
            syncronizeObjs.Add(syncronizeObj);
        }
    }


    void Update()
    {
        if (pv.IsMine)
        {
            //ProcessInput();
        }
        else
        {
            smoothMovement();
            smoothRotation();
            smoothScale();
        }
    }

    //serialize Vetor3 Array which I didn't use
    public static string SerializeVector3Array(Vector3[] aVectors)
    {
        StringBuilder sb = new StringBuilder();
        foreach (Vector3 v in aVectors)
        {
            sb.Append(v.x).Append(" ").Append(v.y).Append(" ").Append(v.z).Append("|");
        }
        if (sb.Length > 0) // remove last "|"
            sb.Remove(sb.Length - 1, 1);
        return sb.ToString();
    }

    //deserialize Vetor3 Array which I didn't use
    public static Vector3[] DeserializeVector3Array(string aData)
    {
        string[] vectors = aData.Split('|');
        Vector3[] result = new Vector3[vectors.Length];
        for (int i = 0; i < vectors.Length; i++)
        {
            string[] values = vectors[i].Split(' ');
            if (values.Length != 3)
                throw new System.FormatException("component count mismatch. Expected 3 components but got " + values.Length);
            result[i] = new Vector3(float.Parse(values[0]), float.Parse(values[1]), float.Parse(values[2]));
        }
        return result;
    }



    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        syncronizeObjLocalpositionList.Clear();
        for (int i = 0; i < syncronizeObjs.Count; i++)
        {
            syncronizeObjLocalpositionList.Add(syncronizeObjs[i].transform.localPosition);
        }
        Vector3[] syncronizeObjLocalpositionArray = syncronizeObjLocalpositionList.ToArray();


        syncronizeObjLocalrotationList.Clear();
        for (int i = 0; i < syncronizeObjs.Count; i++)
        {
            syncronizeObjLocalrotationList.Add(syncronizeObjs[i].transform.localRotation);
        }
        Quaternion[] syncronizeObjLocalrotationArray = syncronizeObjLocalrotationList.ToArray();


        syncronizeObjLocalscaleList.Clear();
        for (int i = 0; i < syncronizeObjs.Count; i++)
        {
            syncronizeObjLocalscaleList.Add(syncronizeObjs[i].transform.localScale);
        }
        Vector3[] syncronizeObjLocalscaleArray = syncronizeObjLocalscaleList.ToArray();

        
        if (stream.IsWriting)
        {
            //send transform information
            stream.SendNext(syncronizeObjLocalpositionArray);
            stream.SendNext(syncronizeObjLocalrotationArray);
            stream.SendNext(syncronizeObjLocalscaleArray);

        }
        else if (stream.IsReading)
        {
            //receive transform information
            StreamObjLocalpositionArray = (Vector3[])stream.ReceiveNext();
            StreamObjLocalrotationArray = (Quaternion[])stream.ReceiveNext();
            StreamObjLocalscaleArray = (Vector3[])stream.ReceiveNext();
        }
    }


    private void smoothMovement()
    {
        if (!(StreamObjLocalpositionArray.Length > 0)||(StreamObjLocalpositionArray.Length!=syncronizeObjs.Count))
        {
            return;
        }
        for (int i = 0; i < syncronizeObjs.Count; i++)
        {
            syncronizeObjs[i].transform.localPosition = Vector3.Lerp(syncronizeObjs[i].transform.localPosition, StreamObjLocalpositionArray[i], Time.deltaTime * 10);
        }
    }

    private void smoothRotation()
    {
        if (!(StreamObjLocalrotationArray.Length > 0))
        {
            return;
        }
        //transform.localRotation = Quaternion.Lerp(transform.localRotation, smoothRotate, Time.deltaTime * 5);
        for (int i = 0; i < syncronizeObjs.Count; i++)
        {
            syncronizeObjs[i].transform.localRotation = Quaternion.Lerp(syncronizeObjs[i].transform.localRotation, StreamObjLocalrotationArray[i], Time.deltaTime * 10);
        }
    }

    private void smoothScale()
    {
        if (!(StreamObjLocalscaleArray.Length > 0))
        {
            return;
        }
        for (int i = 0; i < syncronizeObjs.Count; i++)
        {
            syncronizeObjs[i].transform.localScale = Vector3.Lerp(syncronizeObjs[i].transform.localScale, StreamObjLocalscaleArray[i], Time.deltaTime * 10);
        }
    }

}
