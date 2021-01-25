using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class CsvParser 
{
    static CsvParser csv;
    public List<string> m_ArrayData;
    public static CsvParser GetInstance()
    {
        if (csv == null)
        {
            csv = new CsvParser();
        }
        return csv;
    }

    private CsvParser()
    {
        m_ArrayData = new List<string>();
    }

    public void LoadFile(string path,string fileName)
    {
        m_ArrayData.Clear();
        StreamReader sr = null;
        try
        {
            sr = File.OpenText(path + "//" + fileName);
            Debug.Log("file found");
        }
        catch
        {
            Debug.Log("file not found");
        }

        string line;
        while ((line = sr.ReadLine()) != null)
        {
            m_ArrayData.Add(line);
        }
        sr.Close();
        sr.Dispose(); 
    }
}
