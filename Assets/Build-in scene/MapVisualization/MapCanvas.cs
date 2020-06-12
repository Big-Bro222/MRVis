using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MapGenerator;
using Microsoft.MixedReality.Toolkit.UI;
//[ExecuteInEditMode]
public class MapCanvas : MonoBehaviour
{
    public GameObject nodeprefab;
    public float mapscale=0.0255f;
    public Vector3 maplocalpos;
    public Vector2 portion;
    public Material lineMaterial;
    

    [SerializeField]
    private GameObject FollowingLabel;


    private GameObject NullObj;
    private InteractableToggleCollection interactableToggleCollection;

    //private Vector3 maplocalscale;

    private MapInfoParser mapInfoParser;
    private List<Dictionary<string, string>> nodeinfos;
    private List<Dictionary<string, string>> edgeinfos;

    private GameObject nodeparent;
    private GameObject edgeparent;

    private float Linewidth = 0.01f;
    private Queue <Color> colorqueue;

    private void Awake()
    {
        mapInfoParser = new MapInfoParser();
        nodeinfos = mapInfoParser.GetNodeNEdgeList()[0];
        edgeinfos = mapInfoParser.GetNodeNEdgeList()[1];
        NullObj = new GameObject("yes");
        interactableToggleCollection = FindObjectOfType<InteractableToggleCollection>();

        //Setup color for all metro line
        colorqueue = new Queue<Color>();
        colorqueue.Enqueue(new Color(102,255,148));
        colorqueue.Enqueue(new Color(255,204,204));
        colorqueue.Enqueue(new Color(255,0,222));
        colorqueue.Enqueue(new Color(137,0,255));
        colorqueue.Enqueue(new Color(0,60,255));
        colorqueue.Enqueue(new Color(0,196,255));
        colorqueue.Enqueue(new Color(0,255,247));
        colorqueue.Enqueue(new Color(0,255,111));
        colorqueue.Enqueue(new Color(222,255,0));
        colorqueue.Enqueue(new Color(255,154,0));
        colorqueue.Enqueue(new Color(210,84,58));
        colorqueue.Enqueue(new Color(255,247,0));
        colorqueue.Enqueue(new Color(255,155,185));
        colorqueue.Enqueue(new Color(162,255,155));
        colorqueue.Enqueue(new Color(189,155,255));
        colorqueue.Enqueue(new Color(144,154,55));
        colorqueue.Enqueue(new Color(238,201,146));
    }
    void Start()
    {
        GenerateNodes(nodeinfos);
        GenerateEdge(edgeinfos);
        ShowMetroLine(true);
    }

    //Handler toggle behavior
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
                RevealMeatroLine("M10");
                break;
        }

    }

    //Show all the metro line
    private void ShowMetroLine(bool isShow)
    {
        for (int i = 0; i < edgeparent.transform.childCount; i++)
        {
            edgeparent.transform.GetChild(i).gameObject.SetActive(isShow);
        }
    }
    //show a particular metro line
    private void RevealMeatroLine(string metroname)
    {
        for(int i = 0; i < edgeparent.transform.childCount; i++)
        {
            bool isMetroSelected = edgeparent.transform.GetChild(i).name.Equals(metroname) ? true : false;
            edgeparent.transform.GetChild(i).gameObject.SetActive(isMetroSelected);
        }
    }
    //generate Nodes map
    private void GenerateNodes(List<Dictionary<string, string>> nodeinfos)
    {

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

           

            GameObject NodeObj=Instantiate(nodeprefab, nodeparent.transform,false);
            NodeObj.transform.localPosition = new Vector3(float.Parse(nodeXPos) * portion.x, float.Parse(nodeYPos) * portion.y, -0.01f);
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

        lineObj .GetComponent<Edge>().PosList=pos;

        LineRenderer linerenderer =lineObj.AddComponent<LineRenderer>();
        linerenderer.material = lineMaterial;
        linerenderer.material.color = colorqueue.Dequeue();
        linerenderer.useWorldSpace = false;
        linerenderer.positionCount = pos.Length;
        linerenderer.startWidth = Linewidth;
        linerenderer.endWidth = Linewidth;
        linerenderer.SetPositions(pos);

        
    }
    //generate edge information
    private void GenerateEdge(List<Dictionary<string, string>> edgeinfos)
    {
        //there was something wrong so I had to add a null dictionary here
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
                Edge edge=MetroLine.AddComponent<Edge>();
                edge.Edgelabel = edgelabel;
                edge.FollowingLabelPrefab = FollowingLabel;


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
