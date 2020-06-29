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

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.5f;


    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / (canvas.scaleFactor * 7.8125f);
        Vector2 movementToSent = eventData.delta / (canvas.scaleFactor * 7.8125f * 105*2);

        object[] datas = new object[] { movementToSent.x, movementToSent.y };
        PhotonNetwork.RaiseEvent(Global.SCALE, datas, RaiseEventOptions.Default, SendOptions.SendUnreliable);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.0f;

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
    }
}
