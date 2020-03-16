using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AirTapTest : MonoBehaviour
{
    private Color onhovercolor;
    private Color defaultcolor;
    private bool onHover;
    private Vector3 originalscale;
    private TextMeshProUGUI namelable;

    // Start is called before the first frame update
    void Start()
    {
        namelable = GetComponentInChildren<TextMeshProUGUI>();
        namelable.SetText(gameObject.name);
        namelable.gameObject.SetActive(false);
        onHover = false;
        onhovercolor = Color.red;
        defaultcolor = Color.yellow;
        GetComponent<MeshRenderer>().material.color = defaultcolor;
        originalscale = transform.localScale*1.5f;
        transform.localScale = originalscale;
    }


    public void OnHover(bool onHover)
    {
        namelable.gameObject.SetActive(onHover);
        GetComponent<MeshRenderer>().material.color = onHover ? onhovercolor : defaultcolor;     
        transform.localScale = onHover? originalscale *2.0f: originalscale;
    }

}
