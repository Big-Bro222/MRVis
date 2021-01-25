using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class XMLFileGenerator : MonoBehaviour
{
    private XmlDocument xmlDocument;
    private XmlElement root;
    [SerializeField]
    InputField inputField;
    public List<string> logs;

    private void Start()
    {
        if (inputField.text.Equals(""))
        {
            inputField.text = "Default File";
        }
    }

    public void CreatXMLFile()
    {
        xmlDocument = new XmlDocument();
        root = xmlDocument.CreateElement("Save");
        root.SetAttribute("FileName", inputField.text);
        xmlDocument.AppendChild(root);
        foreach(string log in logs)
        {
            WirteXMLLine(log);
        }
    }

    private void WirteXMLLine(string Input)
    {
        XmlElement taskElement = xmlDocument.CreateElement("Task");
        root.AppendChild(taskElement);
        taskElement.InnerText = Input;
    }

    public void SaveCutScreenPath()
    {
        SaveFileDialog saveFileDialog = new SaveFileDialog();//new folderbrowser
        saveFileDialog.Filter = "XML-File | *.xml";
        saveFileDialog.FileName = inputField.text;
        saveFileDialog.AddExtension = true;
        saveFileDialog.OverwritePrompt = true;   //enable overwrite;
        saveFileDialog.Title = "Save file";
        saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);  //set default path
        //If pressing OK
        if (saveFileDialog.ShowDialog() == DialogResult.OK)
        {
            xmlDocument.Save(saveFileDialog.FileName);
        }
    }




}
