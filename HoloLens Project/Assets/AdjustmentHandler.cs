using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustmentHandler : MonoBehaviour
{
    [SerializeField] InvisiableInteractableSlider AdjustX;
    [SerializeField] InvisiableInteractableSlider AdjustY;
    [SerializeField] InvisiableInteractableSlider AdjustZ;

    private Vector3 OriginalPos;
    void OnEnable()
    {
        OriginalPos = transform.position;
    }


    public void UpdatePos()
    {
        transform.Find("Wall").localPosition = new Vector3(AdjustX.SliderValue*0.1f, AdjustY.SliderValue*0.1f, AdjustZ.SliderValue * 0.1f);
        transform.Find("RecalibtationMarker").localPosition = new Vector3(AdjustX.SliderValue * 0.1f, AdjustY.SliderValue * 0.1f, AdjustZ.SliderValue * 0.1f);

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Debug.Log(transform.GetChild(i).localPosition+" , "+transform.GetChild(i).name);
        }
    }
}
