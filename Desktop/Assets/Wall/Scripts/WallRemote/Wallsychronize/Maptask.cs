using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maptask : MonoBehaviour
{
    public enum TaskState
    {
        PracticeMode,
        OnScreen,
        InFront,
        Fixedlabel,
        Customize
    }
    public TaskState taskState;
    [SerializeField]
    private GameObject FixedAnnotation;
    public Transform EdgeParent;
    void Start()
    {
        taskState = TaskState.OnScreen;
        FixedAnnotation.SetActive(false);
    }

    public void NextTask(string TaskName)
    {
        for(int i = 0; i < EdgeParent.childCount; i++)
        {
            EdgeParent.GetChild(i).gameObject.SetActive(false);
        }

        switch (TaskName)
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
                taskState = TaskState.InFront;
                break;
            default:
                Debug.LogError("No such task found in HoloLens!!!");
                break;
        }
        if (taskState == TaskState.Fixedlabel)
        {
            FixedAnnotation.SetActive(true);
        }
        else
        {
            FixedAnnotation.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
