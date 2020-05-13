using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.Text;

public class PhotonSynChroManager : MonoBehaviourPun, IPunObservable
{

    public List<GameObject> syncronizeObjs;
    public PhotonView pv;

    private List<Vector3> syncronizeObjLocalpositionList;
    private List<Quaternion> syncronizeObjLocalrotationList;
    private List<Vector3> syncronizeObjLocalscaleList;

    private Vector3[] StreamObjLocalpositionArray;
    private Quaternion[] StreamObjLocalrotationArray;
    private Vector3[] StreamObjLocalscaleArray;






    void Awake()
    {
        syncronizeObjLocalpositionList = new List<Vector3>();
        for (int i = 0; i < syncronizeObjs.Count; i++)
        {
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


    void Update()
    {
        Debug.Log(syncronizeObjs.Count);
        if (photonView.IsMine)
        {
            //ProcessInput();
        }
        else
        {
            smoothMovement();
            //smoothRotation();
            //smoothScale();
        }
    }


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
            Debug.Log(i);
            syncronizeObjLocalpositionList.Add(syncronizeObjs[i].transform.localPosition);
        }
        Vector3[] syncronizeObjLocalpositionArray = syncronizeObjLocalpositionList.ToArray();



        //for (int i = 0; i < syncronizeObjs.Count; i++)
        //{
        //    syncronizeObjLocalrotationList[i] = syncronizeObjs[i].transform.localRotation;
        //}
        //Quaternion[] syncronizeObjLocalrotationArray = syncronizeObjLocalrotationList.ToArray();


        //for (int i = 0; i < syncronizeObjs.Count; i++)
        //{
        //    syncronizeObjLocalscaleList[i] = syncronizeObjs[i].transform.localScale;
        //}
        //Vector3[] syncronizeObjLocalscaleArray = syncronizeObjLocalscaleList.ToArray();


        if (stream.IsWriting)
        {
            //stream.SendNext(SerializeVector3Array(syncronizeObjLocalpositionArray));

            stream.SendNext(syncronizeObjLocalpositionArray);
            Debug.Log("sending objNum" + syncronizeObjLocalpositionArray.Length);
            //stream.SendNext(syncronizeObjLocalrotationArray);
            //stream.SendNext(syncronizeObjLocalscaleArray);

        }
        else if (stream.IsReading)
        {
            //string StreamObjLocalpositionArraystr = (string)stream.ReceiveNext();
            //StreamObjLocalpositionArray = DeserializeVector3Array(StreamObjLocalpositionArraystr);


            StreamObjLocalpositionArray = (Vector3[])stream.ReceiveNext();
            Debug.Log("reading objNum" + StreamObjLocalpositionArray.Length);
            //StreamObjLocalrotationArray = (Quaternion[])stream.ReceiveNext();
            //StreamObjLocalscaleArray = (Vector3[])stream.ReceiveNext();
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
