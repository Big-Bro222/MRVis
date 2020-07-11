using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TaskUpdate : MonoBehaviour
{

    private TextMeshProUGUI text;

    void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void Nexttask(string TaskName)
    {
        text.text = "Task " + TaskName;
    }

    public void Resettask()
    {
        text.text = "Task Practice";
    }
}
