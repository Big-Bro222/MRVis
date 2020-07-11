using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTaskController : MonoBehaviourPun
{
    public enum TaskState
    {
        OnScreen,
        InFront,
        Fixedlabel,
        Customize
    }
    public Transform MapVis;
    public TaskState taskState;
    public GameObject FixedPannel;
    public GameObject AnnotationPannel;
    public Transform EdgeParent;
    //public GameObject highlightNode;
    void Start()
    {
        taskState = TaskState.OnScreen;
    }


    public void NextTask()
    {
        MapVis.localPosition = new Vector3(0, 0, 0);
        MapVis.localScale = new Vector3(1, 1, 1);
        MapVis.gameObject.SetActive(false);

        for(int i = 0; i < EdgeParent.childCount; i++)
        {
            EdgeParent.GetChild(i).gameObject.SetActive(false);
        }

        //if (taskState == TaskState.InFront)
        //{
        //    highlightNode.SetActive(true);
        //}
        //else 
        if (taskState == TaskState.Customize)
        {
            return;
        }
        taskState++;
        Invoke("EnableVis", 1.0f);
    }

    private void EnableVis()
    {
        MapVis.gameObject.SetActive(true);
       
        if (taskState == TaskState.Fixedlabel)
        {
            FixedPannel.SetActive(true);
            AnnotationPannel.SetActive(false);
        }
        else
        {
            FixedPannel.SetActive(false);
            AnnotationPannel.SetActive(true);
        }
    }
}
