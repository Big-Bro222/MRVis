using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererUpdate : MonoBehaviour
{
    // Start is called before the first frame update

    LineRenderer lineRenderer;
    [SerializeField]
    private Transform pivot;

    [SerializeField]
    private Transform target;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        if (pivot.position != lineRenderer.GetPosition(0))
        {
            lineRenderer.SetPosition(0,pivot.position);
        }

        if (target.position != lineRenderer.GetPosition(1))
        {
            lineRenderer.SetPosition(1, target.position);
        }


    }
}
