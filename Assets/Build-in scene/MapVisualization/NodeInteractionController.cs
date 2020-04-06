using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NodeInteractionController : MonoBehaviour
{
    public GameObject node;


    private Color onhovercolor;
    private Color defaultcolor;
    private bool onHover;
    private Vector3 originalscale;
    private TextMeshPro namelable;
    private LabelMovementManager labelRotationHandler;

    // Start is called before the first frame update
    void Start()
    {
        node = transform.parent.GetChild(0).gameObject;

        namelable = GetComponent<TextMeshPro>();

        labelRotationHandler = GetComponent<LabelMovementManager>();
        labelRotationHandler.enabled = false;

        onHover = false;
        onhovercolor = Color.red;
        defaultcolor = Color.yellow;
        node.GetComponent<MeshRenderer>().material.color = defaultcolor;
        originalscale = node.transform.localScale*1.5f;
        node.transform.localScale = originalscale;
    }



    public void OnHover(bool onHover)
    {
        Debug.Log(transform.parent.name+ " OnHover " + onHover);
        
        namelable.enabled=onHover;
        namelable.fontSize = onHover? 40 : 28;

        labelRotationHandler.enabled= onHover ? true : false;

        node.GetComponent<MeshRenderer>().material.color = onHover ? onhovercolor : defaultcolor;
        node.transform.localScale = onHover? originalscale *2.0f: originalscale;
    }

}
