using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class Wall
{
    [SerializeField]
    private List<Screen> screenList = new List<Screen>();

    [SerializeField]
    private int h_total_unit_size;
    [SerializeField]
    private int v_total_unit_size;

    [SerializeField]
    private float h_real_size;
    [SerializeField]
    private float v_real_size;

    public void AddScreen(Screen s)
    {
        screenList.Add(s);
    }

    public void AddScreenRange(List<Screen> ls)
    {
        screenList.AddRange(ls);
    }

    public void SetTotalSize(int h, int v)
    {
        h_total_unit_size = h;
        v_total_unit_size = v;
    }

    public void SetRealSize(float h, float v)
    {
        h_real_size = h;
        v_real_size = v;
    }

    public override string ToString()
    {
        string output = "Wall : dimension " + h_total_unit_size + "/" + v_total_unit_size + "\r\n";
        for(int i = 0; i < screenList.Count; i++)
        {
            output += screenList[i].ToString() + "\r\n";
        }
        return output;
    }

    public void Deploy(string key, string password)
    {
        for (int i = 0; i < screenList.Count; i++)
        {
            Screen currScreen = screenList[i];
            Task.Factory.StartNew(() => { currScreen.Deploy(key, password); });
        }
    }

    public void Start(string key, string password)
    {
        for(int i = 0; i<screenList.Count; i++)
        {
            Screen currScreen = screenList[i];
            Task.Factory.StartNew(() => { currScreen.Start(key, password); });
        }
    }    
    
    public void SetupWallDimensions()
    {
        for(int i = 0; i<screenList.Count; i++)
        {
            screenList[i].SetDimensions(this.h_real_size, this.v_real_size, this.h_total_unit_size, this.v_total_unit_size);
        }
    }    

    public List<Screen> GetScreenList()
    {
        return screenList;
    }

    public int GetHUnit()
    {
        return h_total_unit_size;
    }

    public int GetVUnit()
    {
        return v_total_unit_size;
    }

    public float GetHReal()
    {
        return h_real_size;
    }

    public float GetVReal()
    {
        return v_real_size;
    }

    public bool IsEmpty()
    {
        return screenList.Count == 0;
    }
}