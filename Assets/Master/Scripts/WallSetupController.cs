using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using NaughtyAttributes;
using System.IO;
using TMPro;

public class WallSetupController : MonoBehaviour
{
    public GameObject ScreenControl;
    public GameObject GC;
    public GameObject ConfigContent;

    public TabComponents tcPrefab;
    public Button configPrefab;    

    private List<TabComponents> screenTabList = new List<TabComponents>();
    private int screenNumber = 0;
    private int screenIds = 1;
    private Color selectedTabColor;
    private int selectedTabId;

    private string currentFilename = "";

    [ReorderableList]
    public Color[] selectableColors = new Color[]
    {
        new Color(0.8f, 0.2f, 0.2f),
        new Color(0.8f, 0.55f, 0.2f),
        new Color(0.9f, 1f, 0.3f),
        new Color(0.6f, 0.8f, 0.2f),
        new Color(0.2f, 0.8f, 0.3f),
        new Color(0.2f, 0.8f, 0.7f),
        new Color(0.2f, 0.6f, 0.8f),
        new Color(0.16f, 0.35f, 1f),
        new Color(1f, 0.5f, 1f),
        new Color(0.7f, 0.2f, 0.8f),
        new Color(0.8f, 0.2f, 0.6f),
        new Color(1f, 0.8f, 0f),
    };

    private List<Color> unusedColor = new List<Color>();
    
    // Start is called before the first frame update
    void Start()
    {
        DirectoryInfo info = new DirectoryInfo(Path.Combine(Application.streamingAssetsPath, "configs/"));
        FileInfo[] fileInfo = info.GetFiles();
        for (int i = 0; i < fileInfo.Length; i++)
        {
            if (fileInfo[i].Name.Contains(".meta")) continue;

            Button instance = Instantiate(configPrefab, ConfigContent.transform) as Button;
            instance.name = fileInfo[i].Name;
            instance.onClick.AddListener(() => LoadConfig(instance.name));
            instance.GetComponentInChildren<TextMeshProUGUI>().text = fileInfo[i].Name;
            instance.GetComponent<RectTransform>().anchorMin = new Vector2(0f, 0.5f);
            instance.GetComponent<RectTransform>().anchorMax = new Vector2(0f, 0.5f);
            instance.GetComponent<RectTransform>().pivot = new Vector2(0f, 0.5f);
            instance.GetComponent<RectTransform>().anchoredPosition = new Vector3(50f * i, 0f, -1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScreenTab()
    {
        TabComponents tc = Instantiate(tcPrefab, ScreenControl.transform) as TabComponents;        
        tc.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 1f);
        tc.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 1f);
        tc.GetComponent<RectTransform>().anchoredPosition = new Vector3(0f, - 30f * screenNumber -100f, -1f);
        if (unusedColor.Count > 0)
        {
            tc.SetColor(unusedColor[0]);
            unusedColor.RemoveAt(0);
        }
        else
        {
            tc.SetColor(selectableColors[screenNumber]);
        }
        tc.screenButton.onClick.AddListener(OnTabSelected);
        tc.id = screenIds;

        tc.name = "Screen" + screenIds.ToString();
        tc.screenButtonText.text = "Screen " + screenIds.ToString();

        screenNumber += 1;
        screenIds += 1;
        screenTabList.Add(tc);
    }

    public void ReloadScreenTab(string name, string ip)
    {
        TabComponents tc = Instantiate(tcPrefab, ScreenControl.transform) as TabComponents;
        tc.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 1f);
        tc.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 1f);
        tc.GetComponent<RectTransform>().anchoredPosition = new Vector3(0f, -30f * screenNumber - 100f, -1f);
        if (unusedColor.Count > 0)
        {
            tc.SetColor(unusedColor[0]);
            unusedColor.RemoveAt(0);
        }
        else
        {
            tc.SetColor(selectableColors[screenNumber]);
        }
        tc.screenButton.onClick.AddListener(OnTabSelected);
        tc.id = screenIds;

        tc.name = name;
        tc.screenButtonText.text = name;
        tc.nameField.text = name;
        tc.ipField.text = ip;
        tc.ValidateState();

        screenNumber += 1;
        screenIds += 1;
        screenTabList.Add(tc);
    }

    public void DeleteScreenTab(TabComponents tc)
    {
        int ToDeleteId = tc.id;
        unusedColor.Add(tc.screenColor);

        screenTabList.Remove(tc);
        Destroy(tc.gameObject);

        GC.GetComponent<GridControl>().DeleteTab(ToDeleteId);
        screenNumber -= 1;
        PositionScreenTabs();
    }

    void PositionScreenTabs()
    {
        for(int i = 0; i < screenNumber; i++)
        {
            screenTabList[i].GetComponent<RectTransform>().anchoredPosition = new Vector3(0f, -30f * i - 100f, -1f);
        }
    }

    public void OnTabSelected()
    {
        GameObject selectedCurrent = EventSystem.current.currentSelectedGameObject.transform.parent.gameObject;
        selectedTabId = selectedCurrent.GetComponent<TabComponents>().id;
        selectedTabColor = selectedCurrent.GetComponent<TabComponents>().screenColor;
    }

    public void OnUnitSelected()
    {
        GameObject selectedCurrent = EventSystem.current.currentSelectedGameObject;
        var color = selectedCurrent.GetComponent<Button>().colors;

        if (selectedCurrent.GetComponent<Unit>().ownerID == selectedTabId)
        {
            selectedCurrent.GetComponent<Unit>().ownerID = 0;
            color.normalColor = Color.white;
            color.highlightedColor = Color.white;
            color.pressedColor = Color.white;
            selectedCurrent.GetComponent<Button>().colors = color;
        }
        else
        {
            selectedCurrent.GetComponent<Unit>().ownerID = selectedTabId;
            color.normalColor = selectedTabColor;
            color.highlightedColor = selectedTabColor;
            color.pressedColor = selectedTabColor;
            selectedCurrent.GetComponent<Button>().colors = color;
        }
    }

    public void FileNameChange(string value)
    {
        currentFilename = value;
    }

    public void SaveWallSetup()
    {
        List<List<Button>> units = GC.GetComponent<GridControl>().grid;
        List<int> visited = new List<int>() { };
        List<Screen> screens = new List<Screen>();

        if (units.Count == 0)
        {
            SceneManager.UnloadSceneAsync("WallSetup");
            SceneManager.LoadScene("WallLaunch", LoadSceneMode.Additive);            
            return;
        }

        for(int i = 0; i < units.Count; i++)
        {
            for (int j = 0; j < units[0].Count; j++)
            {
                int currentScreenId = units[i][j].GetComponent<Unit>().ownerID;
                if(currentScreenId !=0 && !visited.Contains(currentScreenId))
                {
                    TabComponents curTc = MatchTabToId(currentScreenId);
                    if (curTc == null)
                        continue;

                    Screen curScreen = new Screen(curTc.screenName, curTc.stringIP, new Vector2(i,j));
                    int k = i;
                    int l = j;

                    Debug.Log(k + " " + l);
                    while (k < units.Count && units[k][j].GetComponent<Unit>().ownerID == currentScreenId)
                    {
                        k++;
                    }

                    while ( l < units[0].Count && units[i][l].GetComponent<Unit>().ownerID == currentScreenId)
                    {
                        l++;
                    }

                    curScreen.v_unit_size = k-i;
                    curScreen.h_unit_size = l-j;

                    visited.Add(currentScreenId);
                    screens.Add(curScreen);                    
                }
            }
        }

        MasterSetup.Instance.SetupWall(screens, GC.GetComponent<GridControl>().columns, GC.GetComponent<GridControl>().lines, GC.GetComponent<GridControl>().width, GC.GetComponent<GridControl>().height, currentFilename);
        SceneManager.UnloadSceneAsync("WallSetup");
        SceneManager.LoadScene("WallLaunch", LoadSceneMode.Additive);
    }

    public void LoadConfig(string name)
    {
        currentFilename = name;
        GameObject.Find("ConfigName").GetComponentInChildren<TMP_InputField>().text = name;
        ClearScreenTabs();


        MasterSetup.Instance.LoadConfig(name);


        Wall copy = MasterSetup.Instance.wall;
        List<Screen> screens = copy.GetScreenList();

        for (int i = 0; i < screens.Count; i++)
        {
            ReloadScreenTab(screens[i].id, screens[i].IPAddress);
        }
        GC.GetComponent<GridControl>().LoadGrid(copy);
        GC.GetComponent<GridControl>().ColorGrid(copy, screenTabList);

        //SceneManager.UnloadSceneAsync("WallSetup");
        //SceneManager.LoadScene("WallLaunch", LoadSceneMode.Additive);
    }

    private TabComponents MatchTabToId(int id)
    {
        int it = 0;
        while (it < screenTabList.Count && screenTabList[it].id != id)
        {
            it++;
        }
        return (it < screenTabList.Count) ? screenTabList[it] : null;
    }


    private void ClearScreenTabs()
    {
        for(int i = screenTabList.Count - 1; i >= 0; i++)
        {
            Destroy(screenTabList[i]);
        }
        screenTabList.Clear();
    }
}
