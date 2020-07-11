using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaselLinerenderUpdate : MonoBehaviour
{
    // Start is called before the first frame update

    LineRenderer lineRenderer;
    [SerializeField]
    public Transform pivot;

    [SerializeField]
    public Transform target;

    public Material lineMaterial;

    void Start()
    {
        if (GetComponent<LineRenderer>())
        {
            lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.material = lineMaterial;

        }
        else
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
            lineRenderer.material = lineMaterial;
        }

        lineRenderer.startWidth = 0.003f;
        lineRenderer.endWidth = 0.01f;

        DrawBezier();
    }

    // Update is called once per frame
    void Update()
    {

        DrawBezier();

    }

    private void DrawBezier()
    {
        List<Vector3> pointList = new List<Vector3>();
        Vector3 point3Pos = new Vector3((pivot.transform.position.x + target.transform.position.x) * 0.5f, (pivot.transform.position.y + target.transform.position.y) * 0.65f, (pivot.transform.position.z + target.transform.position.z) * 1.5f);

        int vertexCount = 30;

        for (float ratio = 0; ratio <= 1; ratio += 1.0f / vertexCount)
        {
            Vector3 tangentLineVertex1 = Vector3.Lerp(pivot.position, point3Pos, ratio);
            Vector3 tangentLineVectex2 = Vector3.Lerp(point3Pos, target.position, ratio);
            Vector3 bezierPoint = Vector3.Lerp(tangentLineVertex1, tangentLineVectex2, ratio);
            pointList.Add(bezierPoint);
        }
        lineRenderer.positionCount = pointList.Count;
        lineRenderer.SetPositions(pointList.ToArray());
    }
}
