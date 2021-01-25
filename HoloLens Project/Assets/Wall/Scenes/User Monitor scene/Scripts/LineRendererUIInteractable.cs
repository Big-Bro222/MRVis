using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
//[ExecuteInEditMode]
public class LineRendererUIInteractable : MonoBehaviour
{
    [SerializeField]
    private BoxCollider boxCollider;
    private LineRenderer lineRenderer;
    private float linerendererWidth;
    public ToolTipHandler toolTip;
    public Color DefaultColor;
    public Color HoveredColor;
    public Color SelectedColor;

    void Start()
    {

        lineRenderer = GetComponent<LineRenderer>();
        linerendererWidth = lineRenderer.startWidth;
        lineRenderer.material.color = DefaultColor;

        EdgeCollider2D edgeCollider2D = gameObject.AddComponent<EdgeCollider2D>();
        Vector3[] newPos = new Vector3[lineRenderer.positionCount];
        lineRenderer.GetPositions(newPos);
        lineRenderer.material.SetColor("_MainColor", DefaultColor);

        List<Vector2> points = new List<Vector2>();

        foreach(Vector3 pos in newPos)
        {
            Vector2 point = new Vector2(pos.x, pos.y);
            points.Add(point);
        }

        edgeCollider2D.points = points.ToArray();
        edgeCollider2D.edgeRadius = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {


    }

    public void MouseOverEffect(bool isOver)
    {
        if (isOver)
        {
            lineRenderer.material.SetColor("_MainColor",DefaultColor);
        }
        else
        {
            lineRenderer.material.SetColor("_MainColor", SelectedColor);
        }
        lineRenderer.startWidth = isOver ? linerendererWidth * 2 : linerendererWidth;
        lineRenderer.endWidth = isOver ? linerendererWidth * 2 : linerendererWidth;

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

    private void OnMouseEnter()
    {

        if (!IsPointingAvaliable()|| !toolTip.isInteractable)
        {
            return;
        }

        if (toolTip.isLock)
        {
            return;
        }
        MouseOverEffect(true);
    }
    private void OnMouseExit()
    {
        if (toolTip.isLock|| !toolTip.isInteractable)
        {
            return;
        }
        MouseOverEffect(false);
    }

    private void OnMouseDown()
    {
        if (!toolTip.isInteractable)
        {
            return;
        }

        if (!toolTip.isLock)
        {
            toolTip.ShowTooltip(gameObject.name, gameObject.GetComponent<Edge>().MetroaccidentNum.ToString());
            toolTip.isLock = !toolTip.isLock;
            object[] datas = new object[] { true, gameObject.name, "Map" };
            PhotonNetwork.RaiseEvent(Global.INSTANTIATE_EVENT, datas, RaiseEventOptions.Default, SendOptions.SendReliable);

        }
        else
        {
            if (toolTip.Tooltiptext().Equals(gameObject.name))
            {
                toolTip.HideTooltip();
                toolTip.isLock = !toolTip.isLock;
                object[] datas = new object[] { false, gameObject.name, "Map" };
                PhotonNetwork.RaiseEvent(Global.INSTANTIATE_EVENT, datas, RaiseEventOptions.Default, SendOptions.SendReliable);
            }
            else
            {
                //object[] datas = new object[] { true, gameObject.name, "Map" };
                //PhotonNetwork.RaiseEvent(Global.INSTANTIATE_EVENT, datas, RaiseEventOptions.Default, SendOptions.SendReliable);
            }
        }
    }

    private void OnMouseUp()
    {


        
    }
}
