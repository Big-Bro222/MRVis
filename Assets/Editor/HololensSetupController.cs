using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class HololensSetupController : MonoBehaviour
{
    public DeviceInfo DeviceInfoPrefab;
    public GameObject InputBar;
    public TMP_InputField Name;
    public TMP_InputField IP;
    public TMP_InputField ComID;

    private List<DeviceInfo> RegisteredHL;
    private List<Hololens> HLS;

    // Start is called before the first frame update
    void Start()
    {
        RegisteredHL = new List<DeviceInfo>();
        HLS = MasterSetup.Instance.hololenses;

        for(int i = 0; i < HLS.Count; i++)
        {
            DeviceInfo di = Instantiate(DeviceInfoPrefab, InputBar.transform.parent) as DeviceInfo;
            di.name = RegisteredHL.Count.ToString();
            di.Create(HLS[i].name, HLS[i].IPAddress, HLS[i].ComID);
            di.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, RegisteredHL.Count * -25);
            di.Remove.onClick.AddListener(() => OnRemoveDevice(int.Parse(di.name)));
            RegisteredHL.Add(di);
        }
        InputBar.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, RegisteredHL.Count * -25);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnAddDevice()
    {
        HLS.Add(new Hololens(
            Name.text,
            IP.text,
            ComID.text ));
        DeviceInfo di = Instantiate(DeviceInfoPrefab, InputBar.transform.parent) as DeviceInfo;
        di.name = RegisteredHL.Count.ToString();
        di.Create(Name.text, IP.text, ComID.text);
        di.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, RegisteredHL.Count * -25);
        di.Remove.onClick.AddListener(() => OnRemoveDevice(int.Parse(di.name)));
        RegisteredHL.Add(di);
        InputBar.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, RegisteredHL.Count * -25);
    }

    public void AddDevice(Hololens hl)
    {
        HLS.Add(hl);
        DeviceInfo di = Instantiate(DeviceInfoPrefab, InputBar.transform.parent) as DeviceInfo;
        di.name = RegisteredHL.Count.ToString();
        di.Create(hl.name, hl.IPAddress, hl.port);
        di.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, RegisteredHL.Count * -25);
        di.Remove.onClick.AddListener(() => OnRemoveDevice(int.Parse(di.name)));
        RegisteredHL.Add(di);
        InputBar.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, RegisteredHL.Count * -25);
    }

    public void OnRemoveDevice(int i)
    {
        HLS.RemoveAt(i);
        Destroy(RegisteredHL[i].gameObject);
        RegisteredHL.RemoveAt(i);
        for(int j = 0; j < RegisteredHL.Count; j++)
        {
            RegisteredHL[j].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, j*-25);
            RegisteredHL[j].name = j.ToString();
        }
        InputBar.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, RegisteredHL.Count * -25);
    }

    public void OnSave()
    {
        MasterSetup.Instance.SetupHololens(HLS);
        SceneManager.UnloadSceneAsync("HololensSetup");
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Additive);
    }

    public void LoadLastSetup()
    {
        MasterSetup.Instance.LoadConfigHL("hololens_config.dat");

        foreach (var v in RegisteredHL)
            Destroy(v.gameObject);

        RegisteredHL.Clear();
        HLS.Clear();
        InputBar.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);

        foreach (Hololens h in MasterSetup.Instance.hololenses)
        {
            AddDevice(h);
        }
        Debug.Log("Ok");
    }
}
