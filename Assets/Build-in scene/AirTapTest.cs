using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AirTapTest : MonoBehaviour
{
    public GameObject node;


    private Color onhovercolor;
    private Color defaultcolor;
    private bool onHover;
    private Vector3 originalscale;
    private TextMeshProUGUI namelable;

    // Start is called before the first frame update
    void Awake()
    {
        namelable = GetComponentInChildren<TextMeshProUGUI>();
        namelable.SetText(gameObject.transform.parent.name);
        //namelable.transform.parent.gameObject.SetActive(false);
        onHover = false;
        onhovercolor = Color.red;
        defaultcolor = Color.yellow;
        node.GetComponent<MeshRenderer>().material.color = defaultcolor;
        originalscale = node.transform.localScale*1.5f;
        node.transform.localScale = originalscale;
    }

    //private void Update()
    //{
    //    if (namelable.transform.parent.gameObject.activeSelf)
    //    {
    //        Debug.Log(namelable.transform.parent.name);
    //        namelable.transform.parent.localRotation = Camera.main.transform.rotation;
    //    }
    //}
    public void OnHover(bool onHover)
    {
        namelable.transform.parent.gameObject.SetActive(onHover);
        namelable.color = onHover ? onhovercolor : defaultcolor;
        node.GetComponent<MeshRenderer>().material.color = onHover ? onhovercolor : defaultcolor;
        node.transform.localScale = onHover? originalscale *2.0f: originalscale;
    }

}
