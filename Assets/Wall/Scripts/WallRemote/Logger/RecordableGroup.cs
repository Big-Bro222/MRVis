using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordableGroup : MonoBehaviour
{
    [Tooltip("Can only Check on Box")]
    public bool all;
    public bool Child;
    public bool ChildNGrandChild;

    void Awake()
    {
        if (all)
        {
            foreach (Transform transform in GetComponentsInChildren<Transform>())
            {
                transform.gameObject.AddComponent<Recordable>();
            }
        }
        else if (Child)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.AddComponent<Recordable>();
            }
        }
        else if (ChildNGrandChild)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).childCount != 0)
                {
                    for (int j = 0; j < transform.childCount; j++)
                    {
                        transform.GetChild(i).GetChild(j).gameObject.AddComponent<Recordable>();
                    }
                }
            }
        }

    }

}
