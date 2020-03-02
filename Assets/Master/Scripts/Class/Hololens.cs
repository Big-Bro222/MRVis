using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Hololens
{
    public string name;
    public string IPAddress;
    public string port;
    public string ComID;

    public Hololens(string name, string IPAdress, string ComID)
    {
        this.name = name;
        this.IPAddress = IPAdress;
        this.ComID = ComID;
        this.port = "12345";
    }

    public override string ToString()
    {
        return "Hololens " + name + " : " + IPAddress + " - " + port + " - " + ComID;
    }
}
