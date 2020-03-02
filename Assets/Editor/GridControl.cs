using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System.IO;

public class GridControl : MonoBehaviour
{
    public GameObject GridContent;
    public GameObject ConfigContent;
    public Button unitPrefab;

    public int lines { private set; get; } = 0;
    public int columns { private set; get; } = 0;

    public List<List<Button>> grid { private set; get; }

    [HideInInspector] public float height = 0;
    [HideInInspector] public float width = 0;

    // Start is called before the first frame update
    void Start()
    {
        grid = new List<List<Button>>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LinesUpdate(string value)
    {
        if (value == null || value == "")
            return;

        int newH = int.Parse(value);
        int delta = newH - lines;
        lines = newH;
        UpdateLines(delta);
    }

    public void ColumnsUpdate(string value)
    {
        Debug.Log(value);
        if (value == null || value == "")
            return;

        int newV = int.Parse(value);
        int delta = newV - columns;
        columns = newV;
        UpdateColumns(delta);
    }

    private void UpdateLines(int delta)
    {
        if (columns == 0)
        {
            for (int i = 0; i < delta; i++)
                grid.Add(new List<Button>());
            return;
        }

        if(delta < 0)
        {
            for(int i = lines - 1; i >= lines + delta; i--)
            {
                for(int j = 0; j < grid[i].Count; j++)
                    Destroy(grid[i][j].gameObject);
                grid.RemoveAt(i);
            }
        }
        else if(delta > 0)
        {
            for(int i = lines; i < lines + delta; i++)
            {
                List<Button> toAdd = new List<Button>();
                for(int j = 0; j<columns; j++)
                {
                    Button unit = Instantiate(unitPrefab, GridContent.transform) as Button;
                    unit.onClick.AddListener(() => GameObject.Find("WallSetupController").GetComponent<WallSetupController>().OnUnitSelected());
                    toAdd.Add(unit);
                }
                grid.Add(toAdd);
            }
        }

        PositionGrid();
    }

    private void UpdateColumns(int delta)
    {
        if (lines == 0)
            return;
        
        if (delta < 0)
        {
            for (int i = 0; i < lines; i++)
            {
                for(int j = -delta; j > 0; j--)
                {
                    Destroy(grid[i][j + columns - 1].gameObject);
                    grid[i].RemoveAt(j + columns - 1);
                }
            }
        }
        else if (delta > 0)
        {
            for (int i = 0; i < lines; i++)
            {
                for (int j = columns; j < columns + delta; j++)
                {
                    Button unit = Instantiate(unitPrefab, GridContent.transform) as Button;
                    unit.onClick.AddListener(() => GameObject.Find("WallSetupController").GetComponent<WallSetupController>().OnUnitSelected());
                    grid[i].Add(unit);
                }
            }
        }

        PositionGrid();
    }

    private void PositionGrid()
    {
        for(int i = 0; i < lines; i++)
        {
            for(int j = 0; j < columns; j++)
            {
                grid[i][j].GetComponent<RectTransform>().anchorMin = new Vector2(0, 1);
                grid[i][j].GetComponent<RectTransform>().anchorMax = new Vector2(0, 1);
                grid[i][j].GetComponent<RectTransform>().pivot = new Vector2(0, 1);
                grid[i][j].GetComponent<RectTransform>().anchoredPosition = new Vector3(35f * j, - 35f * i, -1f);
            }
        }
    }

    public void DeleteTab(int id)
    {
        ColorBlock color = new ColorBlock();
        color.normalColor = Color.white;
        color.highlightedColor = Color.white;
        color.pressedColor = Color.white;
        color.colorMultiplier = 1;
        color.fadeDuration = 0.1f;

        for (int i = 0; i < lines; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                if (grid[i][j].GetComponent<Unit>().ownerID == id)
                {
                    grid[i][j].GetComponent<Unit>().ownerID = 0;
                    grid[i][j].colors = color;
                }
            }
        }
    }

    public void UpdateWidth(string value)
    {
        if (value == null || value == "")
            return;

        width = float.Parse(value);
    }

    public void UpdateHeight(string value)
    {
        if (value == null || value == "")
            return;

        height = float.Parse(value);
    }

    private void ClearGrid()
    {
        for(int i = grid.Count-1; i >= 0; i--)
        {
            for(int j = grid[i].Count-1; j >= 0; j--)
            {
                Destroy(grid[i][j]);
            }
            grid[i].Clear();
        }
        grid.Clear();

        lines = 0;
        columns = 0;
        height = 0;
        width = 0;
    }

    public void LoadGrid(Wall wall)
    {
        ClearGrid();        

        List<Screen> screens = wall.GetScreenList();
        string h = wall.GetVUnit().ToString();
        string w = wall.GetHUnit().ToString();
        this.height = wall.GetVReal();
        this.width = wall.GetHReal();

        LinesUpdate(h);
        ColumnsUpdate(w);

        GameObject.Find("Lines").GetComponent<TMP_InputField>().text = h;
        GameObject.Find("Columns").GetComponent<TMP_InputField>().text = w;
        GameObject.Find("Height").GetComponent<TMP_InputField>().text = wall.GetVReal().ToString();
        GameObject.Find("Width").GetComponent<TMP_InputField>().text = wall.GetHReal().ToString();

        PositionGrid();
    }

    public void ColorGrid(Wall wall, List<TabComponents> tcs)
    {
        List<Screen> screens = wall.GetScreenList();

        foreach (Screen s in screens)
        {
            int l = 0;
            while (s.id != tcs[l].name)
            {
                l++;
            }
            Color currentColor = tcs[l].screenColor;
            int currentId = tcs[l].id;

            for (int i = (int)s.up_left_start.x; i < (int)(s.up_left_start.x + s.v_unit_size); i++)
            {
                for (int j = (int)s.up_left_start.y; j < (int)(s.up_left_start.y + s.h_unit_size); j++)
                {
                    grid[i][j].GetComponent<Unit>().ownerID = currentId;
                    ColorBlock color = new ColorBlock();
                    color.normalColor = currentColor;
                    color.highlightedColor = currentColor;
                    color.pressedColor = currentColor;
                    color.colorMultiplier = 1f;
                    grid[i][j].GetComponent<Button>().colors = color;
                }
            }
        }
    }
}
