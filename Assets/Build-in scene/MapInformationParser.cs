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
    public class MapInformationParser
    {
        private XmlDocument doc;
        private string xmlFilePath = "Assets/Build-in scene/MetroPlanar.xml";
        private List<string[]> LocationList;

        public MapInformationParser()
        {
            doc = new XmlDocument();
            doc.Load(xmlFilePath);
            LocationList = new List<string[]>();
            LocationInitialize();
        }


        private void LocationInitialize()
        {
            XmlNodeList nodeList = doc.GetElementsByTagName("node");
            foreach (XmlNode node in nodeList)
            {
                string x = node.Attributes?["x"]?.Value;
                string y = node.Attributes?["y"]?.Value;
                string name= node.Attributes?["name"]?.Value;
                LocationList.Add(new string[3] { x, y ,name});
            }
        }

        public List<string[]>GetLocationList(){
            return LocationList;
        }

    }
}

