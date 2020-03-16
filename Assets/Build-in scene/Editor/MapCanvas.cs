using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MapGenerator;

[ExecuteInEditMode]
public class MapCanvas : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject nodeprefab;
    public Vector3 maplocalscale;
    public Vector2 maplocalpos;

    private MapInformationParser mapInformationParser;  
    private List<string[]> locationlist;
    void Start()
    {
        mapInformationParser = new MapInformationParser();
        locationlist=mapInformationParser.GetLocationList();
        GenerateNodes();
    }


    private void GenerateNodes()
    {
        foreach(string[] location in locationlist)
        {
            GameObject Node=Instantiate(nodeprefab, new Vector3(float.Parse(location[0]), float.Parse(location[1]), -0.01f), Quaternion.Euler(-90,0,0), transform);
            Node.name = location[2];
        }
        
        gameObject.transform.localScale = maplocalscale;
        gameObject.transform.localPosition = new Vector3(maplocalpos.x, maplocalpos.y, 0);
        
    }
    
}
