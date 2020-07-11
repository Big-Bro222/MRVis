using System;
using UnityEngine;
using System.Xml;
using System.IO;
using System.Collections.Generic;

#if WINDOWS_UWP
using Windows.Storage;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using System;
#endif

namespace MapGenerator

{
    public class MapInfoParser
    {

        private string xmlFilePath = "Assets/Build-in scene/MapVisualization/MetroPlanar.xml";
        private string xmlString;
        private List<string[]> LocationList;
        private int NodeNum = 20;
        private List<Dictionary<string, string>> nodeinfos;
        private List<Dictionary<string, string>> edgeinfos;

        public MapInfoParser()
        {
            nodeinfos = new List<Dictionary<string, string>>();
            edgeinfos = new List<Dictionary<string, string>>();
#if UNITY_EDITOR

                        XmlDocument doc = new XmlDocument();
                        doc.Load(xmlFilePath);
                        xmlString=doc.OuterXml;
                        XmlNodeList nodeList = doc.GetElementsByTagName("node");
                        foreach (XmlNode node in nodeList)
                        {
                            Dictionary<string, string> nodeinfo = new Dictionary<string, string>();
                            string x = node.Attributes?["x"]?.Value;
                            string y = node.Attributes?["y"]?.Value;
                            string name = node.Attributes?["name"]?.Value;
                            string id = node.Attributes?["id"]?.Value;
                            nodeinfo.Add("name", name);
                            nodeinfo.Add("id", id);
                            nodeinfo.Add("x", x);
                            nodeinfo.Add("y", y);
                            nodeinfos.Add(nodeinfo);
                        }


                        XmlNodeList edgeList = doc.GetElementsByTagName("edge");
                        foreach (XmlNode edge in edgeList)
                        {
                            Dictionary<string, string> edgeinfo = new Dictionary<string, string>();
                            string source = edge.Attributes?["source"]?.Value;
                            string target = edge.Attributes?["target"]?.Value;
                            string label = edge.Attributes?["label"]?.Value;
                            string id = edge.Attributes?["id"]?.Value;
                            edgeinfo.Add("label", label);
                            edgeinfo.Add("id", id);
                            edgeinfo.Add("target", target);
                            edgeinfo.Add("source", source);
                            edgeinfos.Add(edgeinfo);
                        }
                        NodeNum = nodeinfos.Count;




#elif WINDOWS_UWP
                    Debug.Log("windows_UWP is running");
                    Task task = new Task(
                        async () =>
                        {
                            Debug.Log("this is running async");
                            var xmlFile1 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/MetroPlanar.xml"));
                            Windows.Data.Xml.Dom.XmlDocument xdoc = await Windows.Data.Xml.Dom.XmlDocument.LoadFromFileAsync(xmlFile1);
                            xmlString= xdoc.GetXml();
                            Debug.Log(xmlString);
                            Windows.Data.Xml.Dom.XmlNodeList nodeList= null;
                            
                            nodeList=xdoc.GetElementsByTagName("node");
                            
                            foreach(IXmlNode node in nodeList){
                                
                                Dictionary<string,string> nodeinfo =new Dictionary<string, string>();
                                string name = node.Attributes.GetNamedItem("name").InnerText;
                                string x = node.Attributes.GetNamedItem("x").InnerText;
                                string y = node.Attributes.GetNamedItem("y").InnerText;
                                string id = node.Attributes.GetNamedItem("id").InnerText;
                                nodeinfo.Add("name",name);
                                nodeinfo.Add("x",x);
                                nodeinfo.Add("y",y);
                                nodeinfo.Add("id",id);
                                nodeinfos.Add(nodeinfo);    
                            }
                            





                            Windows.Data.Xml.Dom.XmlNodeList edgeList= null;
                            
                            edgeList=xdoc.GetElementsByTagName("edge");
                            
                            foreach(IXmlNode edge in edgeList){
                                
                                Dictionary<string,string> edgeinfo =new Dictionary<string, string>();
                                string label = edge.Attributes.GetNamedItem("label").InnerText;
                                string id = edge.Attributes.GetNamedItem("id").InnerText;
                                string target = edge.Attributes.GetNamedItem("target").InnerText;
                                string source = edge.Attributes.GetNamedItem("source").InnerText;
                                edgeinfo.Add("label",label);
                                edgeinfo.Add("id",id);
                                edgeinfo.Add("target",target);
                                edgeinfo.Add("source",source);
                                edgeinfos.Add(edgeinfo);
                             }
                        }
            );
                    task.Start();
                    task.Wait();
#endif

        }


        public List<Dictionary<string, string>>[] GetNodeNEdgeList()
        {
            List<Dictionary<string, string>> []infos = new List<Dictionary<string, string>> [2];
            infos[0] = nodeinfos;
            infos[1] = edgeinfos;
            Debug.Log("InParser: "+ edgeinfos.Count);
            return infos;
        }

    }
}