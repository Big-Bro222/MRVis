using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TaskUpdate : MonoBehaviour
{

    private int taskindex;
    private TextMeshProUGUI text;

    void Start()
    {
        taskindex = 1;
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void Nexttask()
    {
        taskindex++;
        text.text = "Task " + taskindex;
    }

    public void Resettask()
    {
        taskindex = 1;
        text.text = "Task 1";
    }
}
