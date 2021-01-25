using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTaskController : MonoBehaviourPun
{
    public enum TaskState
    {
        PracticeMode,
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
    private FocusObj focusObj;

    //public GameObject highlightNode;
    void Start()
    {
        taskState = TaskState.PracticeMode;
        focusObj = GetComponent<FocusObj>();
    }


    public void NextTask(string taskName)
    {
        //focusObj.Focus.GetComponent<NodeInteractionController>().OnHover(false);
        MapVis.localPosition = new Vector3(0, 0, 0);
        MapVis.localScale = new Vector3(1, 1, 1);
        MapVis.gameObject.SetActive(false);
        
        //disable edges
        for(int i = 0; i < EdgeParent.childCount; i++)
        {
            EdgeParent.GetChild(i).gameObject.SetActive(false);
        }
        if (taskState == TaskState.Customize)
        {
            return;
        }

        switch (taskName)
        {
            case "PracticeMode":
                taskState = TaskState.PracticeMode;
                    break;
            case "OnScreen":
                taskState = TaskState.OnScreen;
                break;
            case "InFront":
                taskState = TaskState.InFront;
                break;
            case "Fixedlabel":
                taskState = TaskState.Fixedlabel;
                break;
            case "Customize":
                taskState = TaskState.Customize;
                break;
            case "Over":
                Debug.Log("Task Over");
                break;
            default:
                Debug.Log("No such task found in HoloLens!!!");
                break;
        }

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
