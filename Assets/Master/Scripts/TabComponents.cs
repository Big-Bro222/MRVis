using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NaughtyAttributes;

public class TabComponents : MonoBehaviour
{
    public int id;

    public Button screenButton;
    public TextMeshProUGUI screenButtonText;

    public Button optButton;
    public Image optButtonArrow;

    public Image EditSection;
    public TMP_InputField nameField;
    public TMP_InputField ipField;

    [HideInInspector] public string screenName { private set; get; }
    [HideInInspector] public string stringIP { private set; get; }
    [HideInInspector] public Color screenColor;

    [ShowNonSerializedField]
    private bool editState;

    // Start is called before the first frame update
    void Start()
    {
        editState = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetColor(Color c)
    {
        screenColor = c;
        EditSection.color = c;
        var colors = screenButton.colors;
        colors.normalColor = c;
        screenButton.colors = colors;
        optButton.colors = colors;
    }

    public void EditState(bool state)
    {
        editState = state;
        EditSection.gameObject.SetActive(state);
        screenButton.gameObject.SetActive(!state);
        optButton.gameObject.SetActive(!state);
    }

    public void ValidateState()
    {
        screenName = nameField.text;
        stringIP = ipField.text;

        screenButtonText.text = screenName;

        EditState(false);
    }

    public void DeleteScreenTab()
    {
        GameObject.Find("WallSetupController").GetComponent<WallSetupController>().DeleteScreenTab(this);
    }
}
