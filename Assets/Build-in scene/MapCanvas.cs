using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MapGenerator;

public class MapCanvas : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject nodeprefab;
    public float mapscale=0.0255f;
    public Vector3 maplocalpos;
    public Vector2 portion;

    //private Vector3 maplocalscale;

    private MapInfoParser mapInfoParser;
    private List<Dictionary<string, string>> nodeinfos;
    private List<Dictionary<string, string>> edgeinfos;

    private void Awake()
    {
        mapInfoParser = new MapInfoParser();
        nodeinfos = mapInfoParser.GetNodeList();
        edgeinfos = mapInfoParser.GetEdgeList();
    }
    void Start()
    {
        GenerateNodes(nodeinfos);
        GenerateEdge(edgeinfos);
    }



    private void GenerateNodes(List<Dictionary<string, string>> nodeinfos)
    {

        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(0).gameObject);
        }

        foreach (Dictionary<string,string> nodeinfo in nodeinfos)
        {
            string nodeName;
            string nodeId;
            string nodeXPos;
            string nodeYPos;
            nodeinfo.TryGetValue("name", out nodeName);
            nodeinfo.TryGetValue("id", out nodeId);
            nodeinfo.TryGetValue("x", out nodeXPos);
            nodeinfo.TryGetValue("y", out nodeYPos);

            GameObject NodeObj=Instantiate(nodeprefab, new Vector3(float.Parse(nodeXPos)* portion.x, float.Parse(nodeYPos)* portion.y, -0.01f), Quaternion.Euler(0,0,0), transform);
            NodeObj.name = nodeName;
            NodeObj.transform.localPosition += maplocalpos;
            NodeObj.transform.localScale = new Vector3(mapscale, mapscale, mapscale);

            Node node = NodeObj.AddComponent<Node>();
            node.name = nodeName;
            node.id = nodeId;
        }
    }

    private void GenerateEdge(List<Dictionary<string, string>> edgeinfos)
    {


    }


}
