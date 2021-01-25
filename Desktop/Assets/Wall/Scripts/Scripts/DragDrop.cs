using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class DragDrop : MonoBehaviour,IPointerDownHandler,IBeginDragHandler,IEndDragHandler,IDragHandler
{
    private RectTransform rectTransform;
    [SerializeField] Canvas canvas;
    private CanvasGroup canvasGroup;
    public BoxCollider boxCollider;
    public ToolTipHandler toolTipHandler;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private bool IsPointingAvaliable()
    {
        bool isavaliable = false;

        Vector3 m_Min = boxCollider.bounds.min;
        Vector3 m_Max = boxCollider.bounds.max;
        float Top = m_Max.y;
        float Bottom = m_Min.y;
        float Right = m_Max.x;
        float Left = m_Min.x;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Left < mousePos.x && mousePos.x < Right && Bottom < mousePos.y && mousePos.y < Top)
        {
            isavaliable = true;
        }

        return isavaliable;
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!IsPointingAvaliable())
        {
            return;
        }
        toolTipHandler.isInteractable = false;
        canvasGroup.alpha = 0.5f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!IsPointingAvaliable())
        {
            return;
        }
        rectTransform.anchoredPosition += eventData.delta / (canvas.scaleFactor * 7.8125f);
        Vector2 movementToSent = eventData.delta / (canvas.scaleFactor * 7.8125f * 105);

        object[] datas = new object[] { movementToSent.x, movementToSent.y };
        PhotonNetwork.RaiseEvent(Global.SCALE, datas, RaiseEventOptions.Default, SendOptions.SendUnreliable);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.0f;
        toolTipHandler.isInteractable = true;

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");

    }
}
