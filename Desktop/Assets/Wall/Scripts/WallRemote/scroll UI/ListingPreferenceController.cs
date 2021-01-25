using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ListingPreferenceController : MonoBehaviour
{
    private TextMeshProUGUI textMeshProUGUI;
    private RawImage rawimage;
    private int texttransparency;
    private int imagetransparency;

    private Color textcolor;
    private Color imagecolor;

    public int FadeawayTime;
    public float FadeawayIntensity;

    void Start()
    {
        textMeshProUGUI = GetComponentInChildren<TextMeshProUGUI>();
        Debug.Log(textMeshProUGUI.transform.name);
        rawimage = GetComponent<RawImage>();
        FadeawayTime=0;
        textcolor = textMeshProUGUI.color;
        imagecolor = rawimage.color;
    }

    public void SetUnTransparent()
    {
        textMeshProUGUI.color = new Color(textcolor.r, textcolor.g, textcolor.b, 255);
        rawimage.color = new Color(imagecolor.r, imagecolor.g, imagecolor.b, 255);

    }

    // Update is called once per frame
    void Update()
    {
        FadeawayTime++;
        if (255 - FadeawayIntensity*FadeawayTime * Time.deltaTime < 0)
        {
            return;
        }
        Debug.Log(255 - FadeawayIntensity* FadeawayTime * Time.deltaTime);
        textMeshProUGUI.color = new Color(textcolor.r, textcolor.g, textcolor.b, 255 - FadeawayIntensity* FadeawayTime * Time.deltaTime );
        rawimage.color = new Color(imagecolor.r, imagecolor.g, imagecolor.b, 255 - FadeawayIntensity* FadeawayTime * Time.deltaTime );
    }
}
