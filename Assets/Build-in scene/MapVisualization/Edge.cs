using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;
using TMPro;

public class Edge : MonoBehaviour
{

    public Vector3[] PosList;
    public string Edgelabel;
    public GameObject FollowingLabelPrefab;
    private RaycastManager raycastManager;

    private GameObject FollowingLabel;
    void Start()
    {
        Vector3 InitialLablePoint = GetComponent<LineRenderer>().GetPosition(0);
        FollowingLabel = Instantiate(FollowingLabelPrefab, new Vector3(InitialLablePoint.x, InitialLablePoint.y, 0), Quaternion.identity,transform);
        FollowingLabel.name = Edgelabel + " sphere";
        FollowingLabel.GetComponentInChildren<TextMeshPro>().SetText(Edgelabel);

        raycastManager = FindObjectOfType<RaycastManager>();
 
    }



    // Update is called once per frame
    void Update()
    {

        Vector2 previousPoint = new Vector2(0, 0);
        Vector3 hitPosLocal = transform.InverseTransformPoint(raycastManager.hitpos);


        //FollowingLabel.transform.localPosition = hitPosLocal;

        for (int i = 0; i < PosList.Length; i++)
        {
            if (PosList[i].x < hitPosLocal.x)
            {
                previousPoint = new Vector2(PosList[i].x, PosList[i].y);
            }
            else if (PosList[i].x >= hitPosLocal.x)
            {
                if (i == 0)
                {
                    FollowingLabel.transform.localPosition = new Vector3(PosList[i].x , PosList[i].y, FollowingLabel.transform.localPosition.z);
                    break;
                }
                float pointY = GetY(previousPoint, new Vector2(PosList[i].x, PosList[i].y-0.7f), hitPosLocal.x);
                Vector3 moveTowardsPoint = new Vector3(hitPosLocal.x, pointY, FollowingLabel.transform.localPosition.z);

                //indicator.transform.localPosition = Vector3.Lerp(indicator.transform.localPosition, moveTowardsPoint, 0.05f);
                FollowingLabel.transform.localPosition = moveTowardsPoint;

            }
        }


    }


    private float GetY(Vector2 startPoint,Vector2 endPoint, float x)
    {

        float y = 0;
        y=(endPoint.y - startPoint.y) / (endPoint.x - startPoint.x) * (x - startPoint.x) + startPoint.y;
        Debug.Log(y + " is Y");
        Debug.Log("start at" + startPoint.ToString());
        Debug.Log("end at" + endPoint.ToString());
        return y;

    }
}
