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
        node.GetComponent<MeshRenderer>().material.SetColor("_MainColor", defaultcolor);

        originalscale = node.transform.localScale*1.5f;
        node.transform.localScale = originalscale;
    }



    public void OnHover(bool onHover)
    {
        Debug.Log(transform.parent.name+ " OnHover " + onHover);
        
        namelable.enabled=onHover;
        namelable.fontSize = onHover? 40 : 28;

        labelRotationHandler.enabled= onHover ? true : false;

        //node.GetComponent<MeshRenderer>().material.shader = onHover ? Shader.Find("Mixed Reality Toolkit/Standard") : Shader.Find("Mixed Reality Toolkit/InvisibleShader");
        if (onHover)
        {
            node.GetComponent<MeshRenderer>().material.shader = Shader.Find("Mixed Reality Toolkit/Standard");
            //node.GetComponent<MeshRenderer>().material.SetFloat("_Mode", 0);
            node.GetComponent<MeshRenderer>().material.SetColor("_Color", onhovercolor);
        }
        else
        {
            //node.GetComponent<MeshRenderer>().material.SetFloat("_Mode", 2);
            node.GetComponent<MeshRenderer>().material.shader = Shader.Find("Custom/CliptestReverse");
            node.GetComponent<MeshRenderer>().material.SetColor("_MainColor", defaultcolor);

        }
        node.transform.localScale = onHover? originalscale *2.0f: originalscale;
    }

}
