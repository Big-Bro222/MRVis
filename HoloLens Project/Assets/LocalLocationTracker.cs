using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


[ExecuteAlways]
public class LocalLocationTracker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TextMeshPro[] textmeshpros = GetComponentsInChildren<TextMeshPro>();
        Debug.Log("yes"+textmeshpros.Length);
        foreach(TextMeshPro textMeshPro in textmeshpros)
        {
            DestroyImmediate(textMeshPro.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("P_Local position: "+ transform.parent.localPosition);
        Debug.Log("C_Local position: " + transform.localPosition);

    }
}
