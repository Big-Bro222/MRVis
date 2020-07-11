using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ToolTipHandler : MonoBehaviour
{
    // Start is called before the first frame update

    private Text tooltiptext;
    private Text accidentNumtext;
    private RectTransform backgroundRectTransform;
    //private CustomeStandaloneInputModule inputModule;
    [SerializeField]
    private Camera uiCamera;

    public bool isLock;
    public bool isInteractable;


    void Start()
    {
        isLock = false;
        isInteractable = true;
        backgroundRectTransform = transform.GetComponent<RectTransform>();
        tooltiptext = transform.Find("Text").GetComponent<Text>();
        accidentNumtext= transform.Find("Text (1)").GetComponent<Text>();
        ShowTooltip("Click on MetroLine","");

        //inputModule = FindObjectOfType<CustomeStandaloneInputModule>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (inputModule.IsPointerOverGameObject<PhysicsRaycaster>())
        //{
        //    print("Over 3D Object!");
        //}
        //else if (inputModule.IsPointerOverGameObject<Physics2DRaycaster>())
        //{
        //    print("Over 2D Object!");
        //}
        //else if (inputModule.IsPointerOverGameObject<GraphicRaycaster>())
        //{
        //    print("Over UI Object!");
        //}

        if (isLock)
        {
            return;
        }
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), Input.mousePosition, uiCamera, out localPoint);
        transform.localPosition = new Vector3(localPoint.x+7f,localPoint.y+3f,-10f);
    }

    public void ShowTooltip(string tooltip,string metroAccidentNum)
    {
        gameObject.SetActive(true);
        tooltiptext.text = tooltip;
        accidentNumtext.text = metroAccidentNum;

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
