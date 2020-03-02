using System.Collections.Generic;
using Synchro;
using UnityEngine;
using UnityEngine.SceneManagement;
using NaughtyAttributes;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using UnityEditor;

public class MasterSetup : Singleton<MasterSetup>
{
    public string default_wall_config;
    public SceneAsset startin_wall_scene;
    public List<Hololens> hololenses;

    [HideInInspector] public Wall wall;

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Additive);
        hololenses = new List<Hololens>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetupWall(List<Screen> ls, int h_size, int v_size, float h_real_size, float v_real_size, string setupName)
    {
        wall = new Wall();
        wall.AddScreenRange(ls);
        wall.SetTotalSize(h_size, v_size);
        wall.SetRealSize(h_real_size, v_real_size);
        wall.SetupWallDimensions();
        Debug.Log(wall.ToString());
        SaveSetup(setupName);
    }

    public void DeployWall(string password)
    {
        string sshKeyPath = Path.Combine(Application.streamingAssetsPath, "ssh\\wall_rsa");        
        wall.Deploy(sshKeyPath, password);
    }

    public void LaunchWall(string password)
    {
        if (wall.IsEmpty())
        {
            LoadConfig(default_wall_config + ".dat");
        }
        string sshKeyPath = Path.Combine(Application.dataPath, "Network\\ssh\\wall_rsa");
        wall.Start(sshKeyPath, password);
    }

    public void SetupHololens(List<Hololens> hl_list)
    {
        hololenses = hl_list;
        string result = "";
        for(int i = 0; i < hololenses.Count; i++)
        {
            result += hololenses[i].ToString() + "\n";
            
            NetMqSubscriber nmqs = gameObject.AddComponent<NetMqSubscriber>();
            nmqs.HostIpAddress = hololenses[i].IPAddress;
            nmqs.IpPort = int.Parse(hololenses[i].port);
            nmqs.Topic = "H";    
            gameObject.AddComponent<SynchroClient>();
        }
        Debug.Log(result);
        SaveSetupHL("hololens_config");
    }

    public string GetHololensName(string IP)
    {
        for(int i = 0; i < hololenses.Count; i++)
        {
            if (hololenses[i].IPAddress == IP)
                return hololenses[i].name;
        }
        throw new System.Exception("Unknown Hololens IP address");
    }

    [Button]
    public void resetDim()
    {
        wall.SetupWallDimensions();
        SaveSetup("wilder2");
    }

    [Button]
    public void ShowDataPath()
    {
        Debug.Log(Application.streamingAssetsPath);
        Debug.Log(Application.dataPath + "/Network/ssh/wall_rsa");
        Debug.Log(Path.Combine(Application.dataPath, "Network\\ssh\\walll_rsa"));
        Debug.Log(File.ReadAllText(Path.Combine(Application.dataPath, "Network/ssh\\wall_rsa")));
    }


    public void SaveSetup(string filename)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = File.Create(Path.Combine(Application.streamingAssetsPath, "configs/", filename));
        Debug.Log("Saved to : " + Path.Combine(Application.streamingAssetsPath, "configs/", filename));
        SurrogateSelector ss = new SurrogateSelector();

        // Adding Serialization surrogate to allow serializing Unity's structure that we use
        Vector2SerializationSurrogate vector2SerializationSurrogate = new Vector2SerializationSurrogate();
        Vector3SerializationSurrogate vector3SerializationSurrogate = new Vector3SerializationSurrogate();
        Vector4SerializationSurrogate vector4SerializationSurrogate = new Vector4SerializationSurrogate();
        ss.AddSurrogate(typeof(Vector2), new StreamingContext(StreamingContextStates.All), vector2SerializationSurrogate);
        ss.AddSurrogate(typeof(Vector3), new StreamingContext(StreamingContextStates.All), vector3SerializationSurrogate);
        ss.AddSurrogate(typeof(Vector4), new StreamingContext(StreamingContextStates.All), vector4SerializationSurrogate);
        bf.SurrogateSelector = ss;

        bf.Serialize(fs, wall);
        fs.Close();

        Debug.Log(wall.ToString());
    }

    public void SaveSetupHL(string filename)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = File.Create(Path.Combine(Application.streamingAssetsPath, "configs/", filename + ".dat"));
        Debug.Log("Saved to : " + Path.Combine(Application.streamingAssetsPath, "configs/", filename + ".dat"));
        SurrogateSelector ss = new SurrogateSelector();

        bf.SurrogateSelector = ss;

        bf.Serialize(fs, hololenses);
        fs.Close();

        string hls = "";
        foreach (var h in hololenses)
            hls += h.ToString() + "\n";
        Debug.Log(hls);
    }

    [Button]
    public void LoadSetup()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = File.Open(Path.Combine(Application.streamingAssetsPath, "configs/wall_config.dat"), FileMode.Open, FileAccess.Read);
        Debug.Log("Reading to : " + Path.Combine(Application.streamingAssetsPath, "configs/wall_config.dat"));
        SurrogateSelector ss = new SurrogateSelector();

        // Adding Serialization surrogate to allow serializing Unity's structure that we use
        Vector2SerializationSurrogate vector2SerializationSurrogate = new Vector2SerializationSurrogate();
        Vector3SerializationSurrogate vector3SerializationSurrogate = new Vector3SerializationSurrogate();
        Vector4SerializationSurrogate vector4SerializationSurrogate = new Vector4SerializationSurrogate();
        ss.AddSurrogate(typeof(Vector2), new StreamingContext(StreamingContextStates.All), vector2SerializationSurrogate);
        ss.AddSurrogate(typeof(Vector3), new StreamingContext(StreamingContextStates.All), vector3SerializationSurrogate);
        ss.AddSurrogate(typeof(Vector4), new StreamingContext(StreamingContextStates.All), vector4SerializationSurrogate);
        bf.SurrogateSelector = ss;

        fs.Close();
    }

    public void LoadConfig(string name)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = File.Open(Path.Combine(Application.streamingAssetsPath, "configs/", name), FileMode.Open, FileAccess.Read);
        Debug.Log("Reading to : " + Path.Combine(Application.streamingAssetsPath, "configs/", name));
        SurrogateSelector ss = new SurrogateSelector();

        // Adding Serialization surrogate to allow serializing Unity's structure that we use
        Vector2SerializationSurrogate vector2SerializationSurrogate = new Vector2SerializationSurrogate();
        Vector3SerializationSurrogate vector3SerializationSurrogate = new Vector3SerializationSurrogate();
        Vector4SerializationSurrogate vector4SerializationSurrogate = new Vector4SerializationSurrogate();
        ss.AddSurrogate(typeof(Vector2), new StreamingContext(StreamingContextStates.All), vector2SerializationSurrogate);
        ss.AddSurrogate(typeof(Vector3), new StreamingContext(StreamingContextStates.All), vector3SerializationSurrogate);
        ss.AddSurrogate(typeof(Vector4), new StreamingContext(StreamingContextStates.All), vector4SerializationSurrogate);
        bf.SurrogateSelector = ss;

        wall = (Wall)bf.Deserialize(fs);
        fs.Close();

        Debug.Log(wall.ToString());
    }

    public void LoadConfigHL(string name)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = File.Open(Path.Combine(Application.streamingAssetsPath, "configs/", name), FileMode.Open, FileAccess.Read);
        Debug.Log("Reading to : " + Path.Combine(Application.streamingAssetsPath, "configs/", name));
        SurrogateSelector ss = new SurrogateSelector();

        // Adding Serialization surrogate to allow serializing Unity's structure that we use
        bf.SurrogateSelector = ss;

        hololenses = (List<Hololens>)bf.Deserialize(fs);
        fs.Close();

        string hls = "";
        foreach (var h in hololenses)
            hls += h.ToString() + "\n";
        Debug.Log(hls);
    }

    [Button]
    public void SetSub()
    {
        gameObject.AddComponent<SynchroClient>();
    }

    [Button]
    public void IsEmpty()
    {
        Debug.Log(wall.IsEmpty());
    }
}
