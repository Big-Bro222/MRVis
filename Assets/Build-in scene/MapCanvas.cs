using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MapGenerator;
using Microsoft.MixedReality.Toolkit.UI;

public class MapCanvas : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject nodeprefab;
    public float mapscale=0.0255f;
    public Vector3 maplocalpos;
    public Vector2 portion;
    public Material lineMaterial;
    //public GameObject testobj;

    private GameObject NullObj;
    private InteractableToggleCollection interactableToggleCollection;

    //private Vector3 maplocalscale;

    private MapInfoParser mapInfoParser;
    private List<Dictionary<string, string>> nodeinfos;
    private List<Dictionary<string, string>> edgeinfos;

    private GameObject nodeparent;
    private GameObject edgeparent;

    private float Linewidth = 0.01f;

    private void Awake()
    {
        mapInfoParser = new MapInfoParser();
        nodeinfos = mapInfoParser.GetNodeNEdgeList()[0];
        edgeinfos = mapInfoParser.GetNodeNEdgeList()[1];
        NullObj = new GameObject("yes");
        interactableToggleCollection = FindObjectOfType<InteractableToggleCollection>();
        
    }
    void Start()
    {
        GenerateNodes(nodeinfos);
        GenerateEdge(edgeinfos);
        ShowMetroLine(false);
    }


    public void ToggleHandler()
    {
        int Index=interactableToggleCollection.CurrentIndex;
        switch (Index)
        {
            case 0:
                ShowMetroLine(true);
                break;
            case 1:
                RevealMeatroLine("M10");
                break;
            case 2:
                ShowMetroLine(false);
                break;
            default:
                ShowMetroLine(false);
                break;
        }

    }
    private void ShowMetroLine(bool isShow)
    {
        for (int i = 0; i < edgeparent.transform.childCount; i++)
        {
            edgeparent.transform.GetChild(i).gameObject.SetActive(isShow);
        }
    }

    private void RevealMeatroLine(string metroname)
    {
        for(int i = 0; i < edgeparent.transform.childCount; i++)
        {
            bool isMetroSelected = edgeparent.transform.GetChild(i).name.Equals(metroname) ? true : false;
            edgeparent.transform.GetChild(i).gameObject.SetActive(isMetroSelected);
        }
    }

    private void GenerateNodes(List<Dictionary<string, string>> nodeinfos)
    {
        Debug.Log("Nodegenerator is running");
        
        nodeparent=Instantiate(new GameObject(),transform);
        nodeparent.name = "nodeparent";
        

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

           

            GameObject NodeObj=Instantiate(nodeprefab, new Vector3(float.Parse(nodeXPos)* portion.x, float.Parse(nodeYPos)* portion.y, -0.01f), Quaternion.Euler(0,0,0), nodeparent.transform);
            NodeObj.name = nodeName;
            NodeObj.transform.localPosition += maplocalpos;
            NodeObj.transform.localScale = new Vector3(mapscale, mapscale, mapscale);

            Node node = NodeObj.AddComponent<Node>();
            node.name = nodeName;
            node.id = nodeId;
        }
    }

    private Vector3[] GetNodesPos(List<string>MetroPointArrar) {
        List<Vector3> nodesPoslist = new List<Vector3>();
        foreach(string metropoint in MetroPointArrar)
        {
            Dictionary<string, string> nodeinfomatch = new Dictionary<string, string>();

            nodeinfomatch = nodeinfos.Find(nodeinfo => nodeinfo["id"] == metropoint);
            nodesPoslist.Add(new Vector3(float.Parse(nodeinfomatch["x"])*portion.x+maplocalpos.x, float.Parse(nodeinfomatch["y"]) * portion.y+maplocalpos.y, 0));
        }
        Vector3[] NodesPos = new Vector3[nodesPoslist.Count];
        for(int i = 0; i < NodesPos.Length; i++)
        {
            NodesPos[i] = nodesPoslist[i];
        }

        return NodesPos;
    }

    private void GenerateLineRenderer(Vector3[]pos,GameObject lineObj)
    {
        Debug.Log("generateLine running");
        LineRenderer linerenderer =lineObj.AddComponent<LineRenderer>();
        linerenderer.material = lineMaterial;
        linerenderer.useWorldSpace = false;
        linerenderer.positionCount = pos.Length;
        linerenderer.startWidth = Linewidth;
        linerenderer.endWidth = Linewidth;
        linerenderer.SetPositions(pos);
    }

    private void GenerateEdge(List<Dictionary<string, string>> edgeinfos)
    {
        List<Dictionary<string, string>> M9edgeinfos = edgeinfos;
        Dictionary<string, string> M9generator = new Dictionary<string, string>();
        M9generator.Add("label", "M99");
        M9generator.Add("id", "M99");
        M9generator.Add("target", "M99");
        M9generator.Add("source", "M99");
        M9edgeinfos.Add(M9generator);


        edgeparent = Instantiate(new GameObject(), transform);
        edgeparent.name = "edgeparent";
        List<string> metroName = new List<string>();


        List<string> MetroPointArrar = new List<string>();
        GameObject MetroToBeAdd = new GameObject();
        
        foreach ( Dictionary<string, string> edgeinfo in M9edgeinfos)
        {
            
            string edgelabel=edgeinfo["label"];
            string edgeId = edgeinfo["id"];
            string edgeSource = edgeinfo["source"];
            string edgeTarget=edgeinfo["target"];
            

            if (!metroName.Contains(edgelabel))
            {


                if (metroName.Count != 0)
                {
                    Vector3[] NodesPos = GetNodesPos(MetroPointArrar);
                    GenerateLineRenderer(NodesPos, MetroToBeAdd);
                }

                if (edgelabel.Equals("M99"))
                {
                    break;
                }
                metroName.Add(edgelabel);
                GameObject MetroLine = Instantiate(NullObj, edgeparent.transform);
                MetroLine.name = edgelabel;
                MetroToBeAdd = MetroLine;

                MetroPointArrar.Clear();
                MetroPointArrar.Add(edgeTarget);
                MetroPointArrar.Add(edgeSource);

            }
            else
            {
                MetroPointArrar.Add(edgeSource);
            }



        }

    }


}
