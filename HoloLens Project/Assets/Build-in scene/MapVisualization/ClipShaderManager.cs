using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClipShaderManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] BoxCollider boxCollider;
    private LineRenderer[] lineRenderers;
    private MeshRenderer[] meshRenderers;
    Vector3 m_Center;
    Vector3 m_Size, m_Min, m_Max;
    private enum Renderer
    {
        lineRenderers,
        meshrenderers
    }

    [SerializeField]
    private Renderer renderer;

    void Start()
    {
        if (renderer == Renderer.lineRenderers)
        {
            lineRenderers = GetComponentsInChildren<LineRenderer>();
            ReRenderLine();
        }else if (renderer == Renderer.meshrenderers)
        {
            meshRenderers = GetComponentsInChildren<MeshRenderer>();
            ReRenderLine();
        }

    }

    void ReRenderLine()
    {
        m_Min = boxCollider.bounds.min;
        m_Max = boxCollider.bounds.max;
        float Top = m_Max.y;
        float Bottom = m_Min.y;
        float Right = m_Max.x;
        float Left = m_Min.x;

        if(renderer == Renderer.lineRenderers)
        {
            foreach (LineRenderer lineRenderer in lineRenderers)
            {
                lineRenderer.material.SetFloat("_Top", Top);
                lineRenderer.material.SetFloat("_Bottom", Bottom);
                lineRenderer.material.SetFloat("_Right", Right);
                lineRenderer.material.SetFloat("_Left", Left);
            }
        }else if(renderer == Renderer.meshrenderers)
        {
            foreach (MeshRenderer meshRenderer in meshRenderers)
            {
                meshRenderer.material.SetFloat("_Top", Top);
                meshRenderer.material.SetFloat("_Bottom", Bottom);
                meshRenderer.material.SetFloat("_Right", Right);
                meshRenderer.material.SetFloat("_Left", Left);
            }
        }

        //Debug.Log("Collider bound Minimum : " + m_Min);
        //Debug.Log("Collider bound Maximum : " + m_Max);
    }

    // Update is called once per frame
    void Update()
    {
        if (boxCollider.bounds.min!= m_Min|| boxCollider.bounds.max!= m_Max)
        {
            ReRenderLine();
        }
    }
}
