using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DeviceInfo : MonoBehaviour
{
    public TextMeshProUGUI Name;
    public TextMeshProUGUI IP;
    public TextMeshProUGUI ComID;
    public Button Remove;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }    

    public void Create(string name, string ip, string comID)
    {
        Name.text = name;
        IP.text = ip;
        ComID.text = comID;
    }
}
