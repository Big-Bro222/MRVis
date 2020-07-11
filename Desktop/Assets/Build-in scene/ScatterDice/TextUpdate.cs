using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//using Microsoft.MixedReality.Toolkit.UI;

public class TextUpdate : MonoBehaviour
{
    public float highestNum;
    public float lowestNum;
    //public SliceBehaviorHandler sliceBehaviorHandler;

    public string axis;

    private string name;
    private float highest;
    private float lowest;
    private TextMeshPro textMeshPro;

    void Start()
    {
        highest = highestNum;
        lowest = lowestNum;
        name = gameObject.name;
        textMeshPro = GetComponent<TextMeshPro>();
        textMeshPro.SetText(name+" :"+lowest+"-"+highest);
        
    }


    //public void OnlowestSliderUpdated(SliderEventData eventData)
    //{
    //    if (lockdown)
    //    {
    //        textMeshPro.SetText(textMeshPro.text);
    //        return;
    //    }
    //    float sliderValue = eventData.NewValue;
    //    string previoustext = textMeshPro.text;
    //    string Texthold = previoustext.Split('-')[1];
    //    lowest = sliderValue * (lowestNum + highestNum) / 2;
    //    //textMeshPro.SetText($"{ name} : {lowest.ToString("F2")}-{Texthold}");
    //    textMeshPro.SetText(name + " :" + lowest.ToString("F2") + "-" + Texthold);

    //}


    //public void OnHighestSliderUpdated(SliderEventData eventData)
    //{
    //    if (lockdown)
    //    {
    //        textMeshPro.SetText(textMeshPro.text);
    //        return;
    //    }
    //    float sliderValue = eventData.NewValue;
    //    string previoustext = textMeshPro.text;
    //    string Texthold = previoustext.Split('-')[0];
    //    highest  = sliderValue * (-lowestNum + highestNum) / 2 + (lowestNum + highestNum)/2;
    //    textMeshPro.SetText($"{Texthold}-{highest.ToString("F2")} ");
    //    textMeshPro.SetText(Texthold +  "-" + highest.ToString("F2"));

    //}

    // Update is called once per frame
    //void Update()
    //{

    //    if (axis.Equals("x"))
    //    {
    //        lowest = (sliceBehaviorHandler.xMin+0.5f)*(highestNum-lowestNum);
    //        highest = sliceBehaviorHandler.xMax * (-lowestNum + highestNum)  + (lowestNum + highestNum)/2;

    //    }
    //    else if (axis.Equals("y"))
    //    {
    //        lowest = (sliceBehaviorHandler.yMin + 0.5f) * (highestNum - lowestNum);
    //        highest = sliceBehaviorHandler.yMax * (-lowestNum + highestNum) + (lowestNum + highestNum)/2;
    //    }
    //    else if (axis.Equals("z"))
    //    {
    //        lowest = (sliceBehaviorHandler.zMin + 0.5f) * (highestNum - lowestNum);
    //        highest = sliceBehaviorHandler.zMax * (-lowestNum + highestNum) + (lowestNum + highestNum)/2;
    //    }
    //    textMeshPro.SetText(name + " :" + lowest.ToString("F2") + "-" + highest.ToString("F2"));
    //}
}
