using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolTipHandler : MonoBehaviour
{
    // Start is called before the first frame update

    private Text tooltiptext;
    private RectTransform backgroundRectTransform;
    [SerializeField]
    private Camera uiCamera;
    private List<string>Metrostation;


    public bool isLock;
    void Start()
    {
        Metrostation = new List<string>();
        isLock = false;
        backgroundRectTransform = transform.GetComponent<RectTransform>();
        tooltiptext = transform.Find("Text").GetComponent<Text>();
        ShowTooltip("yessssss");
    }

    // Update is called once per frame
    void Update()
    {
        if (isLock)
        {
            return;
        }
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), Input.mousePosition, uiCamera, out localPoint);
        transform.localPosition = new Vector3(localPoint.x+7f,localPoint.y+3f,-10f);
    }

    public void ShowTooltip(string tooltip)
    {

        if(Metrostation.Count >= 7)
        {
            if (tooltip.Equals(Metrostation[6]))
            {
                gameObject.SetActive(true);
                tooltiptext.text = "RER B";
            }
            else
            {
                gameObject.SetActive(true);
                tooltiptext.text = tooltip;
            }
        }
        else
        {
            if (!Metrostation.Contains(tooltip))
            {
                Metrostation.Add(tooltip);
            }
            gameObject.SetActive(true);
            tooltiptext.text = tooltip;
        }

    }

    public string Tooltiptext()
    {
        return tooltiptext.text;
    }

    public void HideTooltip()
    {
        gameObject.SetActive(false);
    }
}
