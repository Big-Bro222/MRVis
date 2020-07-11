using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScrollBehaviorHandler : MonoBehaviour
{
    public GameObject content;
    public GameObject eventListPrefab;

    public void TextEventReciever(string listText)
    {
        GameObject eventlist=Instantiate(eventListPrefab, content.transform);
        eventlist.GetComponentInChildren<TextMeshProUGUI>().text = listText;
    }


}
