using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecalibrationController : MonoBehaviour
{
    public List<GameObject> SetActiveGameObjects;
    void Start()
    {
        
    }

    public void FinishCalibartion()
    {
        foreach(GameObject setactive in SetActiveGameObjects)
        {
            setactive.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
