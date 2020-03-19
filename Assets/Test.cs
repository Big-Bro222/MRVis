using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Microsoft.MixedReality.Toolkit;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update

    private TextMeshProUGUI lable;
    private GameObject canvas;
    void Start()
    {
        lable = GetComponentInChildren<TextMeshProUGUI>();
        canvas = lable.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Camera.main.transform.rotation;
    }
}
