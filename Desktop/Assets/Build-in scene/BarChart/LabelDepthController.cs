using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabelDepthController : MonoBehaviour
{
    int depthScale;
    RectTransform[] rectTransforms;
    public float depth;
    void Start()
    {
        depthScale = 0;
        rectTransforms = GetComponentsInChildren<RectTransform>();
    }


    public void LabelDepthUpdate()
    {
        if (depthScale > 5)
        {
            depthScale=0;
        }

        foreach (RectTransform rectTransform in rectTransforms)
        {
            rectTransform.position += new Vector3(0, 0, -depth);
        }
        depthScale++;

    }

}
